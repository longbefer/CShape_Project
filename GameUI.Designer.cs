using System;
using System.Windows.Forms;

namespace Snake_v1._2
{
    partial class GameUI
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menu_game = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_start = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_level = new System.Windows.Forms.ToolStripMenuItem();
            this.level_easy = new System.Windows.Forms.ToolStripMenuItem();
            this.level_usually = new System.Windows.Forms.ToolStripMenuItem();
            this.level_different = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_setting = new System.Windows.Forms.ToolStripMenuItem();
            this.setting_game = new System.Windows.Forms.ToolStripMenuItem();
            this.setting_hell = new System.Windows.Forms.ToolStripMenuItem();
            this.setting_music = new System.Windows.Forms.ToolStripMenuItem();
            this.setting_backgroundmusic = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_member = new System.Windows.Forms.ToolStripMenuItem();
            this.game_pic = new System.Windows.Forms.PictureBox();
            this.life_label = new System.Windows.Forms.Label();
            this.money_label = new System.Windows.Forms.Label();
            this.level_label = new System.Windows.Forms.Label();
            this.time_label = new System.Windows.Forms.Label();
            this.system_timer = new System.Windows.Forms.Timer(this.components);
            this.game_timer = new System.Windows.Forms.Timer(this.components);
            this.snake_timer = new System.Windows.Forms.Timer(this.components);
            this.hunter_timer = new System.Windows.Forms.Timer(this.components);
            this.draw_timer = new System.Windows.Forms.Timer(this.components);
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.game_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.White;
            this.menu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_game,
            this.menu_about});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 32);
            this.menu.TabIndex = 0;
            this.menu.Text = "菜单栏";
            this.menu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            this.menu.MouseLeave += new System.EventHandler(this.OnMenuMouseLeave);
            // 
            // menu_game
            // 
            this.menu_game.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_start,
            this.menu_level,
            this.menu_setting,
            this.menu_exit});
            this.menu_game.Name = "menu_game";
            this.menu_game.Size = new System.Drawing.Size(92, 28);
            this.menu_game.Text = "游戏 (&G)";
            // 
            // menu_start
            // 
            this.menu_start.Name = "menu_start";
            this.menu_start.Size = new System.Drawing.Size(209, 34);
            this.menu_start.Text = "开始游戏 (&S)";
            this.menu_start.Click += new System.EventHandler(this.OnStartClick);
            // 
            // menu_level
            // 
            this.menu_level.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level_easy,
            this.level_usually,
            this.level_different});
            this.menu_level.Name = "menu_level";
            this.menu_level.Size = new System.Drawing.Size(209, 34);
            this.menu_level.Text = "等级 (&L)";
            // 
            // level_easy
            // 
            this.level_easy.Checked = true;
            this.level_easy.CheckOnClick = true;
            this.level_easy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.level_easy.Name = "level_easy";
            this.level_easy.Size = new System.Drawing.Size(177, 34);
            this.level_easy.Text = "简单 (&E)";
            this.level_easy.Click += new System.EventHandler(this.OnLevelEasyClick);
            // 
            // level_usually
            // 
            this.level_usually.CheckOnClick = true;
            this.level_usually.Name = "level_usually";
            this.level_usually.Size = new System.Drawing.Size(177, 34);
            this.level_usually.Text = "一般 (&U)";
            this.level_usually.Click += new System.EventHandler(this.OnLevelUsuallyClick);
            // 
            // level_different
            // 
            this.level_different.CheckOnClick = true;
            this.level_different.Name = "level_different";
            this.level_different.Size = new System.Drawing.Size(177, 34);
            this.level_different.Text = "困难 (&D)";
            this.level_different.Click += new System.EventHandler(this.OnLevelDifferentClick);
            // 
            // menu_setting
            // 
            this.menu_setting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setting_game,
            this.setting_music});
            this.menu_setting.Name = "menu_setting";
            this.menu_setting.Size = new System.Drawing.Size(209, 34);
            this.menu_setting.Text = "设置 (&T)";
            // 
            // setting_game
            // 
            this.setting_game.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setting_hell});
            this.setting_game.Name = "setting_game";
            this.setting_game.Size = new System.Drawing.Size(217, 34);
            this.setting_game.Text = "游戏设置";
            // 
            // setting_hell
            // 
            this.setting_hell.CheckOnClick = true;
            this.setting_hell.Name = "setting_hell";
            this.setting_hell.Size = new System.Drawing.Size(213, 34);
            this.setting_hell.Text = "地狱模式 (&H)";
            this.setting_hell.Click += new System.EventHandler(this.OnHellModleClick);
            // 
            // setting_music
            // 
            this.setting_music.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setting_backgroundmusic});
            this.setting_music.Name = "setting_music";
            this.setting_music.Size = new System.Drawing.Size(217, 34);
            this.setting_music.Text = "音效设置 (&M)";
            // 
            // setting_backgroundmusic
            // 
            this.setting_backgroundmusic.Checked = true;
            this.setting_backgroundmusic.CheckOnClick = true;
            this.setting_backgroundmusic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.setting_backgroundmusic.Name = "setting_backgroundmusic";
            this.setting_backgroundmusic.Size = new System.Drawing.Size(210, 34);
            this.setting_backgroundmusic.Text = "背景音效 (&B)";
            this.setting_backgroundmusic.Click += new System.EventHandler(this.OnBKMusicClick);
            // 
            // menu_exit
            // 
            this.menu_exit.Name = "menu_exit";
            this.menu_exit.Size = new System.Drawing.Size(209, 34);
            this.menu_exit.Text = "退出游戏 (&E)";
            this.menu_exit.Click += new System.EventHandler(this.OnExitGameClick);
            // 
            // menu_about
            // 
            this.menu_about.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_member});
            this.menu_about.Name = "menu_about";
            this.menu_about.Size = new System.Drawing.Size(92, 28);
            this.menu_about.Text = "关于 (&A)";
            // 
            // menu_member
            // 
            this.menu_member.Name = "menu_member";
            this.menu_member.Size = new System.Drawing.Size(181, 34);
            this.menu_member.Text = "成员 (&M)";
            this.menu_member.Click += new System.EventHandler(this.OnMemberClick);
            // 
            // game_pic
            // 
            this.game_pic.BackColor = System.Drawing.Color.White;
            this.game_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.game_pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.game_pic.Location = new System.Drawing.Point(0, 68);
            this.game_pic.Name = "game_pic";
            this.game_pic.Size = new System.Drawing.Size(800, 587);
            this.game_pic.TabIndex = 2;
            this.game_pic.TabStop = false;
            this.game_pic.Paint += new System.Windows.Forms.PaintEventHandler(this.GamePaint);
            // 
            // life_label
            // 
            this.life_label.AutoSize = true;
            this.life_label.Location = new System.Drawing.Point(12, 47);
            this.life_label.Name = "life_label";
            this.life_label.Size = new System.Drawing.Size(80, 18);
            this.life_label.TabIndex = 3;
            this.life_label.Text = "生命值：";
            this.life_label.Paint += new System.Windows.Forms.PaintEventHandler(this.OnLifeDraw);
            // 
            // money_label
            // 
            this.money_label.AutoSize = true;
            this.money_label.Location = new System.Drawing.Point(206, 47);
            this.money_label.Name = "money_label";
            this.money_label.Size = new System.Drawing.Size(62, 18);
            this.money_label.TabIndex = 4;
            this.money_label.Text = "金币：";
            // 
            // level_label
            // 
            this.level_label.AutoSize = true;
            this.level_label.Location = new System.Drawing.Point(381, 47);
            this.level_label.Name = "level_label";
            this.level_label.Size = new System.Drawing.Size(62, 18);
            this.level_label.TabIndex = 5;
            this.level_label.Text = "等级：";
            // 
            // time_label
            // 
            this.time_label.AutoSize = true;
            this.time_label.Location = new System.Drawing.Point(541, 47);
            this.time_label.Name = "time_label";
            this.time_label.Size = new System.Drawing.Size(62, 18);
            this.time_label.TabIndex = 6;
            this.time_label.Text = "用时：";
            // 
            // system_timer
            // 
            this.system_timer.Enabled = true;
            this.system_timer.Interval = 1000;
            this.system_timer.Tick += new System.EventHandler(this.OnSystemTimer);
            // 
            // game_timer
            // 
            this.game_timer.Enabled = true;
            this.game_timer.Interval = 1000;
            this.game_timer.Tick += new System.EventHandler(this.OnGameTimer);
            // 
            // snake_timer
            // 
            this.snake_timer.Enabled = true;
            this.snake_timer.Tick += new System.EventHandler(this.OnSnake);
            // 
            // hunter_timer
            // 
            this.hunter_timer.Enabled = true;
            this.hunter_timer.Tick += new System.EventHandler(this.OnHunter);
            // 
            // draw_timer
            // 
            this.draw_timer.Enabled = true;
            this.draw_timer.Interval = 50;
            this.draw_timer.Tick += new System.EventHandler(this.OnDraw);
            // 
            // GameUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 654);
            this.Controls.Add(this.time_label);
            this.Controls.Add(this.level_label);
            this.Controls.Add(this.money_label);
            this.Controls.Add(this.life_label);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.game_pic);
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameUI";
            this.Text = "贪吃蛇—瑞奇的逃亡之旅";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnGameKeyDown);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.game_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void OnMemberClick(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(this, "1720521 李涵\n1720521 范宝权\n1720521 龙博峰", "制作人员");
        }


        private void OnStartClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(langRec.GetString("restart_unsafe_go"), langRec.GetString("tip_app"), MessageBoxButtons.OKCancel, 0) == DialogResult.OK)
            {
                InitGame();
                InitGameComponent();
            }
        }

        private void OnLevelEasyClick(object sender, EventArgs e)
        {
            if (level_easy.Checked &&
                MessageBox.Show(langRec.GetString("restart_unsafe_go"), langRec.GetString("tip_app"), MessageBoxButtons.OKCancel, 0) == DialogResult.OK)
            {
                if (level_different.Checked)
                    level_different.Checked = false;
                if (level_usually.Checked)
                    level_usually.Checked = false;
                level_easy.Checked = true;
                column = 21; row = 21;
                //对猎人的启动时间
                hunter_start_min = 10; hunter_start_max = 15;
                hunter_search_area = 5;
                //对蛇和猎人的速度
                snake_speed = 2.0; hunter_speed = 2.0;
                //对道具的设置
                props_producted_times = 3; props_area = 5;
                InitGame();
                InitGameComponent();
               
            }
            else level_easy.Checked = false;
        }

        private void OnLevelUsuallyClick(object sender, EventArgs e)
        {
            if (level_usually.Checked &&
                MessageBox.Show(langRec.GetString("restart_unsafe_go"), langRec.GetString("tip_app"), MessageBoxButtons.OKCancel, 0) == DialogResult.OK)
            {
                if (level_different.Checked)
                    level_different.Checked = false;
                if (level_easy.Checked)
                    level_easy.Checked = false;
                level_usually.Checked = true;
                column = 31; row = 31;
                //对猎人的启动时间
                hunter_start_min = 7; hunter_start_max = 15;
                hunter_search_area = 8;
                //对蛇和猎人的速度
                snake_speed = 5.0; hunter_speed = 4.0;
                //对道具的设置
                props_producted_times = 5; props_area = 10;
                InitGame();
                InitGameComponent();
                
            }
            else level_usually.Checked = false;
        }

        private void OnLevelDifferentClick(object sender, EventArgs e)
        {
            if (level_different.Checked &&
                MessageBox.Show(langRec.GetString("restart_unsafe_go"), langRec.GetString("tip_app"), MessageBoxButtons.OKCancel, 0) == DialogResult.OK)
            {
                if (level_easy.Checked)
                    level_easy.Checked = false;
                if (level_usually.Checked)
                    level_usually.Checked = false;
                column = 41; row = 41;
                level_different.Checked = true;
                //对猎人的启动时间
                hunter_start_min = 5; hunter_start_max = 10;
                hunter_search_area = 10;
                //对蛇和猎人的速度
                snake_speed = 10.0; hunter_speed = 8.0;
                //对道具的设置
                props_producted_times = 3; props_area = 10;
                InitGame();
                InitGameComponent();
            }
            else level_different.Checked = false;
        }


        private void OnHellModleClick(object sender, EventArgs e)
        {
            is_hell_mode = setting_hell.Checked;
        }



        private void OnBKMusicClick(object sender, EventArgs e)
        {
            if (setting_backgroundmusic.Checked)
                bk_music.PlayLooping();
            else bk_music.Stop();
        }



        private void OnExitGameClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(langRec.GetString("exit_game_go_app"), langRec.GetString("tip_app"), MessageBoxButtons.OKCancel, 0) == DialogResult.OK)
                Close();
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            TimerStop();
        }
        private void OnMenuMouseLeave(object sender, EventArgs e)
        {
            if (!snake.Death())
                TimerStart();
        }

        private void OnGameTimer(object sender, EventArgs e)
        {
            game_time++;
            hunter.current_time = game_time;
            props.current_time = game_time;
        }

        private void OnSystemTimer(object sender, EventArgs e)
        {
            system_time++;
        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menu_game;
        private System.Windows.Forms.ToolStripMenuItem menu_about;
        private System.Windows.Forms.PictureBox game_pic;
        private System.Windows.Forms.Label life_label;
        private System.Windows.Forms.ToolStripMenuItem menu_start;
        private System.Windows.Forms.ToolStripMenuItem menu_level;
        private System.Windows.Forms.ToolStripMenuItem menu_setting;
        private System.Windows.Forms.ToolStripMenuItem setting_game;
        private System.Windows.Forms.ToolStripMenuItem setting_music;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.ToolStripMenuItem menu_member;
        private System.Windows.Forms.Label money_label;
        private System.Windows.Forms.Label level_label;
        private System.Windows.Forms.Label time_label;
        private System.Windows.Forms.ToolStripMenuItem level_easy;
        private System.Windows.Forms.ToolStripMenuItem level_usually;
        private System.Windows.Forms.ToolStripMenuItem level_different;
        private System.Windows.Forms.ToolStripMenuItem setting_hell;
        private System.Windows.Forms.ToolStripMenuItem setting_backgroundmusic;
        private System.Windows.Forms.Timer system_timer;
        private System.Windows.Forms.Timer game_timer;
        private System.Windows.Forms.Timer snake_timer;
        private System.Windows.Forms.Timer hunter_timer;
        private System.Windows.Forms.Timer draw_timer;
    }
}

