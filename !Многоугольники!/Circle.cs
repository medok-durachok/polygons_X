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
    class Circle : Shape
    {

    public Circle(float x, float y) : base(x, y)
        {
        }

        public override void Draw(Graphics g)
        {
            brush = new SolidBrush(Color);
            g.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
        }

        public override bool Check(int xx, int yy)
        {
            if ((int)Math.Sqrt(Math.Pow(xx - x, 2) + Math.Pow(yy - y, 2)) <= radius * 1.5)
            {
                return true;
            }
            return false;
        }
    }
}
