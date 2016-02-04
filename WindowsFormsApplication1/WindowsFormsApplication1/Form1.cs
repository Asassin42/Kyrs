using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private List<IFigure> FigureList = new List<IFigure>();
        private IFigure draggedElement;
        private List<IFigure> draggedElements=new List<IFigure>();
        private Point draggedPoint;

        private bool dragEl=false;

        private Point oldMousePosition;

        private Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void линияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var o = new Line(new Point(rnd.Next(50, 500), rnd.Next(50, 500)), 
                new Point(rnd.Next(50, 500), rnd.Next(50, 500)));
            FigureList.Add(o);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var i in FigureList)
                i.Draw(e.Graphics);
        }

     

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var o = new Triangle(new Point(rnd.Next(50, 500), rnd.Next(50, 500)), 
                new Point(rnd.Next(50, 500), rnd.Next(50, 500)), 
                new Point(rnd.Next(50, 500), rnd.Next(50, 500)));
            FigureList.Add(o);
            Invalidate();
        }

        private void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var o = new Rectangl(new Point(rnd.Next(500), rnd.Next(500)),
                new Point(rnd.Next(50, 500), rnd.Next(50, 500)),
                new Point(rnd.Next(50, 500), rnd.Next(50, 500)),
                new Point(rnd.Next(50, 500), rnd.Next(50, 500)));
            FigureList.Add(o);
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void многоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var o = new Figure(comboBox1.SelectedIndex+5);
            FigureList.Add(o);
            Invalidate();
        }

        private void многоугольникToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var fl in FigureList)
            {
                foreach (var i in fl.GetInnerRectangle())
                {
                    if (i.Contains(e.Location))
                    {
                        draggedPoint.X = i.X + 4;
                        draggedPoint.Y = i.Y + 4;
                        draggedElement = fl;
                    }
                }
            }

            Region r = new Region();

            foreach (var fl in FigureList)
            {
                r = fl.GetFigure();
                if (r != null)
                    if (r.IsVisible(e.Location))
                        draggedElement = fl;
            }

            if (dragEl)
            if (draggedElement != null)
                if (!(draggedElement is Line))
                    foreach (var fl in FigureList)
                    {

                        if (fl is Line)
                        {
                            var o = fl;
                            r = draggedElement.GetFigure();
                            if (r.IsVisible(o.GetInnerRectangle()[0]) || r.IsVisible(o.GetInnerRectangle()[1]))
                            {
                                draggedElements.Add(o);
                                draggedElements.Add(draggedElement);
                                foreach (var el in FigureList)
                                    if(o!=el)
                                    if (el.GetFigure().IsVisible(o.GetInnerRectangle()[0]) || el.GetFigure().IsVisible(o.GetInnerRectangle()[1]))
                                        if (el != draggedElement)
                                            draggedElements.Add(el);
                            }
                        }

                    }

            oldMousePosition = e.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            draggedElement = null;
            draggedPoint = Point.Empty;

            draggedElements.Clear();
         
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedElements.Count!=0)
            {
                var dx = e.Location.X - oldMousePosition.X;
                var dy = e.Location.Y - oldMousePosition.Y;
                if (dragEl)
                    foreach(var i in draggedElements)
                    i.Shift(dx, dy);
              

                oldMousePosition = e.Location;

                Invalidate();
            }
            if (draggedElement != null)
            {
                var dx = e.Location.X - oldMousePosition.X;
                var dy = e.Location.Y - oldMousePosition.Y;
                if (dragEl)
                    draggedElement.Shift(dx, dy);
                else
                    if(draggedPoint!=Point.Empty)
                        draggedElement.Shift(dx, dy, draggedPoint);

                oldMousePosition = e.Location;
                draggedPoint.X += dx;
                draggedPoint.Y += dy;
                Invalidate();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dragEl = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dragEl = true;
        }
    }
}
