namespace MonitorRenamer
{
    partial class MonitorRenamer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.monitorComboBox = new System.Windows.Forms.ComboBox();
            this.displayNameLabel = new System.Windows.Forms.Label();
            this.displayNameTextBox = new System.Windows.Forms.TextBox();
            this.renameButton = new System.Windows.Forms.Button();
            this.aboutLabel = new System.Windows.Forms.LinkLabel();
            this.usageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // monitorComboBox
            // 
            this.monitorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monitorComboBox.FormattingEnabled = true;
            this.monitorComboBox.Location = new System.Drawing.Point(12, 35);
            this.monitorComboBox.Name = "monitorComboBox";
            this.monitorComboBox.Size = new System.Drawing.Size(422, 21);
            this.monitorComboBox.TabIndex = 0;
            this.monitorComboBox.SelectedIndexChanged += new System.EventHandler(this.monitorComboBox_SelectedIndexChanged);
            // 
            // displayNameLabel
            // 
            this.displayNameLabel.AutoSize = true;
            this.displayNameLabel.Location = new System.Drawing.Point(12, 74);
            this.displayNameLabel.Name = "displayNameLabel";
            this.displayNameLabel.Size = new System.Drawing.Size(73, 13);
            this.displayNameLabel.TabIndex = 1;
            this.displayNameLabel.Text = "Display name:";
            // 
            // displayNameTextBox
            // 
            this.displayNameTextBox.Location = new System.Drawing.Point(91, 71);
            this.displayNameTextBox.Name = "displayNameTextBox";
            this.displayNameTextBox.Size = new System.Drawing.Size(217, 20);
            this.displayNameTextBox.TabIndex = 2;
            // 
            // renameButton
            // 
            this.renameButton.Location = new System.Drawing.Point(314, 69);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(120, 23);
            this.renameButton.TabIndex = 3;
            this.renameButton.Text = "Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
            // 
            // aboutLabel
            // 
            this.aboutLabel.AutoSize = true;
            this.aboutLabel.Location = new System.Drawing.Point(399, 98);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Size = new System.Drawing.Size(35, 13);
            this.aboutLabel.TabIndex = 4;
            this.aboutLabel.TabStop = true;
            this.aboutLabel.Text = "About";
            this.aboutLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.aboutLabel_LinkClicked);
            // 
            // usageLabel
            // 
            this.usageLabel.AutoSize = true;
            this.usageLabel.Location = new System.Drawing.Point(88, 9);
            this.usageLabel.Name = "usageLabel";
            this.usageLabel.Size = new System.Drawing.Size(301, 13);
            this.usageLabel.TabIndex = 5;
            this.usageLabel.Text = "Drag this window to a monitor to find its matching hardware ID.";
            // 
            // MonitorRenamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 120);
            this.Controls.Add(this.usageLabel);
            this.Controls.Add(this.aboutLabel);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.displayNameTextBox);
            this.Controls.Add(this.displayNameLabel);
            this.Controls.Add(this.monitorComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MonitorRenamer";
            this.Text = "MonitorRenamer";
            this.Load += new System.EventHandler(this.MonitorRenamer_Load);
            this.Move += new System.EventHandler(this.MonitorRenamer_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox monitorComboBox;
        private System.Windows.Forms.Label displayNameLabel;
        private System.Windows.Forms.TextBox displayNameTextBox;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.LinkLabel aboutLabel;
        private System.Windows.Forms.Label usageLabel;
    }
}

