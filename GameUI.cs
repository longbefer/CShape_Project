using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_v1._2
{
    delegate void PropsEvent(Things things);
    public partial class GameUI : Form
    {
        //翻译文件
        readonly ResourceManager langRec = new ResourceManager("Snake_v1._2.Language", typeof(GameUI).Assembly);
        //资源列表
        private readonly Color[] map_color = { Color.OrangeRed, Color.DarkOrange, Color.Chocolate, Color.Brown };
        private readonly Image[] bk_image = { Properties.Resources.CN,Properties.Resources.AJ,
            Properties.Resources.IN, Properties.Resources.BL};
        private readonly SoundPlayer bk_music = new System.Media.SoundPlayer(Properties.Resources.music_position);
        //设置资源
        private int current_color = 0;//改变地图颜色
        private int pic_num = 0;//改变bk_image

        private int system_time = 0;//系统时间，总共时间
        private int game_time = 0;//每个游戏时间
        private bool is_pause = false;//是否暂停
        private bool is_hell_mode = false;//为地狱模式
        private Random random;//所有的随机函数

        //游戏资源
        private Map map;//地图
        private Hunter hunter;//猎人
        private Snake snake;//蛇
        private Props props;//道具
        //private PropsEvent Event;//道具事件

        //设置游戏资源
        public int column = 21, row = 21;//横行和纵行
        private int map_show_time = 0, hunter_frozen_time = 0;//地图显示，猎人冰冻（道具）
        private double hunter_speed = 2.0, snake_speed = 2.0;//必须有个保存speed的值
        private int props_producted_times = 5, props_area = 5;//道具的保存的值
        private double hunter_start_min = 10, hunter_start_max = 15;//猎人开始时间上下限
        private int hunter_search_area = 5;
        private int add_life_times = 0;//用于过关加心的判断
        private string tip_str = Properties.Resources.help_snake_go_to_the_end;

        public static int game_money = 0;//金币
        public static int game_level = 1;//等级

        public GameUI()
        {
            InitializeComponent();
            CenterToScreen();
            InitGame();
            InitGameComponent();
        }

        private void InitGameComponent()
        {
            //在这里更改游戏系统界面
            this.game_pic.BackgroundImage = bk_image[pic_num];

            //游戏设计
            //游戏时间设计
            hunter_timer.Interval = (int)(1000.0 / hunter.speed);
            snake_timer.Interval = (int)(1000.0 / snake.speed);

            //游戏界面设计
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ClientSize = new Size((column + 2) * map.size, (row + 10) * map.size);
            this.game_pic.ClientSize = new Size((column + 2) * map.size, (row + 10) * map.size);


        }
        private void InitGame()
        {
            //在这里初始化游戏必要组件
            random = new Random();
            game_time = 0;
            pic_num = 0;
            current_color = 0;
            bk_music.PlayLooping();
            system_time = 0;
            game_time = 0;
            is_pause = false;
            map_show_time = 0;
            hunter_frozen_time = 0;

            //地图组件
            map = new Map(column,row);
            map.is_show_way = false;
            map.Start = new Point(0, random.Next(1, row));
            map.MapColor = map_color[current_color];//过关需更新
            //map.End = new Point(column + 1, random.Next(1, row));
            map.CreateMaze();

            //蛇的组件
            snake = new Snake();
            snake.map = map;//过关需更新，通过地图判断小蛇是否碰壁
            snake.speed = snake_speed;
            snake.SetHead(map.Start);//过关需要更新

            //猎人组件
            hunter = new Hunter();
            hunter.map = map;//过关需要更新，通过地图来寻找小蛇位置
            hunter.speed = hunter_speed;
            hunter.is_frozen = false;
            hunter.current_time = 0;
            hunter.location = map.Start;
            hunter.vision_area = hunter_search_area;//猎人搜索范围
            hunter.star_times = random.Next((int)hunter_start_min, (int)hunter_start_max);
            //hunter.hunter_color = new SolidBrush(map_color[current_color]);
            //hunter.snake_location = snake.Location;//每次需要更新

            //道具组件
            props = new Props();
            props.map = map;//过关需要更新，通过地图来确认道具位置
            props.show_time = 0;
            props.current_time = 0;
            props.product_time = props_producted_times;//道具每次产生时间
            props.area = props_area;//道具每次产生离蛇范围，越大道具离蛇的范围将会扩大
            props.Clear();
            //PropsEvent
            props.Effect += Effective;
            props.Effect += DrawPropsDis;

            //时间开启
            TimerStart();

            //文字组件
            game_money = 0;
            game_level = 1;
        }

        private void NextLevel()//下一关
        {
            //初始化系统资源
            game_time = 0;//单个游戏时间
            current_color++;//变换颜色
            current_color %= map_color.Count();
            pic_num++;//变换背景
            pic_num %= bk_image.Count();

            map_show_time = 0;
            hunter_frozen_time = 0;

            //重新初始化地图
            map.is_show_way = false;
            map.Start = new Point(0, random.Next(1, row));
            //map.End = new Point(column + 1, random.Next(1, row));
            map.MapColor = map_color[current_color];
            map.CreateMaze();

            //重新初始化蛇
            snake.map = map;
            snake.speed += 0.3;//过关时速度微调
            snake.SetHead(map.Start);
            if (game_level == (int)Math.Pow(add_life_times, 2) + add_life_times + 2)
            {//x^2+x+2来增加心
                add_life_times++;
                if (snake.life < 3)
                    snake.life++;
            }

            //重新初始化猎人
            hunter.map = map;
            hunter.speed += 0.3;//过关时速度微调
            hunter.is_frozen = false;
            hunter.current_time = 0;
            hunter.location = map.Start;//猎人起点
            hunter.vision_area = hunter_search_area;//猎人搜索范围
            hunter.star_times = random.Next((int)hunter_start_min, (int)hunter_start_max);
            //对hunter_start_min随等级增大而减少
            if (hunter_start_min > 5)
                hunter_start_min-=0.3;
            if (hunter_start_max > 7)
                hunter_start_max-=0.3;

            //重新初始化道具
            props.map = map;
            props.show_time = 0;//过关showtime为0，gametime亦为0
            props.current_time = 0;
            props.product_time = props_producted_times;//道具每次产生时间
            props.area = props_area;//道具每次产生离蛇范围，越大道具离蛇的范围将会扩大
            props.Clear();

            //重新初始化文字
            game_money += 100;
            game_level++;
            tip_str = Properties.Resources.help_snake_go_to_the_end;

            InitGameComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Setup setup = new Setup();
            setup.Show();
            //Application.ExitThread();//退出游戏
        }

        private void OnSnake(object sender, EventArgs e)
        {
            snake.Move();//小蛇移动速度
        }

        private void OnHunter(object sender, EventArgs e)
        {
            //hunter.Move(snake);//猎人移动速度
            hunter.PuzzledMove(snake);
        }
        /// <summary>
        /// 按键敲击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGameKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down: snake.SnakeDirection = Direction.Down; break;
                case Keys.Up: snake.SnakeDirection = Direction.UP; break;
                case Keys.Left: snake.SnakeDirection = Direction.Left; break;
                case Keys.Right: snake.SnakeDirection = Direction.Right; break;
                case Keys.Enter: if (snake.Death()) { InitGame(); InitGameComponent(); } break;
                case Keys.Space: if (is_pause) { TimerStart();is_pause = false; } 
                                else { is_pause = true; TimerStop();draw_timer.Start(); } break;
                case Keys.Escape: this.Close();bk_music.Stop(); break;
            }
        }
        /// <summary>
        /// 判断过关条件，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDraw(object sender, EventArgs e)
        {
            this.Focus();
            game_pic.Invalidate();//将调用GamePaint()

            //道具产生
            props.Producted(snake);//到时间道具产生
            props.Eat(snake);//判断是否吃到食物

            //处理判断结束条件
            if (hunter.Catched(snake))//检查是否被猎人捉到
                HandleCatched();//处理被捉到的情况
            if (snake.Location == map.End)//是否过关
                NextLevel();//过关
            if (hunter.is_frozen && hunter.current_time - hunter_frozen_time >= hunter.star_times)
                hunter.is_frozen = false;//冻住的猎人解冻
            if (map.is_show_way && game_time - map_show_time >= 4)//是否接到路径道具
                map.is_show_way = false;//显示路径消失
            else map.Connected(snake.Location, map.End);//吃到寻路道具时，每次自动寻路
        }
        /// <summary>
        /// 绘制各种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GamePaint(object sender, PaintEventArgs e)
        {
            //可使用委托graphics foreach()
            Graphics graphics = e.Graphics;
            if (!snake.Death())
            {
                if (!is_pause)
                {
                    map.Draw(graphics);
                    snake.Draw(graphics);
                    hunter.Draw(graphics);
                    props.Draw(graphics);
                    graphics.DrawString(tip_str, this.Font, Brushes.Black, 0, (column + 3) * 10);
                }
                else GamePauseUI(graphics);
            }
            else GameOver(graphics);
            DrawLabel();
        }
        /// <summary>
        /// 处理被猎人捉到的情况
        /// </summary>
        private void HandleCatched()
        {
            TimerStop();
            hunter.is_frozen = true;
            map.show_map = false;
            //is_pause = true;
            if (MessageBox.Show(this, langRec.GetString("hunter_catch_tip"), langRec.GetString("tip_app"), MessageBoxButtons.OK, 0) == DialogResult.OK)
            {
                snake.life--;
                hunter_frozen_time = game_time;
                map.show_map = true;
                if (is_hell_mode)//地狱模式，即返回原点
                {
                    snake.SetHead(map.Start);
                    hunter.location = map.Start;
                }
                TimerStart();
            }
        }
        /// <summary>
        /// 判定游戏结束
        /// </summary>
        /// <param name="g"></param>
        private void GameOver(Graphics g)
        {
            Font font = new Font(this.Font, FontStyle.Bold);
            g.DrawString(langRec.GetString("game_over_app"), font, Brushes.Red, new Point((int)(row * 4.0), 40));
            g.DrawString(langRec.GetString("enter_restart_app"), font, Brushes.Red, new Point((int)(row * 3.5), 60));
            g.DrawString(langRec.GetString("esc_exit_app"), font, Brushes.Red, new Point((int)(row * 3.5), 80));
            TimerStop();
            //文件写入操作
            WriteFile();
        }
        /// <summary>
        /// 游戏暂停时界面
        /// </summary>
        /// <param name="g"></param>
        private void GamePauseUI(Graphics g)
        {
            g.DrawString(langRec.GetString("pause_app"), this.Font, Brushes.Red, new Point((int)(row * 4.5), 40));
            g.DrawString(langRec.GetString("blank_to_start_app"), this.Font, Brushes.Black, new Point((int)(row * 3.5), 60));
        }

        /// <summary>
        /// 绘制标签栏的游戏进度
        /// </summary>
        private void DrawLabel()
        {
            //string heart = "生命值： ";
            //for (int i = 0; i < snake.life; i++)
            //    heart += "♥";//除了重新添加一个，如何改变成红色？
            //this.life_label.Text = heart;
            this.life_label.Invalidate();
            this.money_label.Text = langRec.GetString("gold_props") + game_money;
            this.time_label.Text = langRec.GetString("time_props") + system_time;
            this.level_label.Text = langRec.GetString("level_props") + game_level;
        }
        /// <summary>
        /// 绘制生命值部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLifeDraw(object sender, PaintEventArgs e)
        {
            life_label.Text = "                ";
            string str = langRec.GetString("life_props");
            string heart = "        ";
            for (int i = 0; i < snake.life; i++)
                heart += "♥";
            Point point = new Point(life_label.Padding.Left, life_label.Padding.Top);
            TextRenderer.DrawText(e.Graphics, str, life_label.Font, point, Color.Black);
            TextRenderer.DrawText(e.Graphics, heart, life_label.Font, point, Color.Red);
        }

        /// <summary>
        /// 绘制道具提示
        /// </summary>
        /// <param name="things"></param>
        private void DrawPropsDis(Things things)
        {
            switch (things)
            {
                case Things.Bomb: tip_str = Properties.Resources.things_bomb; break;
                case Things.Find: tip_str = Properties.Resources.things_findway; break;
                case Things.Frozen: tip_str = Properties.Resources.things_frozen; break;
                case Things.Gold: tip_str = Properties.Resources.things_gold; break;
                default: tip_str = Properties.Resources.help_snake_go_to_the_end; break;
            }
        }

        /// <summary>
        /// 道具效果
        /// </summary>
        /// <param name="things"></param>
        private void Effective(Things things)
        {
            switch (things)
            {
                case Things.Bomb: hunter.location = map.Start; break;
                case Things.Find: map.is_show_way = true; map_show_time = game_time; break;
                case Things.Frozen: if (hunter.IsStart()) { hunter.is_frozen = true; hunter_frozen_time = game_time; } break;
                case Things.Gold: game_money += 100; break;
            }
        }
        /// <summary>
        /// 全部时间暂停
        /// </summary>
        private void TimerStop()
        {
            system_timer.Stop();
            game_timer.Stop();
            snake_timer.Stop();
            hunter_timer.Stop();
            draw_timer.Stop();
        }
        /// <summary>
        /// 全部时间开启
        /// </summary>
        private void TimerStart()
        {
            system_timer.Start();
            game_timer.Start();
            snake_timer.Start();
            hunter_timer.Start();
            draw_timer.Start();
        }
        /// <summary>
        /// 写文件操作
        /// </summary>
        private void WriteFile()
        {
            if (!File.Exists("..\\..\\Resource\\User.txt"))
                File.WriteAllText("..\\..\\Resource\\User.txt", "难度  等级  用时\n");
            
            string file = "";
            file += column == 21 ? "简单" : column == 31 ? "一般" : "困难";
            file += "  ";
            file += game_level;
            file += "      ";
            file += system_time;
            file += '\n';
            //File.AppendAllText("..\\..\\Resource\\User.txt", file);

            //确定行数，只保存最近五行
            int file_length = 1;
            string rec_line = "";//早前记录
            StreamReader sr = new StreamReader("..\\..\\Resource\\User.txt");
            string file_line = sr.ReadLine();
            if (file_line.Equals("难度  等级  用时"))
                file_line = sr.ReadLine();
            while (file_line != null && file_length < 5)
            {
                file_length++;
                rec_line += (file_line + "\n");
                file_line = sr.ReadLine();
            }
            sr.Close();
            file += rec_line;
            string total_file = "难度  等级  用时\n";
            total_file += file;
            File.WriteAllText("..\\..\\Resource\\User.txt", total_file);
        }
    }
}
