namespace Snake_v1._2
{
    partial class Setup
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
            this.Start = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.Rank = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Title)).BeginInit();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.Color.Transparent;
            this.Start.BackgroundImage = global::Snake_v1._2.Properties.Resources.Button;
            this.Start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Start.FlatAppearance.BorderSize = 0;
            this.Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Start.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Start.ForeColor = System.Drawing.Color.White;
            this.Start.Location = new System.Drawing.Point(176, 202);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(458, 66);
            this.Start.TabIndex = 1;
            this.Start.Text = "开始游戏";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.OnStart);
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.Transparent;
            this.Exit.BackgroundImage = global::Snake_v1._2.Properties.Resources.Button;
            this.Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Exit.ForeColor = System.Drawing.Color.White;
            this.Exit.Location = new System.Drawing.Point(176, 423);
            this.Exit.Name = "Exit";
            this.Exit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Exit.Size = new System.Drawing.Size(458, 66);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "退出游戏";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.onExit);
            // 
            // Rank
            // 
            this.Rank.BackColor = System.Drawing.Color.Transparent;
            this.Rank.BackgroundImage = global::Snake_v1._2.Properties.Resources.Button;
            this.Rank.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rank.FlatAppearance.BorderSize = 0;
            this.Rank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Rank.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Rank.ForeColor = System.Drawing.Color.White;
            this.Rank.Location = new System.Drawing.Point(176, 310);
            this.Rank.Name = "Rank";
            this.Rank.Size = new System.Drawing.Size(458, 66);
            this.Rank.TabIndex = 2;
            this.Rank.Text = "排行榜";
            this.Rank.UseVisualStyleBackColor = false;
            this.Rank.Click += new System.EventHandler(this.OnRankClick);
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Location = new System.Drawing.Point(176, 33);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(458, 88);
            this.Title.TabIndex = 4;
            this.Title.TabStop = false;
            this.Title.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaintTitle);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Snake_v1._2.Properties.Resources.BK;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 541);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Rank);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Setup";
            this.Text = "Setup";
            ((System.ComponentModel.ISupportInitialize)(this.Title)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Rank;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.PictureBox Title;
    }
}