namespace FixMeCodeWatcher
{
    partial class Form1
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
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.buttonDirectoryPicker = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.timerReload = new System.Windows.Forms.Timer(this.components);
            this.checkBoxFixMe = new System.Windows.Forms.CheckBox();
            this.checkBoxToDo = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDirectory.Location = new System.Drawing.Point(2, 3);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(345, 20);
            this.textBoxDirectory.TabIndex = 0;
            this.textBoxDirectory.TextChanged += new System.EventHandler(this.textBoxDirectory_TextChanged);
            // 
            // buttonDirectoryPicker
            // 
            this.buttonDirectoryPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDirectoryPicker.Location = new System.Drawing.Point(351, 1);
            this.buttonDirectoryPicker.Name = "buttonDirectoryPicker";
            this.buttonDirectoryPicker.Size = new System.Drawing.Size(26, 23);
            this.buttonDirectoryPicker.TabIndex = 1;
            this.buttonDirectoryPicker.Text = "...";
            this.buttonDirectoryPicker.UseVisualStyleBackColor = true;
            this.buttonDirectoryPicker.Click += new System.EventHandler(this.buttonDirectoryPicker_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.Location = new System.Drawing.Point(0, 53);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(379, 401);
            this.textBoxOutput.TabIndex = 2;
            // 
            // timerReload
            // 
            this.timerReload.Enabled = true;
            this.timerReload.Interval = 1000;
            this.timerReload.Tick += new System.EventHandler(this.timerReload_Tick);
            // 
            // checkBoxFixMe
            // 
            this.checkBoxFixMe.AutoSize = true;
            this.checkBoxFixMe.Location = new System.Drawing.Point(12, 29);
            this.checkBoxFixMe.Name = "checkBoxFixMe";
            this.checkBoxFixMe.Size = new System.Drawing.Size(68, 18);
            this.checkBoxFixMe.TabIndex = 3;
            this.checkBoxFixMe.Text = "FixMe(s)";
            this.checkBoxFixMe.UseVisualStyleBackColor = true;
            this.checkBoxFixMe.CheckedChanged += new System.EventHandler(this.checkBoxFixMe_CheckedChanged);
            // 
            // checkBoxToDo
            // 
            this.checkBoxToDo.AutoSize = true;
            this.checkBoxToDo.Location = new System.Drawing.Point(86, 29);
            this.checkBoxToDo.Name = "checkBoxToDo";
            this.checkBoxToDo.Size = new System.Drawing.Size(64, 18);
            this.checkBoxToDo.TabIndex = 4;
            this.checkBoxToDo.Text = "ToDo(s)";
            this.checkBoxToDo.UseVisualStyleBackColor = true;
            this.checkBoxToDo.CheckedChanged += new System.EventHandler(this.checkBoxToDo_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 453);
            this.Controls.Add(this.checkBoxToDo);
            this.Controls.Add(this.checkBoxFixMe);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonDirectoryPicker);
            this.Controls.Add(this.textBoxDirectory);
            this.Name = "Form1";
            this.Text = "FixMe Code Watcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.Button buttonDirectoryPicker;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Timer timerReload;
        private System.Windows.Forms.CheckBox checkBoxFixMe;
        private System.Windows.Forms.CheckBox checkBoxToDo;
    }
}

