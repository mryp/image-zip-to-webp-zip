
namespace ImageZipToWebpZip
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderPathRefButton = new System.Windows.Forms.Button();
            this.qualityNumeric = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.levelZeroCheckBox = new System.Windows.Forms.CheckBox();
            this.deleteCheckBox = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressStatusLabel = new System.Windows.Forms.Label();
            this.logButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.cancelButton = new System.Windows.Forms.Button();
            this.useSubfolderCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.qualityNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // folderPathTextBox
            // 
            this.folderPathTextBox.Location = new System.Drawing.Point(109, 12);
            this.folderPathTextBox.Name = "folderPathTextBox";
            this.folderPathTextBox.Size = new System.Drawing.Size(606, 27);
            this.folderPathTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "フォルダパス：";
            // 
            // folderPathRefButton
            // 
            this.folderPathRefButton.Location = new System.Drawing.Point(721, 11);
            this.folderPathRefButton.Name = "folderPathRefButton";
            this.folderPathRefButton.Size = new System.Drawing.Size(70, 29);
            this.folderPathRefButton.TabIndex = 2;
            this.folderPathRefButton.Text = "参照";
            this.folderPathRefButton.UseVisualStyleBackColor = true;
            this.folderPathRefButton.Click += new System.EventHandler(this.folderPathRefButton_Click);
            // 
            // qualityNumeric
            // 
            this.qualityNumeric.Location = new System.Drawing.Point(109, 45);
            this.qualityNumeric.Name = "qualityNumeric";
            this.qualityNumeric.Size = new System.Drawing.Size(103, 27);
            this.qualityNumeric.TabIndex = 3;
            this.qualityNumeric.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "品質：";
            // 
            // levelZeroCheckBox
            // 
            this.levelZeroCheckBox.AutoSize = true;
            this.levelZeroCheckBox.Checked = true;
            this.levelZeroCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.levelZeroCheckBox.Location = new System.Drawing.Point(109, 78);
            this.levelZeroCheckBox.Name = "levelZeroCheckBox";
            this.levelZeroCheckBox.Size = new System.Drawing.Size(274, 24);
            this.levelZeroCheckBox.TabIndex = 5;
            this.levelZeroCheckBox.Text = "変換後のZIPファイルは無圧縮で作成する";
            this.levelZeroCheckBox.UseVisualStyleBackColor = true;
            // 
            // deleteCheckBox
            // 
            this.deleteCheckBox.AutoSize = true;
            this.deleteCheckBox.Checked = true;
            this.deleteCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteCheckBox.Location = new System.Drawing.Point(109, 108);
            this.deleteCheckBox.Name = "deleteCheckBox";
            this.deleteCheckBox.Size = new System.Drawing.Size(171, 24);
            this.deleteCheckBox.TabIndex = 6;
            this.deleteCheckBox.Text = "ZIPファイルは上書きする";
            this.deleteCheckBox.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 180);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(779, 49);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "処理開始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 235);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(779, 29);
            this.progressBar.TabIndex = 8;
            // 
            // progressStatusLabel
            // 
            this.progressStatusLabel.AutoSize = true;
            this.progressStatusLabel.Location = new System.Drawing.Point(12, 267);
            this.progressStatusLabel.Name = "progressStatusLabel";
            this.progressStatusLabel.Size = new System.Drawing.Size(54, 20);
            this.progressStatusLabel.TabIndex = 9;
            this.progressStatusLabel.Text = "未処理";
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(489, 303);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(148, 29);
            this.logButton.TabIndex = 10;
            this.logButton.Text = "ログフォルダを開く";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Location = new System.Drawing.Point(643, 303);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(148, 29);
            this.aboutButton.TabIndex = 11;
            this.aboutButton.Text = "バージョン情報";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 303);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(91, 29);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // useSubfolderCheckBox
            // 
            this.useSubfolderCheckBox.AutoSize = true;
            this.useSubfolderCheckBox.Location = new System.Drawing.Point(109, 138);
            this.useSubfolderCheckBox.Name = "useSubfolderCheckBox";
            this.useSubfolderCheckBox.Size = new System.Drawing.Size(264, 24);
            this.useSubfolderCheckBox.TabIndex = 13;
            this.useSubfolderCheckBox.Text = "サブフォルダ内のZIPファイルも対象にする";
            this.useSubfolderCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 364);
            this.Controls.Add(this.useSubfolderCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.progressStatusLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.deleteCheckBox);
            this.Controls.Add(this.levelZeroCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.qualityNumeric);
            this.Controls.Add(this.folderPathRefButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderPathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Image Zip to Webp Zip";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.qualityNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button folderPathRefButton;
        private System.Windows.Forms.NumericUpDown qualityNumeric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox levelZeroCheckBox;
        private System.Windows.Forms.CheckBox deleteCheckBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressStatusLabel;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox useSubfolderCheckBox;
    }
}

