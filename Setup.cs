using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_v1._2
{
    public partial class Setup : Form
    {
        private Point mouseDown;//鼠标移动位置变量
        private bool leftFlag;//是否为左键
        public Setup()
        {
            InitializeComponent();
            CenterToScreen();
        }
        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = new Point(-e.X, -e.Y);
                leftFlag = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseDown.X, mouseDown.Y);
                Location = mouseSet;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (leftFlag) leftFlag = false;
        }

        private void onExit(object sender, EventArgs e)//退出提示
        {
            if (MessageBox.Show("确认退出游戏？", "游戏提示", MessageBoxButtons.YesNo, 0) == DialogResult.Yes)
                Application.ExitThread();//退出游戏
        }

        private void OnStart(object sender, EventArgs e)//开始游戏
        {
            GameUI game = new GameUI();//运行游戏界面，可以适当添加选择难度
            game.Show();
            //this.Hide();
            this.Close();//关闭setup窗口
        }

        private void OnPaintTitle(object sender, PaintEventArgs e)
        {
            FontFamily fontFamily = new FontFamily("楷体");
            Font font = new Font(fontFamily, 20.0f, FontStyle.Bold);
            e.Graphics.DrawString("瑞奇的逃亡之旅", font, Brushes.Black, 50, 30);
        }

        private void OnRankClick(object sender, EventArgs e)
        {
            //排行榜
            if (File.Exists("..\\..\\Resource\\User.txt"))
                MessageBox.Show(this, File.ReadAllText("..\\..\\Resource\\User.txt"), "排行榜", MessageBoxButtons.OK, 0);
            else MessageBox.Show(this, "还没有排行榜呢！快去试试吧。", "排行榜", MessageBoxButtons.OK, 0);
        }
    }
}
