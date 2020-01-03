using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_v1._2
{
    class Hunter:Body
    {
        //public Point location;//猎人位置
        //public Point snake_location;//蛇的位置
        public int current_time = 0;
        public int star_times = 15;//猎人开始时间
        //public Brush hunter_color;//猎人颜色
        //public int size = 10;
        public bool is_frozen = false;
        public int vision_area = 10;//猎人可视范围，在范围内则标记

        private Map hunter_map;
        public Map map
        {
            set
            {
                hunter_map = value;
                end_point = hunter_map.Start;
            }
        }

        public double speed = 2.0;//猎人速度
        //private Image hunter_image;

        public Hunter()
        {
            location = new Point(0, 0);
            color = Brushes.Black;
        }
        /// <summary>
        /// 地图，猎人必须有一份地图
        /// </summary>
        

        /// <summary>
        /// 绘制猎人
        /// </summary>
        /// <returns>绘制结果</returns>
        public override bool Draw(Graphics g)
        {
            if (IsStart())//猎人开始行动
            {
                g.FillRectangle(color, location.X * size, location.Y * size, size, size);

                //for test
                g.DrawEllipse(new Pen(Color.Black), (location.X - vision_area) * size + size / 2,
                    (location.Y - vision_area) * size + size / 2,
                    2 * vision_area * size, 2 * vision_area * size);

                //for test，黑点标记猎人即将前往的点，遇到小蛇时将会标记小蛇
                g.FillRectangle(color, end_point.X * size + 2, end_point.Y * size + 2, 5, 5);
            }
            return true;
        }
        /// <summary>
        /// 通过判断小蛇位置移动猎人
        /// </summary>
        /// <param name="snake">小蛇对象</param>
        /// <returns>移动结果</returns>
        public bool Move(Snake snake)
        {
            //可以对移动进行改进，比如，当距离蛇的范围在一定区域时
            //比如以蛇为半径的5的区域内，猎人可以发现小蛇，然后加快速度
            //离小蛇大于5的半径区域内，猎人的速度小于小蛇，且
            //不一定要找到小蛇，取离小蛇附近的一些点，可以比较随机的找寻小蛇
            //比如：
            //Prop prop = new Prop();
            //prop.area = 6;
            //Point end_point = prop.RandomLocation(map, snake.Location);
            //设置一个随机终点，这样猎人不会立刻就找到小蛇

            //然后添加一个判断
            //比如：判断小蛇离猎人的半径小于等于5，则猎人加速找到它
            //if (Math.Sqrt(Math.Pow((location.X - snake.Location.X), 2) - Math.Pow(location.Y - snake.Location.Y, 2)) <= 5)
            //    Connected(location, snake.Location);


            if (hunter_map == null)
                throw new Exception("Please init Map");
            if (IsStart() && !is_frozen)
            {
                Map trace_way = new Map();
                trace_way.SetMapMaze(hunter_map.maze);
                trace_way.Connected(location, snake.Location);

                //for test
                end_point = snake.Location;

                if (trace_way.maze_way[location.X + 1][location.Y] == Map.access)
                    location.X++;
                else if (trace_way.maze_way[location.X - 1][location.Y] == Map.access)
                    location.X--;
                else if (trace_way.maze_way[location.X][location.Y + 1] == Map.access)
                    location.Y++;
                else if (trace_way.maze_way[location.X][location.Y - 1] == Map.access)
                    location.Y--;
            }
            return true;
        }

        
        private Point end_point;//由于下面的寻路计划

        /// <summary>
        /// 不直接寻找小蛇，随机寻找，等距离为vision_area时找到，并快速到达
        /// </summary>
        /// <param name="snake"></param>
        /// <returns></returns>
        public bool PuzzledMove(Snake snake)
        {
            if (hunter_map == null)
                throw new Exception("Please init Map");
            if (IsStart() && !is_frozen)
            {
                bool inVision = InVision(snake.Location);
                if (end_point == location || inVision)
                {//之所以添加end_point==location判断条件，是想要猎人到固定点后再去追踪小蛇
                    //如果不添加end_point==location，猎人每走一步都会判断小蛇是否在可视范围内
                    //不在范围内时，随机产生一个坐标，可能导致在原地不动的情况，
                    //同时也会出现，当小蛇处于可视范围临界值时，可能出现的抖动情况，即在原地来回跳动
                    //那个特别小的黑点即end_point，可以再DrawHunter中修改它
                    if (inVision)
                        end_point = snake.Location;
                    else
                    {
                        Random random = new Random();
                        Prop prop = new Prop();
                        prop.area = random.Next(3, this.vision_area);
                        end_point = prop.RandomLocation(hunter_map, snake.Location);
                    }
                }
                {
                    //追踪小蛇或者随便走走
                    Map trace_way = new Map();
                    trace_way.SetMapMaze(hunter_map.maze);
                    trace_way.Connected(location, end_point);

                    //如果需要，需要判断location是否越界
                    if (trace_way.maze_way[location.X + 1][location.Y] == Map.access)
                        location.X++;
                    else if (trace_way.maze_way[location.X - 1][location.Y] == Map.access)
                        location.X--;
                    else if (trace_way.maze_way[location.X][location.Y + 1] == Map.access)
                        location.Y++;
                    else if (trace_way.maze_way[location.X][location.Y - 1] == Map.access)
                        location.Y--;
                }
            }
            return true;

        }
        /// <summary>
        /// 判断点是否在猎人范围内
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        private bool InVision(Point loc)
        {
            if (Math.Sqrt(Math.Pow((location.X - loc.X), 2) +
                    Math.Pow(location.Y - loc.Y, 2)) <= this.vision_area)
                return true;
            else return false;
        }

        /// <summary>
        /// 判断小蛇是否被捉住
        /// </summary>
        /// <param name="snake"></param>
        /// <returns></returns>
        public bool Catched(Snake snake)
        {
            if (IsStart() && !is_frozen)//若此时被冰冻或者捉住一次，则暂时不能再次捉住小蛇了
                return (location == snake.Location);
            return false;
        }
        /// <summary>
        /// 判断猎人是否已经开始行动，用于对冰冻和移动进行判断
        /// </summary>
        /// <returns></returns>
        public bool IsStart()
        {
            return (current_time >= star_times);
        }
    }
}


            ////存在问题，在临界值时，将会出现抖动的情况
            //if (map == null)
            //    throw new Exception("Please Init map");
            //if (IsStart() && !is_frozen)
            //{
            //    Random random = new Random();
            //    Prop prop = new Prop();
            //    prop.area = random.Next(3, this.vision_area);
            //    Point end_point = prop.RandomLocation(map, snake.Location);
            //    Map trace_way = new Map();
            //    trace_way.SetMapMaze(map.maze);
            //    //解决临界值抖动，可以给临界值添加一个随机抖动《====依旧存在抖动
            //    //添加一旦在可视范围内则可以找到小蛇了
            //    if (Math.Sqrt(Math.Pow((location.X - snake.Location.X), 2) +
            //        Math.Pow(location.Y - snake.Location.Y, 2)) <= this.vision_area + random.Next(0, 4))
            //        trace_way.Connected(location, snake.Location);
            //    else trace_way.Connected(location, end_point);

            //    if (trace_way.maze_way[location.X + 1][location.Y] == Map.access)
            //        location.X++;
            //    else if (trace_way.maze_way[location.X - 1][location.Y] == Map.access)
            //        location.X--;
            //    else if (trace_way.maze_way[location.X][location.Y + 1] == Map.access)
            //        location.Y++;
            //    else if (trace_way.maze_way[location.X][location.Y - 1] == Map.access)
            //        location.Y--;
            //}
            //return true;