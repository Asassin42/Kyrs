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
        private IFigure _draggedFigure;
        private List<IFigure> FigureListDragged=new List<IFigure>();
        private Point _draggedPoint;

        private bool _dragEl;

        private Point _oldMousePosition;

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
            CheckClickCoal(e);

            
            CheckMoveFigure(e);
            CheckConnectFigure();

            _oldMousePosition = e.Location;
        }

       

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _draggedFigure = null;
            _draggedPoint = Point.Empty;

            FigureListDragged.Clear();
         
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            CheckFigureListDragged(e);
            CheckDraggedFigure(e);
        }

      

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _dragEl = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _dragEl = true;
        }

        //Check Mouse_Down
        private void CheckConnectFigure()
        {
            Region r;
            if (_dragEl)
                if (_draggedFigure != null)
                    if (!(_draggedFigure is Line))
                        foreach (var fl in FigureList)
                        {
                            if (fl is Line)
                            {
                                var o = fl;
                                r = _draggedFigure.GetFigure();
                                if (r.IsVisible(o.GetInnerRectangle()[0]) || r.IsVisible(o.GetInnerRectangle()[1]))
                                {
                                    FigureListDragged.Add(o);
                                    FigureListDragged.Add(_draggedFigure);
                                    foreach (var el in FigureList)
                                        if (o != el)
                                            if (el.GetFigure().IsVisible(o.GetInnerRectangle()[0]) ||
                                                el.GetFigure().IsVisible(o.GetInnerRectangle()[1]))
                                                if (el != _draggedFigure)
                                                    FigureListDragged.Add(el);
                                }
                            }
                        }
        }

        private void CheckMoveFigure(MouseEventArgs e)
        {
            var r = new Region();

            foreach (var fl in FigureList)
            {
                r = fl.GetFigure();
                if (r != null)
                    if (r.IsVisible(e.Location))
                        _draggedFigure = fl;
            }
        }

        private void CheckClickCoal(MouseEventArgs e)
        {
            foreach (var fl in FigureList)
            {
                foreach (var i in fl.GetInnerRectangle())
                {
                    if (i.Contains(e.Location))
                    {
                        _draggedPoint.X = i.X + 4;
                        _draggedPoint.Y = i.Y + 4;
                        _draggedFigure = fl;
                    }
                }
            }
        }

        //Check Mouse_Up

        private void CheckDraggedFigure(MouseEventArgs e)
        {
            if (_draggedFigure != null)
            {
                var dx = e.Location.X - _oldMousePosition.X;
                var dy = e.Location.Y - _oldMousePosition.Y;
                if (_dragEl)
                    _draggedFigure.Shift(dx, dy);
                else if (_draggedPoint != Point.Empty)
                    _draggedFigure.Shift(dx, dy, _draggedPoint);

                _oldMousePosition = e.Location;
                _draggedPoint.X += dx;
                _draggedPoint.Y += dy;
                Invalidate();
            }
        }

        private void CheckFigureListDragged(MouseEventArgs e)
        {
            if (FigureListDragged.Count != 0)
            {
                var dx = e.Location.X - _oldMousePosition.X;
                var dy = e.Location.Y - _oldMousePosition.Y;
                if (_dragEl)
                    foreach (var i in FigureListDragged)
                        i.Shift(dx, dy);


                _oldMousePosition = e.Location;

                Invalidate();
            }
        }
    }
}
