using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace _Многоугольники_
{
    [Serializable]
    public delegate void RadiusChangedEventHandler(object sender, RadiusEventArgs e);
    public class RadiusEventArgs : EventArgs
    {
        public int Rad { get; set; }

        public RadiusEventArgs(int r)
        {
            Rad = r;
        }
    }
}
