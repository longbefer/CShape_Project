using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_v1._2
{
    enum Direction { UP = 8, Down = 1, Left = 4, Right = 2 };
    /// <summary>
    /// Map用于生成随机地图（使用普利姆算法）并给出迷宫结果（使用栈）
    /// </summary>
    class Map
    {
        //特殊宏定义
        public readonly static int MAXSIZE = 100;
        public readonly static int way = 0;//0表示路
        public readonly static int wall = 1;//1表示墙
        public readonly static int access = 2;//2表示通路
        //数据成员（字段）
        private Point _start, _end;//开始点和结束点
        public static int row, column;//横列和纵列，故row代表y，column代表x
        public int size = 10;//地图size
        public bool is_show_way = false;//显示通关路径
        //（属性）
        /// <summary>
        /// 迷宫的开始点，改变迷宫起点。（不建议更改）
        /// </summary>
        public Point Start { set => _start = value; get => _start; }
        /// <summary>
        /// 迷宫的终点，不建议改变迷宫终点。（耗时太大）
        /// </summary>
        public Point End { set => _end = value; get => _end; }
        /// <summary>
        /// 设置地图颜色
        /// </summary>
        public Color MapColor { get; set; }

        //方法
        /// <summary>
        /// 默认构造函数，设置行列均为31，起点为（1，0）点，终点为（31,32)点
        /// </summary>
        public Map()
        {
            //column = 9; row = 9;
            Start = new Point(0, 1);
            End = new Point(column + 1, row);
            MapColor = Color.OrangeRed;
        }
        /// <summary>
        /// 默认构造函数，设置横列和纵列。（横列和纵列需为单数）
        /// </summary>
        /// <param name="row">横列y</param>
        /// <param name="column">纵列x</param>
        public Map(int column, int row)
        {
            if (column >= MAXSIZE || row >= MAXSIZE)
                throw new Exception("Over the max size");
            row = row % 2 == 0 ? row + 1 : row;
            column = column % 2 == 0 ? column + 1 : column;
            Map.column = column; Map.row = row;
            Start = new Point(0, 1);
            End = new Point(column + 1, row);
            MapColor = Color.OrangeRed;
        }
        /// <summary>
        /// 设置地图的横列和纵列。（横列和纵列需为单数）
        /// </summary>
        /// <param name="row">横列y</param>
        /// <param name="column">纵列x</param>
        public static void SetMapSize(int column, int row)
        {
            if (column >= MAXSIZE || row >= MAXSIZE)
                throw new Exception("Over the max size");
            row = row % 2 == 0 ? row + 1 : row;
            column = column % 2 == 0 ? column + 1 : column;
            Map.row = row; Map.column = column;
        }
        /// <summary>
        /// 设置迷宫地图
        /// </summary>
        /// <param name="maze">地图</param>
        public void SetMapMaze(int[][] maze)
        {
            this.maze = maze;
        }


        //创建迷宫来源于：https://blog.csdn.net/qq_38677814/article/details/79745659
        //构建迷宫
        //构建迷宫所需资源
        private List<int> block_row;
        private List<int> block_column;
        private List<Direction> block_direct;
        private int x_num = 1, y_num = 1;
        public int[][] maze;//创建的地图
        /// <summary>
        /// 创建迷宫，将创建一个设定范围的迷宫
        /// </summary>
        /// <returns>返回是否成功创建一个迷宫</returns>
        public bool CreateMaze()//创建迷宫
        {
            //int cycle_times = 0;//循环次数
            do
            {
                InitMaze();
                Random random = new Random();
                Count();
                while (block_row.Count() > 0)
                {
                    int num = block_row.Count();
                    int randnum = random.Next(0, num);
                    x_num = block_column[randnum];
                    y_num = block_row[randnum];
                    switch (block_direct[randnum])
                    {
                        case Direction.Down: y_num++; break;
                        case Direction.Right: x_num++; break;
                        case Direction.Left: x_num--; break;
                        case Direction.UP: y_num--; break;
                    }
                    if (maze[x_num][y_num] == wall)
                    {
                        //只能为单数是因为每次拆墙直接拆两个👇
                        maze[block_column[randnum]][block_row[randnum]] = way;
                        maze[x_num][y_num] = way;
                        Count();
                    }
                    block_row.RemoveAt(randnum);
                    block_column.RemoveAt(randnum);
                    block_direct.RemoveAt(randnum);

                    //Console.WriteLine("\n");
                    //Show(maze);
                }
                maze[Start.X][Start.Y] = way;
                maze[End.X][End.Y] = way;
                //cycle_times++;
                //if (cycle_times > 10)//循环次数大于10次
                //    throw new Exception("Using too many time, Forbiden!!");
            } while (!Connected(Start, End));
            return true;
        }
        /// <summary>
        /// 初始化迷宫的数组及所需的数据变量。
        /// </summary>
        private void InitMaze()
        {
            maze = new int[MAXSIZE][];
            for (int i = 0; i <= column + 1; i++)//row+1包含墙壁2栋墙壁
                maze[i] = new int[MAXSIZE];
            for (int i = 0; i <= column + 1; i++)//x
                for (int j = 0; j <= row + 1; j++)//y
                    maze[i][j] = wall;
            block_row = new List<int>();
            block_column = new List<int>();
            block_direct = new List<Direction>();
        }
        /// <summary>
        /// 入列函数，将创建随机迷宫所需的数据压入列
        /// </summary>
        /// <param name="x">随机对应的x值</param>
        /// <param name="y">随机对应的y值</param>
        /// <param name="direct">随机对应的方向</param>
        private void PushToList(int x, int y, Direction direct)
        {
            block_row.Add(y);
            block_column.Add(x);
            block_direct.Add(direct);
        }
        /// <summary>
        /// 是否有通路
        /// </summary>
        /// <returns>返回通路的值</returns>
        private int Count()
        {
            int cnt = 0;
            if (x_num + 1 <= column)//碰到墙壁，则取消入列
            {
                PushToList(x_num + 1, y_num, Direction.Right);
                cnt++;
            }
            if (y_num + 1 <= row)
            {
                PushToList(x_num, y_num + 1, Direction.Down);
                cnt++;
            }
            if (x_num - 1 >= 1)
            {
                PushToList(x_num - 1, y_num, Direction.Left);
                cnt++;
            }
            if (y_num - 1 >= 1)
            {
                PushToList(x_num, y_num - 1, Direction.UP);
                cnt++;
            }
            return cnt;
        }
        /// <summary>
        /// for debug，调试时使用
        /// </summary>
        private void Show(int[][] maze)
        {
            for (int i = 0; i <= row + 1; i++)//y
            {
                for (int j = 0; j <= column + 1; j++)//x
                    if ((j == Start.X && i == Start.Y) || (j == End.X && i == End.Y))
                        Console.Write("=>");
                    else if (maze[j][i] == way)
                        Console.Write("  ");
                    else if (maze[j][i] == wall)
                        Console.Write("[]");
                    else if (maze[j][i] == access)
                        Console.Write("TT");
                    else if (maze[j][i] == 4)
                        Console.Write("XX");
                Console.Write("\n");
            }
        }
        /// <summary>
        /// 绘制地图，在调用此函数前必须先执行CreateMap()或者SetMapMaze()
        /// </summary>
        /// <param name="g">绘图</param>
        /// <returns>创建成功</returns>
        public bool DrawMap(Graphics g)
        {
            if (maze == null)
                throw new Exception("Please Init Map, Try to CreateMap().");
            for (int i = 0; i <= row + 1; i++)//y
                for (int j = 0; j <= column + 1; j++)//x
                {
                    if (maze[j][i] == wall)
                        g.FillRectangle(new SolidBrush(MapColor), j * size, i * size, size, size);
                    else if (is_show_way && maze_way[j][i] == access)
                        g.FillRectangle(Brushes.Red, j * 10 + 2, i * 10 + 2, 5, 5);
                }
            return true;
        }


        //以下是判断迷宫是否有路的一个解法，使用的是stack来判断四周是否有路
        //判断依据为：通过判断四周是否为路，若为路则将四周有路的点入栈，否则不入栈，选取一条路，向前走，每走一步用一个符号标识即access
        //若周围不存在路（即周围为墙或者不通路）则将该路出栈，标识为不通路，即4  ⬇  下面代码中写了
        //若栈空且未找到终点，可以判定此迷宫不通
        //这个判断解迷宫不存在问题，但在游戏中会存在一定bug，可以换成其他方式解决：
        //如深度遍历、广度遍历试试，这里不再改正。（bug为存在多条路径通向终点时，猎人会出现抖动情况，详见猎人类中实现）

        /// <summary>
        /// 解迷宫的类
        /// </summary>
        private struct PathWay
        {
            public Point pos;
            public bool hasLeft, hasRight, hasUp, hasDown;
        };
        public int[][] maze_way;//包含迷宫和迷宫路
        /// <summary>
        /// 判断从起点到终点是否联通
        /// </summary>
        /// <param name="start_point">起点</param>
        /// <param name="end_point">终点</param>
        /// <returns>若联通，则返回true，不连通返回false</returns>
        public bool Connected(Point start_point, Point end_point)
        {
            InitMazeWay();
            Stack<PathWay> S = new Stack<PathWay>();
            PathWay point = new PathWay
            {
                pos = start_point,
                hasDown = false,
                hasUp = false,
                hasLeft = false,
                hasRight = false
            };

            while (point.pos != end_point)
            {
                maze_way[point.pos.X][point.pos.Y] = access;
                int times = MazeAround(ref point);
                if (times == 0)
                {
                    maze_way[point.pos.X][point.pos.Y] = 4;
                    if (S.Count == 0)//找不到出口
                        return false;
                    point = S.Pop();
                    times = MazeAround(ref point);
                }
                if (times != 0)
                    S.Push(point);
                if (point.hasRight)
                    point.pos.X += 1;
                else if (point.hasDown)
                    point.pos.Y += 1;
                else if (point.hasUp)
                    point.pos.Y -= 1;
                else if (point.hasLeft)
                    point.pos.X -= 1;

                //Console.WriteLine();
                //Show(maze_way);
            }
            maze_way[end_point.X][end_point.Y] = access;
            return true;
        }
        /// <summary>
        /// 查找地图
        /// </summary>
        private void InitMazeWay()
        {
            if (maze == null)
                throw new Exception("Please Init Map, Try Use SetMapMaze(int[][] maze).");
            maze_way = new int[MAXSIZE][];
            for (int i = 0; i <= column + 1; i++)//row+1包含墙壁2栋墙壁
                maze_way[i] = new int[MAXSIZE];
            for (int i = 0; i <= column + 1; i++)//x
                for (int j = 0; j <= row + 1; j++)//y
                    maze_way[i][j] = maze[i][j];
        }

        /// <summary>
        /// 判断地图是否有路可走
        /// </summary>
        /// <param name="p">输入点</param>
        /// <returns>返回周围路的个数</returns>
        private int MazeAround(ref PathWay p)
        {
            int times = 0;
            bool isUpEdge = false;
            bool isRightEdge = false;
            bool isLeftEdge = false;
            bool isDownEdge = false;
            if (p.pos.Y <= 0)
                isUpEdge = true;
            if (p.pos.Y >= row + 1)
                isDownEdge = true;
            if (p.pos.X <= 0)
                isLeftEdge = true;
            if (p.pos.X >= column + 1)
                isRightEdge = true;
            if (!isUpEdge && maze_way[p.pos.X][p.pos.Y - 1] == way)
            {
                p.hasUp = true;
                times++;
            }
            else p.hasUp = false;
            if (!isRightEdge && maze_way[p.pos.X + 1][p.pos.Y] == way)
            {
                p.hasRight = true;
                times++;
            }
            else p.hasRight = false;
            if (!isDownEdge && maze_way[p.pos.X][p.pos.Y + 1] == way)
            {
                p.hasDown = true;
                times++;
            }
            else p.hasDown = false;
            if (!isLeftEdge && maze_way[p.pos.X - 1][p.pos.Y] == way)
            {
                p.hasLeft = true;
                times++;
            }
            else p.hasLeft = false;
            return times;
        }
    }
}
