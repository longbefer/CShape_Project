using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_v1._2
{
    class Body
    {
        public Body() { location = new Point(0, 0); color = Brushes.Gold; }
        public Body(Point point) { location = point; color = Brushes.Gold; }
        public Point location;
        public Brush color;
        //public Image image;
    }

    class Snake//小蛇的操作实现
    {
        //数据成员
        public int life;//生命值
        public List<Body> Body;//身体
        public int size = 10;//蛇身体大小
        public Map map;

        public double speed = 2.0;
        public Direction SnakeDirection { set; get; }
        //方法
        /// <summary>
        /// 构造函数
        /// </summary>
        public Snake()
        {
            life = 3;
            Body = new List<Body>();
            Body.Add(new Body());
            SnakeDirection = Direction.Right;
        }
        /// <summary>
        /// 设置蛇的位置
        /// </summary>
        /// <param name="head">蛇头位置</param>
        public void SetHead(Point head)
        {
            //清除原有Snake的身体
            if (Body.Count != 0)
                Body.Clear();
            Body.Add(new Body(head));
            Body.Add(new Body(new Point(head.X - 1, head.Y)));//默认三节
            Body.Add(new Body(new Point(head.X - 2, head.Y)));
            Body[0].color = Brushes.Green;//将头部默认为绿色
            SnakeDirection = Direction.Right;
        }
        /// <summary>
        /// 返回蛇的位置
        /// </summary>
        public Point Location { get => Body[0].location; }
        /// <summary>
        /// 判断小蛇是否死亡
        /// </summary>
        /// <returns>返回小蛇是否死亡</returns>
        public bool Death()
        {
            return (life <= 0);
        }

        /// <summary>
        /// 移动小蛇
        /// </summary>
        public void Move()
        {
            if (!HitWall())
            {
                for (int i = Body.Count - 1; i > 0; i--)//将每一节身体移向上一节身体
                    Body[i].location = Body[i - 1].location;
                switch (SnakeDirection)
                {
                    case Direction.UP: Body[0].location.Y--; break;
                    case Direction.Down: Body[0].location.Y++; break;
                    case Direction.Left: Body[0].location.X--; break;
                    case Direction.Right: Body[0].location.X++; break;
                }
            }
        }
        /// <summary>
        /// 绘制小蛇
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public bool DrawSnake(Graphics g)
        {
            for (int i = Body.Count - 1; i >= 0; i--)
                g.FillEllipse(Body[i].color, Body[i].location.X * size, Body[i].location.Y * size, size, size);
                //g.FillRectangle(Body[i].color, Body[i].location.X * size, Body[i].location.Y * size, size, size);
            return true;
        }

        /// <summary>
        /// 判断蛇是否碰到了墙壁
        /// </summary>
        /// <returns>是否撞壁</returns>
        private bool HitWall()
        {
            //x不能等于0不可以从出口动，因为头从0开始
            if (Location.X < 0 || Location.Y < 0 ||
                Location.X > Map.column + 1 || Location.Y > Map.row + 1)
                return true;
            //需要添加防止蛇跑向起点,《==这个起点默认在x=0处才生效吧，这里需要改进
            if (SnakeDirection == Direction.Left && Location == map.Start)
                return true;
            switch (SnakeDirection)
            {
                case Direction.Down: if (map.maze[Location.X][Location.Y + 1] == Map.wall) return true; break;
                case Direction.UP: if (map.maze[Location.X][Location.Y - 1] == Map.wall) return true; break;
                case Direction.Left: if (map.maze[Location.X - 1][Location.Y] == Map.wall) return true; break;
                case Direction.Right: if (map.maze[Location.X + 1][Location.Y] == Map.wall) return true; break;
            }
            return false;
        }
    }
}
