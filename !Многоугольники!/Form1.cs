using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _Многоугольники_
{
    [Serializable]
    public partial class Form1 : Form
    {
        List<Shape> List;
        Shape s;
        int what = 0; bool b; Pen pen;

        UndoRedo<List<Shape>> undoRedo = new UndoRedo<List<Shape>>();
        int changes_count;

        public Form1()
        {
            InitializeComponent();
            List = new List<Shape>();
            pen = new Pen(Shape.Color);
            pen.Width = 3;

            undoRedo.Copy(List);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void сохранитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SafeState();
        }

        private void SafeState()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("save.bin", FileMode.Create, FileAccess.Write);
            binaryFormatter.Serialize(fileStream, List);
            fileStream.Close();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadState();
        }
        private void LoadState()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("save.bin", FileMode.Open, FileAccess.Read);
            List = (List<Shape>)binaryFormatter.Deserialize(fileStream);
            Invalidate();
            fileStream.Close();

            undoRedo.Copy(List);
            changes_count = 0;
            this.Invalidate();
        }

        private void кругToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            what = 0;
        }

        private void квадратToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            what = 1;
        }

        private void треугольникToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            what = 2;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            b = false;
            if (e.Button == MouseButtons.Left)
            {
                foreach (Shape s in List)
                {
                    if (s.Check(e.X, e.Y))
                    {
                        s.IsClicked = true;
                        b = true;
                        s.deltaX = e.X - s.x;
                        s.deltaY = e.Y - s.y;
                    }
                }
                if (!b)
                    switch (what)
                    {
                        case 0:
                            {
                                s = new Circle(e.X, e.Y);
                                List.Add(s);
                                break;
                            }

                        case 1:
                            {
                                s = new Square(e.X, e.Y);
                                List.Add(s);
                                break;
                            }

                        case 2:
                            {
                                s = new Triangle(e.X, e.Y);
                                List.Add(s);
                                break;
                            }
                    }
            }
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (List[i].Check(e.X, e.Y))
                    {
                        List.RemoveAt(i);
                    }
                }
            }
            Refresh();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Shape s in List)
            {
                if (s.IsClicked)
                {
                    s.x = e.X - s.deltaX;
                    s.y = e.Y - s.deltaY;
                }
            }
            Refresh();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            for (int q = 0; q < List.Count; q++)
            {
                List[q].IsClicked = false;
               /* if (List[q].IsInMembrane == false)
                {
                    List.RemoveAt(q);
                }*/
            }

            undoRedo.Copy(List);
            changes_count = 0;
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape s in List)
            {
                s.Draw(e.Graphics);
            }

            for (int i = 0; i < List.Count; i++)                                                      //Грэхем
            {

                int v = 0;
                for (int j = i + 1; j < List.Count; j++)
                {
                    List[i].IsInMembrane = true;
                    float k = (List[i].y - List[j].y) / (List[i].x - List[j].x);
                    float b = List[i].y - k * List[i].x;
                    int above = 0;
                    int under = 0;
                    for (int m = 0; m < List.Count; m++)
                    {
                        if (m != j && m != i)
                        {
                            if (k * List[m].x + b > List[m].y)
                            {
                                above++;
                            }
                            if (k * List[m].x + b < List[m].y)
                            {
                                under++;
                            }
                        }
                        v = m + 1;
                    }
                    if (above == 0 || under == 0)
                    {
                        e.Graphics.DrawLine(pen, List[i].x, List[i].y, List[j].x, List[j].y);
                        /*List[i].IsInMembrane = true;
                        List[j].IsInMembrane = true;*/
                        /*SolidBrush solid = new SolidBrush(Shape.color);
                        PointF[] arr = List.ConvertAll(new Converter<Shape, Point>(point));
                        e.Graphics.FillClosedCurve(solid, arr);*/
                    }
                }
                  /*for (int q = 0; q < List.Count - 1; q++)
                  {
                      if (!List[q].IsInMembrane)
                      {
                          List.RemoveAt(q);
                      }
                  }*/
            }

        }

        private static PointF point (Shape s)
        {
            return new Point((int)s.x, (int)s.y);
        }

        private void цветВершинToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

                Shape.Color = colorDialog1.Color;
                Invalidate();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Изменение_радиуса f2 = new Изменение_радиуса();
            f2.Show();
            f2.RadiusChanged += RadiusDelegate;
        }

        private void RadiusDelegate(object sender, RadiusEventArgs e)
        {
            Shape.radius = e.Rad;
            Refresh();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            foreach (Shape s in List)
            {
                s.x = s.x + random.Next(-5, 5);
                s.y = s.y + random.Next(-5, 5);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer2.Interval = 200;
            timer2.Start();
            timer2.Tick += new EventHandler(timer_Tick);
        }

        private void изменитьЦветГраницToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pen.Color = colorDialog1.Color;
            Refresh();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            timer2.Stop();

            undoRedo.Copy(List);
            changes_count = 0;
            this.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (changes_count < undoRedo.history.Count - 1)
            {
                changes_count++;
            }
            List = undoRedo.Record(undoRedo.Undo(changes_count));
            this.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (changes_count > 0)
            {
                changes_count--;
            }
            List = undoRedo.Record(undoRedo.Undo(changes_count));
            this.Invalidate();
        }
    }
}
