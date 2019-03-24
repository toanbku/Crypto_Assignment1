namespace MatMaAnimal
{
    partial class GeneratorKeyForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnKeyCopy = new MetroFramework.Controls.MetroButton();
            this.btnSaveGenKey = new MetroFramework.Controls.MetroButton();
            this.btnKeyGenerate = new MetroFramework.Controls.MetroButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGenKeyLength = new System.Windows.Forms.TextBox();
            this.txtKeyGen = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbKeySize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenKey = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 47);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(551, 304);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnKeyCopy);
            this.tabPage1.Controls.Add(this.btnSaveGenKey);
            this.tabPage1.Controls.Add(this.btnKeyGenerate);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtGenKeyLength);
            this.tabPage1.Controls.Add(this.txtKeyGen);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(543, 275);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Symmetric";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnKeyCopy
            // 
            this.btnKeyCopy.Location = new System.Drawing.Point(415, 29);
            this.btnKeyCopy.Name = "btnKeyCopy";
            this.btnKeyCopy.Size = new System.Drawing.Size(122, 23);
            this.btnKeyCopy.TabIndex = 4;
            this.btnKeyCopy.Text = "Copy to clipboard";
            this.btnKeyCopy.UseSelectable = true;
            this.btnKeyCopy.Click += new System.EventHandler(this.btnKeyCopy_Click);
            // 
            // btnSaveGenKey
            // 
            this.btnSaveGenKey.BackColor = System.Drawing.Color.LightSalmon;
            this.btnSaveGenKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveGenKey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveGenKey.Location = new System.Drawing.Point(242, 149);
            this.btnSaveGenKey.Name = "btnSaveGenKey";
            this.btnSaveGenKey.Size = new System.Drawing.Size(150, 81);
            this.btnSaveGenKey.TabIndex = 3;
            this.btnSaveGenKey.Text = "Save Key";
            this.btnSaveGenKey.UseSelectable = true;
            this.btnSaveGenKey.Click += new System.EventHandler(this.btnSaveGenKey_Click);
            // 
            // btnKeyGenerate
            // 
            this.btnKeyGenerate.BackColor = System.Drawing.Color.LightSalmon;
            this.btnKeyGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKeyGenerate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnKeyGenerate.Location = new System.Drawing.Point(70, 149);
            this.btnKeyGenerate.Name = "btnKeyGenerate";
            this.btnKeyGenerate.Size = new System.Drawing.Size(150, 81);
            this.btnKeyGenerate.TabIndex = 3;
            this.btnKeyGenerate.Text = "Generate";
            this.btnKeyGenerate.UseSelectable = true;
            this.btnKeyGenerate.Click += new System.EventHandler(this.btnKeyGenerate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Key Length";
            // 
            // txtGenKeyLength
            // 
            this.txtGenKeyLength.Location = new System.Drawing.Point(165, 86);
            this.txtGenKeyLength.Name = "txtGenKeyLength";
            this.txtGenKeyLength.Size = new System.Drawing.Size(227, 22);
            this.txtGenKeyLength.TabIndex = 0;
            // 
            // txtKeyGen
            // 
            this.txtKeyGen.Location = new System.Drawing.Point(70, 30);
            this.txtKeyGen.Name = "txtKeyGen";
            this.txtKeyGen.Size = new System.Drawing.Size(322, 22);
            this.txtKeyGen.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.btnGenKey);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(543, 275);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Asymmetric";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Key Generator";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbKeySize);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(92, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 79);
            this.panel1.TabIndex = 5;
            // 
            // cbKeySize
            // 
            this.cbKeySize.FormattingEnabled = true;
            this.cbKeySize.Items.AddRange(new object[] {
            "2048",
            "3072 (recommended)",
            "4096",
            "8192",
            "16384"});
            this.cbKeySize.Location = new System.Drawing.Point(125, 25);
            this.cbKeySize.Name = "cbKeySize";
            this.cbKeySize.Size = new System.Drawing.Size(180, 24);
            this.cbKeySize.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Key Size:";
            // 
            // btnGenKey
            // 
            this.btnGenKey.Location = new System.Drawing.Point(219, 184);
            this.btnGenKey.Name = "btnGenKey";
            this.btnGenKey.Size = new System.Drawing.Size(116, 23);
            this.btnGenKey.TabIndex = 4;
            this.btnGenKey.Text = "Generate";
            this.btnGenKey.UseVisualStyleBackColor = true;
            this.btnGenKey.Click += new System.EventHandler(this.btnGenKey_Click);
            // 
            // GeneratorKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 352);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Name = "GeneratorKeyForm";
            this.Text = "GeneratorKey";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroButton btnKeyCopy;
        private MetroFramework.Controls.MetroButton btnKeyGenerate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGenKeyLength;
        private System.Windows.Forms.TextBox txtKeyGen;
        private MetroFramework.Controls.MetroButton btnSaveGenKey;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbKeySize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenKey;
    }
}