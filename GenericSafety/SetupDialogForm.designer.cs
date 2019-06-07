namespace ASCOM.GenericSafety
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.textBoxDataFileName = new System.Windows.Forms.TextBox();
            this.buttonChooseFile = new System.Windows.Forms.Button();
            this.textBoxSafeString = new System.Windows.Forms.TextBox();
            this.labelSafeString = new System.Windows.Forms.Label();
            this.textBoxUnsafeString = new System.Windows.Forms.TextBox();
            this.labelUnsafeString = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxUsePowerStatus = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(357, 155);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(357, 185);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Set the exact strings which the safety file contains.";
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.GenericSafety.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(368, 9);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "File Name";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(16, 180);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // textBoxDataFileName
            // 
            this.textBoxDataFileName.Location = new System.Drawing.Point(71, 68);
            this.textBoxDataFileName.Name = "textBoxDataFileName";
            this.textBoxDataFileName.Size = new System.Drawing.Size(254, 20);
            this.textBoxDataFileName.TabIndex = 7;
            this.toolTip1.SetToolTip(this.textBoxDataFileName, "An empty file name will not do this test.");
            // 
            // buttonChooseFile
            // 
            this.buttonChooseFile.Location = new System.Drawing.Point(331, 66);
            this.buttonChooseFile.Name = "buttonChooseFile";
            this.buttonChooseFile.Size = new System.Drawing.Size(29, 24);
            this.buttonChooseFile.TabIndex = 8;
            this.buttonChooseFile.Text = "...";
            this.buttonChooseFile.UseVisualStyleBackColor = true;
            this.buttonChooseFile.Click += new System.EventHandler(this.buttonChooseFile_Click);
            // 
            // textBoxSafeString
            // 
            this.textBoxSafeString.Location = new System.Drawing.Point(90, 119);
            this.textBoxSafeString.Name = "textBoxSafeString";
            this.textBoxSafeString.Size = new System.Drawing.Size(213, 20);
            this.textBoxSafeString.TabIndex = 10;
            // 
            // labelSafeString
            // 
            this.labelSafeString.AutoSize = true;
            this.labelSafeString.Location = new System.Drawing.Point(11, 122);
            this.labelSafeString.Name = "labelSafeString";
            this.labelSafeString.Size = new System.Drawing.Size(59, 13);
            this.labelSafeString.TabIndex = 9;
            this.labelSafeString.Text = "Safe String";
            // 
            // textBoxUnsafeString
            // 
            this.textBoxUnsafeString.Location = new System.Drawing.Point(90, 145);
            this.textBoxUnsafeString.Name = "textBoxUnsafeString";
            this.textBoxUnsafeString.Size = new System.Drawing.Size(213, 20);
            this.textBoxUnsafeString.TabIndex = 12;
            // 
            // labelUnsafeString
            // 
            this.labelUnsafeString.AutoSize = true;
            this.labelUnsafeString.Location = new System.Drawing.Point(13, 148);
            this.labelUnsafeString.Name = "labelUnsafeString";
            this.labelUnsafeString.Size = new System.Drawing.Size(71, 13);
            this.labelUnsafeString.TabIndex = 11;
            this.labelUnsafeString.Text = "Unsafe String";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(112, 181);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(99, 13);
            this.labelVersion.TabIndex = 13;
            this.labelVersion.Text = "Version: 0.0.0000.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(273, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Set the file which contains the safety state in the first line";
            // 
            // checkBoxUsePowerStatus
            // 
            this.checkBoxUsePowerStatus.AutoSize = true;
            this.checkBoxUsePowerStatus.Location = new System.Drawing.Point(16, 12);
            this.checkBoxUsePowerStatus.Name = "checkBoxUsePowerStatus";
            this.checkBoxUsePowerStatus.Size = new System.Drawing.Size(158, 17);
            this.checkBoxUsePowerStatus.TabIndex = 15;
            this.checkBoxUsePowerStatus.Text = "Unsafe if mains power  is off";
            this.toolTip1.SetToolTip(this.checkBoxUsePowerStatus, "Use this to detect when a laptop is running on battery power.\r\nUsing mains power " +
        "and unknown will retun true.");
            this.checkBoxUsePowerStatus.UseVisualStyleBackColor = true;
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 218);
            this.Controls.Add(this.checkBoxUsePowerStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.textBoxUnsafeString);
            this.Controls.Add(this.labelUnsafeString);
            this.Controls.Add(this.textBoxSafeString);
            this.Controls.Add(this.labelSafeString);
            this.Controls.Add(this.buttonChooseFile);
            this.Controls.Add(this.textBoxDataFileName);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenericSafety Setup";
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.TextBox textBoxDataFileName;
        private System.Windows.Forms.Button buttonChooseFile;
        private System.Windows.Forms.TextBox textBoxSafeString;
        private System.Windows.Forms.Label labelSafeString;
        private System.Windows.Forms.TextBox textBoxUnsafeString;
        private System.Windows.Forms.Label labelUnsafeString;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxUsePowerStatus;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}