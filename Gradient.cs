using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_v1._2
{
    class Gradient//渐变
    {
        private Color[] color = new Color[4];

        /// <summary>
        /// 请依次为左上，左下，右下，右上
        /// </summary>
        /// <param name="color">必须为4种颜色</param>
        public Gradient(Color[] color)
        {
            if (color.Length != 4)
                throw new Exception("Gradient Need Four Color");
            this.color = color;
        }

        /// <summary>
        /// 获取渐变中对应像素所得的颜色
        /// </summary>
        /// <param name="x">X坐标（0-1之间）左上角为源点</param>
        /// <param name="y">Y坐标（0-1之间）</param>
        /// <returns>返回对应坐标的颜色</returns>
        public Color GetColor(double x, double y)
        {
            Color[] color1 = new Color[2];

            color1[0] = color[1];
            color1[1] = color[2];
            Color temp_2 = Intrp(color1, x);

            color1[0] = color[0];
            color1[1] = color[3];
            Color temp_4 = Intrp(color1, x);

            color1[0] = temp_4;
            color1[1] = temp_2;
            return Intrp(color1, y);
        }

        private Color Intrp(Color[] _color, double t)
        {
            if (_color.Length != 2)
                throw new Exception("Gradient Error");
            return Color.FromArgb((int)((1 - t) * _color[0].A + t * _color[1].A), (int)((1 - t) * _color[0].R + t * _color[1].R),
               (int)((1 - t) * _color[0].G + t * _color[1].G), (int)((1 - t) * _color[0].B + t * _color[1].B));
        }
        /// <summary>
        /// 渐变填充一个四边形（构造是必须传入4种颜色）
        /// </summary>
        /// <param name="g">绘制函数</param>
        /// <param name="x">x坐标（左上角）</param>
        /// <param name="y">y坐标（左上角）</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="multi">放大倍数</param>
        public void FillGradient(Graphics g, int x, int y, int width, int height, int multi = 1)
        {
            for (int k = 0; k < height; k++)//k是y
                for (int l = 0; l < width; l++)//l是x
                {
                    Color getColor = MaxToOne(GetColor((double)(l / (double)width), (double)(k / (double)height)));
                    Color colorA = Color.FromArgb(getColor.A, getColor.R, getColor.G, getColor.B);
                    g.FillRectangle(new SolidBrush(colorA), x + l * multi, y + k * multi, multi, multi);
                }
        }

        private Color MaxToOne(Color color)
        {
            int max_value = Math.Max(color.R, Math.Max(color.G, color.B));

            if (max_value > 255)
            {
                int A = (color.A / max_value) * 255;
                int R = (color.R / max_value) * 255;
                int G = (color.G / max_value) * 255;
                int B = (color.B / max_value) * 255;
                return Color.FromArgb(A, R, G, B);
            }
            else return color;
        }

        //private Color Normal(Color color)
        //{
        //    int A = color.A, R = color.R, G = color.G, B = color.B;
        //    if (A > 255)
        //        A = 255;
        //    if (A < 0)
        //        A = 0;
        //    if (R > 255)
        //        R = 255;
        //    if (R < 0)
        //        R = 0;
        //    if (G > 255)
        //        G = 255;
        //    if (G < 0)
        //        G = 0;
        //    if (B > 255)
        //        B = 255;
        //    if (B < 0)
        //        B = 0;
        //    return Color.FromArgb(A, R, G, B);

        //}

    }
}
