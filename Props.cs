using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_v1._2
{
    public enum Things { Bomb, Find, Gold, Frozen };//四类道具

    class Prop//单个道具
    {
        public int size = 10;//道具大小
        public Things thing;//道具类型
        public static int duration = 10;//道具持续时间
        public int show_times;//道具出现时间，需要在产生道具时将此时的游戏时间赋值给他
        public Point location;//道具产生位置
        public int area = 5;//道具产生范围，必须为正数且保持在【3，无穷】较好
        //public Map map;//获取地图

        public Prop()
        {
            Random random = new Random();
            thing = (Things)random.Next(0, 4);
        }
        /// <summary>
        /// 设置道具的位置，需要一张地图，避免生成在墙上和一个点，生成在附近的点
        /// </summary>
        /// <param name="map"></param>
        /// <param name="location"></param>
        public void SetLocation(Map map, Point location)
        {
            if (map == null)
                throw new Exception("No map, Please Init");
            this.location = RandomLocation(map, location);
        }
        /// <summary>
        /// 记得初始化area的值，此为随机生成一个在area附近的点
        /// </summary>
        /// <param name="map"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public Point RandomLocation(Map map, Point location)
        {
            if(map==null)
                throw new Exception("No map, Please Init");
            int x, y;
            int loop_times = 0;
            Random random = new Random();
            do
            {
                x = location.X + random.Next(-area, area);
                y = location.Y + random.Next(-area, area);
                if (loop_times >= 100)//固定一个循环次数，减少时间，防止无限循环
                    this.location = location;//当范围在0时可能引起的问题，或者-1，1时概率性出现的问题
            } while (x <= 0 || y <= 0 || x >= Map.column || y >= Map.row ||
                        map.maze[x][y] == Map.wall || location == new Point(x, y));
            return new Point(x, y);
        }
        /// <summary>
        /// 道具是否到时间该消失了
        /// </summary>
        /// <param name="current_time">将当前时间传入</param>
        /// <returns>返回是否该消失了</returns>
        public bool Display(int current_time)
        {
            if (current_time - show_times <= duration)
                return true;
            else return false;
        }
        /// <summary>
        /// 道具颜色
        /// </summary>
        /// <returns></returns>
        public Brush GetPropColor()
        {
            switch (thing)
            {
                case Things.Bomb: return Brushes.Black;
                case Things.Find: return Brushes.Red;
                case Things.Frozen: return Brushes.DeepSkyBlue;
                case Things.Gold: return Brushes.Goldenrod;
                default: return Brushes.White;
            }
        }
    }
    //道具类，我需要每个道具都有一个位置，当一个道具由于时间到了或者小蛇吃到了
     //则消失，每个道具需要有不同的作用，需要显示在地图上的find()如何实现？
      //bomb()是将猎人送回原点，并冻住一定时间，gold()需实现添加金币，金币必须私有不可修改
      //frozen()猎人原地冻结，
      //道具出现的地点（一开局随机产生几个道具，每个道具到一定时间消失）
      //之后会再次间断一定时间产生
    class Props:IDraw
    {
        //字段
        public PropsEvent Effect;
        private readonly List<Prop> props;
        public int current_time;//需要与gametime同步
        public int show_time = 0;
        public int product_time = 5;//产生道具的时间间隔
        public Map map;
        public int area = 5;//道具产生在蛇附近的范围，必须为正数
        //方法
        public Props()
        {
            props = new List<Prop>();
        }
        /// <summary>
        /// 产生道具
        /// </summary>
        /// <param name="snake">传入小蛇，因为是在小蛇附近产生</param>
        /// <returns></returns>
        public bool Producted(Snake snake)
        {
            if (map == null)
                throw new Exception("No map, Please init");
            if (current_time - show_time >= product_time)//product_time秒一生产
            {
                show_time = current_time;
                Prop prop = new Prop();
                do
                {
                    prop.area = this.area;//越大，产生的道具的范围离蛇变大
                    prop.SetLocation(map, snake.Location);
                    prop.show_times = current_time;
                } while (IsPosition(prop));//若在同一位置则重新生成
                props.Add(prop);
            }
            return true;
        }
        /// <summary>
        /// 绘制道具
        /// </summary>
        /// <param name="g"></param>
        public bool Draw(Graphics g)
        {
            for (int i = 0; i < props.Count; i++)
                if (props[i].Display(current_time))//是否要显示
                    g.FillEllipse(props[i].GetPropColor(),
                        props[i].location.X * props[i].size,
                        props[i].location.Y * props[i].size,
                        props[i].size, props[i].size);//建议将此部分放入Prop中
                else props.RemoveAt(i);
            return true;
        }
        /// <summary>
        /// 判断小蛇是否吃到了道具
        /// </summary>
        /// <param name="snake">小蛇</param>
        public bool Eat(Snake snake)
        {
            for(int i = 0; i < props.Count; i++)
                if (props[i].location == snake.Location)
                {
                    //判断是哪种道具
                    Effect(props[i].thing);
                    props.RemoveAt(i);
                    return true;
                }
            return false;
        }
        /// <summary>
        /// 每次过关时清空props
        /// </summary>
        public void Clear()
        {
            props.Clear();
        }
        /// <summary>
        /// 判断此位置上是否已存在一个道具
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private bool IsPosition(Prop prop)//去重
        {
            for (int i = 0; i < props.Count; i++)
                if (prop.location == props[i].location)
                    return true;
            return false;
        }
    }
}
