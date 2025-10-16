namespace TokatsApplication_v1._1
{
    partial class A1010101
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(A1010101));
            panel1 = new Panel();
            Panel1_lab1 = new Label();
            panel2 = new Panel();
            panel3 = new Panel();
            Panel3_Panel3 = new Panel();
            Panel3_Panel3_But1 = new Button();
            Panel3_Panel2 = new Panel();
            Panel3_Panel2_Text1 = new TextBox();
            label1 = new Label();
            Panel3_Panel1 = new Panel();
            Panel3_Panel1_Text1 = new TextBox();
            Panel3_Lab2 = new Label();
            Panel3_Lab1 = new Label();
            TPanel2 = new TableLayoutPanel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            Panel3_Panel3.SuspendLayout();
            Panel3_Panel2.SuspendLayout();
            Panel3_Panel1.SuspendLayout();
            TPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(Panel1_lab1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1682, 150);
            panel1.TabIndex = 0;
            // 
            // Panel1_lab1
            // 
            Panel1_lab1.Dock = DockStyle.Fill;
            Panel1_lab1.Font = new Font("メイリオ", 36F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Panel1_lab1.Location = new Point(0, 0);
            Panel1_lab1.Name = "Panel1_lab1";
            Panel1_lab1.Size = new Size(1682, 150);
            Panel1_lab1.TabIndex = 0;
            Panel1_lab1.Text = "TOKATS";
            Panel1_lab1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(TPanel2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 150);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(1682, 903);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.BackgroundImageLayout = ImageLayout.Stretch;
            panel3.Controls.Add(Panel3_Panel3);
            panel3.Controls.Add(Panel3_Panel2);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(Panel3_Panel1);
            panel3.Controls.Add(Panel3_Lab2);
            panel3.Controls.Add(Panel3_Lab1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(591, 10);
            panel3.Margin = new Padding(10);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(50, 50, 50, 100);
            panel3.Size = new Size(480, 630);
            panel3.TabIndex = 0;
            // 
            // Panel3_Panel3
            // 
            Panel3_Panel3.Controls.Add(Panel3_Panel3_But1);
            Panel3_Panel3.Dock = DockStyle.Fill;
            Panel3_Panel3.Location = new Point(50, 380);
            Panel3_Panel3.Name = "Panel3_Panel3";
            Panel3_Panel3.Size = new Size(380, 150);
            Panel3_Panel3.TabIndex = 5;
            // 
            // Panel3_Panel3_But1
            // 
            Panel3_Panel3_But1.BackColor = Color.FromArgb(22, 66, 60);
            Panel3_Panel3_But1.Dock = DockStyle.Bottom;
            Panel3_Panel3_But1.FlatStyle = FlatStyle.Flat;
            Panel3_Panel3_But1.Font = new Font("メイリオ", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Panel3_Panel3_But1.ForeColor = Color.White;
            Panel3_Panel3_But1.Location = new Point(0, 85);
            Panel3_Panel3_But1.Name = "Panel3_Panel3_But1";
            Panel3_Panel3_But1.Size = new Size(380, 65);
            Panel3_Panel3_But1.TabIndex = 0;
            Panel3_Panel3_But1.Text = "Login";
            Panel3_Panel3_But1.UseVisualStyleBackColor = false;
            Panel3_Panel3_But1.Click += Panel3_Panel3_But1_Click;
            // 
            // Panel3_Panel2
            // 
            Panel3_Panel2.BackColor = Color.FromArgb(22, 66, 60);
            Panel3_Panel2.Controls.Add(Panel3_Panel2_Text1);
            Panel3_Panel2.Dock = DockStyle.Top;
            Panel3_Panel2.Location = new Point(50, 330);
            Panel3_Panel2.Name = "Panel3_Panel2";
            Panel3_Panel2.Padding = new Padding(1);
            Panel3_Panel2.Size = new Size(380, 50);
            Panel3_Panel2.TabIndex = 4;
            // 
            // Panel3_Panel2_Text1
            // 
            Panel3_Panel2_Text1.BorderStyle = BorderStyle.None;
            Panel3_Panel2_Text1.Dock = DockStyle.Fill;
            Panel3_Panel2_Text1.Font = new Font("メイリオ", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Panel3_Panel2_Text1.Location = new Point(1, 1);
            Panel3_Panel2_Text1.Multiline = true;
            Panel3_Panel2_Text1.Name = "Panel3_Panel2_Text1";
            Panel3_Panel2_Text1.Size = new Size(378, 48);
            Panel3_Panel2_Text1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("メイリオ", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(50, 250);
            label1.Name = "label1";
            label1.Size = new Size(380, 80);
            label1.TabIndex = 3;
            label1.Text = "PassWord";
            label1.TextAlign = ContentAlignment.BottomLeft;
            // 
            // Panel3_Panel1
            // 
            Panel3_Panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Panel3_Panel1.BackColor = Color.FromArgb(22, 66, 60);
            Panel3_Panel1.Controls.Add(Panel3_Panel1_Text1);
            Panel3_Panel1.Dock = DockStyle.Top;
            Panel3_Panel1.Location = new Point(50, 200);
            Panel3_Panel1.Name = "Panel3_Panel1";
            Panel3_Panel1.Padding = new Padding(1);
            Panel3_Panel1.Size = new Size(380, 50);
            Panel3_Panel1.TabIndex = 2;
            // 
            // Panel3_Panel1_Text1
            // 
            Panel3_Panel1_Text1.BorderStyle = BorderStyle.None;
            Panel3_Panel1_Text1.Dock = DockStyle.Fill;
            Panel3_Panel1_Text1.Font = new Font("メイリオ", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Panel3_Panel1_Text1.Location = new Point(1, 1);
            Panel3_Panel1_Text1.Multiline = true;
            Panel3_Panel1_Text1.Name = "Panel3_Panel1_Text1";
            Panel3_Panel1_Text1.Size = new Size(378, 48);
            Panel3_Panel1_Text1.TabIndex = 0;
            // 
            // Panel3_Lab2
            // 
            Panel3_Lab2.Dock = DockStyle.Top;
            Panel3_Lab2.Font = new Font("メイリオ", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Panel3_Lab2.Location = new Point(50, 140);
            Panel3_Lab2.Name = "Panel3_Lab2";
            Panel3_Lab2.Size = new Size(380, 60);
            Panel3_Lab2.TabIndex = 1;
            Panel3_Lab2.Text = "ID";
            Panel3_Lab2.TextAlign = ContentAlignment.BottomLeft;
            // 
            // Panel3_Lab1
            // 
            Panel3_Lab1.Dock = DockStyle.Top;
            Panel3_Lab1.Font = new Font("メイリオ", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Panel3_Lab1.Location = new Point(50, 50);
            Panel3_Lab1.Name = "Panel3_Lab1";
            Panel3_Lab1.Size = new Size(380, 90);
            Panel3_Lab1.TabIndex = 0;
            Panel3_Lab1.Text = "ログイン";
            Panel3_Lab1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TPanel2
            // 
            TPanel2.ColumnCount = 3;
            TPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            TPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TPanel2.Controls.Add(panel3, 1, 0);
            TPanel2.Dock = DockStyle.Fill;
            TPanel2.Location = new Point(10, 10);
            TPanel2.Name = "TPanel2";
            TPanel2.RowCount = 2;
            TPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 650F));
            TPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TPanel2.Size = new Size(1662, 883);
            TPanel2.TabIndex = 1;
            // 
            // A1010101
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            BackColor = Color.White;
            ClientSize = new Size(1682, 1053);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Cursor = Cursors.AppStarting;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "A1010101";
            StartPosition = FormStartPosition.Manual;
            Text = "A1010101";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            Panel3_Panel3.ResumeLayout(false);
            Panel3_Panel2.ResumeLayout(false);
            Panel3_Panel2.PerformLayout();
            Panel3_Panel1.ResumeLayout(false);
            Panel3_Panel1.PerformLayout();
            TPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label Panel1_lab1;
        private Panel panel2;
        private Panel panel3;
        private Label Panel3_Lab1;
        private Panel Panel3_Panel1;
        private Label Panel3_Lab2;
        private Label label1;
        private TextBox Panel3_Panel1_Text1;
        private Panel Panel3_Panel2;
        private TextBox Panel3_Panel2_Text1;
        private Panel Panel3_Panel3;
        private Button Panel3_Panel3_But1;
        private TableLayoutPanel TPanel2;
    }
}