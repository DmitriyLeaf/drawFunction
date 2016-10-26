using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_6 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private int zoom = 10;
        private int lastX = 0;
        private int lastY = 0;
        private double fi = 0.001;

        private void decartCoord() {  //760 360
            Graphics gr = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Black);

            gr.DrawLine(p, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));
            gr.DrawLine(p, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
        }
        private void polarCoord() {
            Graphics gr = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Black);

            gr.DrawEllipse(p, pictureBox1.Width / 2 - 1, pictureBox1.Height / 2 - 1, 2, 2);
            gr.DrawLine(p, new Point(pictureBox1.Width / 2, pictureBox1.Height / 2), new Point(pictureBox1.Width / 2 + 100, pictureBox1.Height / 2));
            gr.DrawLine(p, new Point(pictureBox1.Width / 2 + 100, pictureBox1.Height / 2), new Point(pictureBox1.Width / 2 + 95, pictureBox1.Height / 2 + 3));
            gr.DrawLine(p, new Point(pictureBox1.Width / 2 + 100, pictureBox1.Height / 2), new Point(pictureBox1.Width / 2 + 95, pictureBox1.Height / 2 - 3));
        }

        private void drawPointTaskFirst(double x, double y, Color color) {
            Graphics gr = pictureBox1.CreateGraphics();
            Pen p = new Pen(color);
            int _x = (int)(x * zoom + pictureBox1.Width / 2);
            int _y = (int)(y * zoom + pictureBox1.Height / 2);

            gr.DrawLine(p, new Point(lastX, lastY), new Point(_x, _y));

            lastY = _y;
            lastX = _x;
        }

        private void drawPointTaskSecond(double f, double p, Color color) {
            double x = p * Math.Cos(f);
            double y = p * Math.Sin(f);
            drawPointTaskFirst(x, y, color);
        }

        private double taskFirstFuncFirst(double x) {
            double y = 0;
            y = 4 - Math.Sin(Math.Abs(x));
            return y;
        }
        private double taskFirstFuncSecond(double x) {
            double y = 0;
            y = Math.Abs(Math.Sin(x));
            return y;
        }
        private double taskSecondFunc(double f) {
            double p = 0;
            p = 400 / (Math.Cos(Math.PI * f) + Math.Cos(Math.E * f) + 4);
            return p;
        }
        private double taskThirdFunc(double t) {
            double x = 110 * Math.Cos(t) - 10 * Math.Cos(11 * t);
            double y = 110 * Math.Sin(t) - 10 * Math.Sin(11 * t);
            return x;
        }

        private void button5_Click(object sender, EventArgs e) {
            // first f
            double x = -1 * pictureBox1.Width / (2 * zoom);
            double y = 0;

            zoom = 10;
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(pictureBox1.BackColor);
            decartCoord();

            lastX = -1;
            lastY = (int)taskFirstFuncFirst(lastX) * zoom + pictureBox1.Height / 2;

            for (int i = 0; i < pictureBox1.Width / (fi * zoom); i++) {
                x += fi;
                y = taskFirstFuncFirst(x);
                drawPointTaskFirst(x, y, Color.Red);
            }
            // second f
            x = -1 * pictureBox1.Width / (2 * zoom);
            y = 0;
            
            decartCoord();

            lastX = -1;
            lastY = (int)taskFirstFuncSecond(lastX) * zoom + pictureBox1.Height / 2;

            for (int i = 0; i < pictureBox1.Width / (fi * zoom); i++) {
                x += fi;
                y = taskFirstFuncSecond(x);
                drawPointTaskFirst(x, y, Color.Green);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(pictureBox1.BackColor);
            zoom += 1;
            button5_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e) {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(pictureBox1.BackColor);
            zoom -= 1;
            button5_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e) {
            double f = 0;
            double p = taskSecondFunc(f);

            zoom = 1;
            fi = 0.001;

            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(pictureBox1.BackColor);
            polarCoord();

            lastX = (int)(p * Math.Cos(f) * zoom + pictureBox1.Width / 2);
            lastY = (int)(p * Math.Sin(f) * zoom + pictureBox1.Height / 2);

            for (int i = 0; i < 360 / (fi * zoom); i++) {
                f += fi;
                p = taskSecondFunc(f);
                drawPointTaskSecond(f, p, Color.Blue);
                if (f > 360 * 0.0174532925) break;
            }
        }

        private void button7_Click(object sender, EventArgs e) {
            double t = 0;
            double x = 0;
            double y = 0;

            zoom = 1;
            fi = 0.0001;

            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(pictureBox1.BackColor);
            decartCoord();

            lastX = (int)((110 * Math.Cos(t) - 10 * Math.Cos(11 * t)) * zoom + pictureBox1.Width / 2);
            lastY = (int)((110 * Math.Sin(t) - 10 * Math.Sin(11 * t)) * zoom + pictureBox1.Height / 2);

            for (int i = 0; i < 360 / (fi * zoom); i++) {
                t += fi;
                x = 110 * Math.Cos(t) - 10 * Math.Cos(11 * t);
                y = 110 * Math.Sin(t) - 10 * Math.Sin(11 * t);
                drawPointTaskFirst(x, y, Color.Green);
                if (t > 360 * 0.0174532925) break;
            }
        }
    }
}


/*
    Font myFont = new Font("Arial", 8);
            gr.DrawString(Convert.ToString(x), myFont, Brushes.Black, new Point(20, wtf));
            gr.DrawString(Convert.ToString(y), myFont, Brushes.Black, new Point(60, wtf));
            gr.DrawString(Convert.ToString(_x), myFont, Brushes.Black, new Point(20, wtf + 10));
            gr.DrawString(Convert.ToString(_y), myFont, Brushes.Black, new Point(60, wtf + 10));
            wtf += 20;
*/
// p = 400/(cos(Pi*f) + cos(E*f) +4);