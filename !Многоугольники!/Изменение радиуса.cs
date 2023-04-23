using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Многоугольники_
{
    public partial class Изменение_радиуса : Form
    {
        public event RadiusChangedEventHandler RadiusChanged;
        public Изменение_радиуса()
        {
            InitializeComponent();
        }

        private void Изменение_радиуса_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if(this.RadiusChanged != null)
            {
                this.RadiusChanged(this, new RadiusEventArgs(trackBar1.Value));
            }
        }

        private void trackBar1_Move(object sender, EventArgs e)
        {
            if (this.RadiusChanged != null)
            {
                this.RadiusChanged(this, new RadiusEventArgs(trackBar1.Value));
            }
        }
    }
}
