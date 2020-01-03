using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_v1._2
{
    interface IDraw
    {
        bool Draw(Graphics g);
    }

    interface IMove
    {
        void Move();
    }
}
