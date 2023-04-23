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
    abstract class Shape
    {
        public float x { get; set; }
        public float y { get; set; }
        public static int radius;
        public static Color Color { get; set; }
        public bool IsInMembrane { get; set; }
        public bool IsClicked { get; set; }


        [NonSerialized]

        public Brush brush;
        public float deltaX;
        public float deltaY;


        public Shape(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        static Shape()
        {
            radius = 20;
            Color = Color.DarkBlue;
        }

        public abstract void Draw(Graphics g);

        public abstract bool Check(int xx, int yy);
    }
}
