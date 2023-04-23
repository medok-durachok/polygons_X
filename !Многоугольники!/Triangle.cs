using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace _Многоугольники_
{
    [Serializable]
    class Triangle : Shape
    {
        public Triangle(float x, float y) : base(x, y)
        {
        }

        public override void Draw(Graphics g)
        {
            g.FillPolygon(brush, new PointF[] { new PointF(x, y - radius), new PointF(x - radius, y + radius), new PointF(x + radius, y + radius) });
        }

        public override bool Check(int xx, int yy)
        {
            /* if (y > 2 * ( - Y2) * (X - X1) / (X2 - X1) + Y2 && Y > 2 * (Y1 - Y2) * (X - X2) / (X1 - X2) + Y2 && Y < Y2)
             {
                 return true;
             }*/
            return false;
        }
    }
}
