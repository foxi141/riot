namespace riotpls
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
            this.summonerNameTextBox = new System.Windows.Forms.TextBox();
            this.tagTextBox = new System.Windows.Forms.TextBox();
            this.btnCheckPuuid = new System.Windows.Forms.Button();
            this.apiKeyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // summonerNameTextBox
            // 
            this.summonerNameTextBox.Location = new System.Drawing.Point(226, 136);
            this.summonerNameTextBox.Name = "summonerNameTextBox";
            this.summonerNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.summonerNameTextBox.TabIndex = 0;
            this.summonerNameTextBox.TextChanged += new System.EventHandler(this.summonerNameTextBox_TextChanged);
            // 
            // tagTextBox
            // 
            this.tagTextBox.Location = new System.Drawing.Point(433, 136);
            this.tagTextBox.Name = "tagTextBox";
            this.tagTextBox.Size = new System.Drawing.Size(100, 20);
            this.tagTextBox.TabIndex = 1;
            this.tagTextBox.TextChanged += new System.EventHandler(this.tagTextBox_TextChanged);
            // 
            // btnCheckPuuid
            // 
            this.btnCheckPuuid.Location = new System.Drawing.Point(342, 136);
            this.btnCheckPuuid.Name = "btnCheckPuuid";
            this.btnCheckPuuid.Size = new System.Drawing.Size(75, 23);
            this.btnCheckPuuid.TabIndex = 2;
            this.btnCheckPuuid.Text = "button1";
            this.btnCheckPuuid.UseVisualStyleBackColor = true;
            this.btnCheckPuuid.Click += new System.EventHandler(this.btnCheckPuuid_Click);
            // 
            // apiKeyTextBox
            // 
            this.apiKeyTextBox.Location = new System.Drawing.Point(222, 92);
            this.apiKeyTextBox.Name = "apiKeyTextBox";
            this.apiKeyTextBox.Size = new System.Drawing.Size(311, 20);
            this.apiKeyTextBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.apiKeyTextBox);
            this.Controls.Add(this.btnCheckPuuid);
            this.Controls.Add(this.tagTextBox);
            this.Controls.Add(this.summonerNameTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox summonerNameTextBox;
        private System.Windows.Forms.TextBox tagTextBox;
        private System.Windows.Forms.Button btnCheckPuuid;
        private System.Windows.Forms.TextBox apiKeyTextBox;
    }
}

