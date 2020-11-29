using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebPWrapper;

namespace ImageZipToWebpZip
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// ワーカー処理に渡すパラメータークラス
        /// </summary>
        protected class WorkerParam
        {
            public string FolderPath { get; set; } = "";
            public int Quality { get; set; } = 75;
            public CompressionLevel Level { get; set; } = CompressionLevel.NoCompression;
            public bool IsDelete { get; set; } = true;

            public override string ToString()
            {
                return $"{FolderPath},{Quality},{Level},{IsDelete}";
            }
        }

        /// <summary>
        /// WebPに変換する画像ファイルの拡張子リスト
        /// </summary>
        private static readonly string[] _imageExtList = { ".jpg", ".png" };

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// アプリ起動時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_Load(object sender, EventArgs e)
        {
            Log.Info("アプリケーション起動");
        }

        /// <summary>
        /// アプリ終了時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Info("アプリケーション終了");
        }

        /// <summary>
        /// フォルダ参照ボタンを押下したとき
        /// </summary>
        private void folderPathRefButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 処理開始ボタンを押下したとき
        /// </summary>
        private void startButton_Click(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                return;
            }

            var folderPath = folderPathTextBox.Text;
            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("フォルダパスが指定されていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("フォルダパスが存在しません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            startButton.Enabled = false;
            cancelButton.Enabled = true;
            bgWorker.RunWorkerAsync(new WorkerParam()
            {
                FolderPath = folderPath,
                Quality = (int)qualityNumeric.Value,
                Level = levelZeroCheckBox.Checked ? CompressionLevel.NoCompression : CompressionLevel.Fastest,
                IsDelete = deleteCheckBox.Checked ? true : false,
            });
        }

        /// <summary>
        /// ログ参照ボタンを押下したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(Application.StartupPath, "logs"));
        }

        /// <summary>
        /// バージョン情報ボタンを押下したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ImageZip to WebPZip version 1.0.0\r\ncorpyright (c) 2020 picobox");
        }

        /// <summary>
        /// 処理キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            bgWorker.CancelAsync();
        }

        /// <summary>
        /// 非同期実処理ワーカー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (sender is not BackgroundWorker worker)
            {
                Log.Error("sender is not BackgroundWorker");
                e.Result = "処理パラメーターエラー";
                return;
            }
            if (e.Argument is not WorkerParam param)
            {
                Log.Error("e.Argument is not WorkerParam");
                e.Result = "処理パラメーターエラー";
                return;
            }

            Log.Info($"処理開始 param={param}");
            var zipFileList = Directory.GetFiles(param.FolderPath, "*.zip");
            if (zipFileList.Length == 0)
            {
                Log.Warn("フォルダ内にZIPファイルが見つからない");
                e.Result = "指定されたフォルダ内にZIPファイルが見つかりません";
                return;
            }

            worker.ReportProgress(zipFileList.Length);
            var pos = 0;
            var successCount = 0;
            var errorCount = 0;
            foreach (var zipFile in zipFileList)
            {
                worker.ReportProgress(pos, Path.GetFileName(zipFile));
                if (worker.CancellationPending)
                {
                    Log.Info("処理キャンセル検知");
                    e.Cancel = true;
                    return;
                }

                Log.Info($"処理ファイル={zipFile}");
                if (imageZipToWebpZip(zipFile, param))
                {
                    successCount++;
                }
                else
                {
                    errorCount++;
                }
                pos++;
                worker.ReportProgress(pos, Path.GetFileName(zipFile));
            }

            Log.Info("処理完了");
            e.Result = $"処理結果 成功={successCount} 失敗={errorCount}";
        }

        /// <summary>
        /// ZIPファイルの画像をWebPに変換してZIPファイルとして再度作成する
        /// </summary>
        /// <param name="zipFile">ZIPファイルパス</param>
        /// <param name="param">変換パラメーター</param>
        /// <returns></returns>
        private static bool imageZipToWebpZip(string zipFile, WorkerParam param)
        {
            string workDir = "";
            try
            {
                //作業フォルダを作成
                workDir = createWorkDir();
                Log.Debug($"workDir={workDir}");

                //ZIP展開
                if (!unzip(zipFile, workDir))
                {
                    return false;
                }

                //画像ファイル一覧を取得
                var imageFileList = Directory.GetFiles(workDir).Where(
                    file => _imageExtList.Any(ext => file.ToLower().EndsWith(ext)));
                if (!imageFileList.Any())
                {
                    return false;
                }

                //画像を変換する
                foreach (var imageFile in imageFileList)
                {
                    if (!imageToWebp(imageFile, param.Quality, true))
                    {
                        return false;
                    }
                }

                //ZIPに固める
                var workZipFile = workDir + ".zip";
                if (!zip(workDir, workZipFile, param.Level))
                {
                    return false;
                }

                //元の位置に移動する
                if (param.IsDelete)
                {
                    File.Move(workZipFile, zipFile, true);
                }
                else
                {
                    var zipDirPath = Path.GetDirectoryName(zipFile);
                    if (zipDirPath == null)
                    {
                        return false;
                    }

                    var moveZipPath = Path.Combine(zipDirPath, Path.GetFileNameWithoutExtension(zipFile)
                        + "_webp.zip");
                    File.Move(workZipFile, moveZipPath, true);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "変換処理例が発生");
                return false;
            }
            finally
            {
                //作業フォルダを削除
                if (!string.IsNullOrEmpty(workDir))
                {
                    if (Directory.Exists(workDir))
                    {
                        Directory.Delete(workDir, true);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ZIPファイルの展開・WebPファイル変換を行うための作業フォルダを作成して返す
        /// </summary>
        /// <returns></returns>
        private static string createWorkDir()
        {
            var tempPath = Path.GetTempPath();
            var dirPath = Path.Combine(tempPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(dirPath);

            return dirPath;
        }

        /// <summary>
        /// ZIPファイルを展開する
        /// </summary>
        /// <param name="zipFile"></param>
        /// <param name="outputDir"></param>
        /// <returns></returns>
        private static bool unzip(string zipFile, string outputDir)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFile, outputDir);
            }
            catch (Exception e)
            {
                Log.Error(e, "Zip展開処理例外発生");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 画像ファイルをWebPファイル形式に変換する
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="quality"></param>
        /// <param name="isDeleteSourceFile"></param>
        /// <returns></returns>
        private static bool imageToWebp(string imagePath, int quality, bool isDeleteSourceFile)
        {
            var dirPath = Path.GetDirectoryName(imagePath);
            if (dirPath == null)
            {
                Log.Warn($"画像ファイルパスはフルパスで指定してください file={imagePath}");
                return false;
            }
            var outputPath = Path.Combine(dirPath, Path.GetFileNameWithoutExtension(imagePath) + ".webp");
            if (File.Exists(outputPath))
            {
                Log.Warn($"WebPファイルが既に存在しています file={outputPath}");
                return false;
            }

            try
            {
                using (var bitmap = new Bitmap(imagePath))
                {
                    using var convBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height)
                        , System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    using WebP webp = new WebP();
                    webp.Save(convBitmap, outputPath, quality);
                }

                if (isDeleteSourceFile)
                {
                    File.Delete(imagePath);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "WebP変換処理例外");
                return false;
            }

            return true;
        }

        /// <summary>
        /// フォルダをZIPファイルに変換する
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="outputPath"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private static bool zip(string dirPath, string outputPath, CompressionLevel level)
        {
            if (File.Exists(outputPath))
            {
                Log.Warn($"ZIPファイルが既に存在しています file={outputPath}");
                return false;
            }

            try
            {
                ZipFile.CreateFromDirectory(dirPath, outputPath, level, false);
            }
            catch (Exception e)
            {
                Log.Error(e, $"ZIP作成処理例外発生={e.Message}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// ワーカー進捗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var message = "";
            if (e.UserState != null)
            {
                message = e.UserState.ToString();
            }

            if (message == "")
            {
                progressBar.Maximum = e.ProgressPercentage;
                progressBar.Value = 0;
                progressStatusLabel.Text = "";
            }
            else
            {
                progressBar.Value = e.ProgressPercentage;
                progressStatusLabel.Text = message;
            }
        }

        /// <summary>
        /// ワーカー処理完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            startButton.Enabled = true;
            cancelButton.Enabled = false;
            if (e.Cancelled)
            {
                MessageBox.Show("キャンセルされました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                progressStatusLabel.Text = "キャンセルされました";
            }
            else if (e.Result == null)
            {
                MessageBox.Show("想定外のエラーが発生しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressStatusLabel.Text = "想定外のエラーが発生しました";
            }
            else
            {
                MessageBox.Show(e.Result.ToString(), "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressStatusLabel.Text = e.Result.ToString();
            }
        }
    }
}
