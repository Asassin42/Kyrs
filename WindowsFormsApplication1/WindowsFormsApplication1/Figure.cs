using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Figure : IFigure
    {
        private Point[] points;

        private int _n;
        public Figure(int n)
        {
            _n = n;
            points = new Point[_n];
            SetRandomCoordinates();
        }

        public void Draw(Graphics g)
        {
            g.DrawPolygon(Pens.Black, points);
            foreach (var i in points)
                g.FillEllipse(Brushes.Black, i.X - 3, i.Y - 3, 6, 6);
        }

        public void Shift(int dx, int dy, Point p)
        {
            for (int i = 0; i < _n; i++)
                if (p == points[i])
                {
                    points[i].X += dx;
                    points[i].Y += dy;
                }
        }
        public void Shift(int dx, int dy)
        {
            for (int i = 0; i < _n; i++)
                {
                    points[i].X += dx;
                    points[i].Y += dy;
                }
        }

        public void SetRandomCoordinates()
        {
            var rnd = new Random();
            for (int i = 0; i < _n;i++ )
            {
                points[i].X = rnd.Next(50, 500);
                points[i].Y = rnd.Next(50, 500);
            }
        }

        public Rectangle[] GetInnerRectangle()
        {
            var o = new Rectangle[_n];
            for (int i = 0; i < _n; i++)
                o[i] = new Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8);
            return o;
        }

        public Region GetFigure()
        {
            var path = new GraphicsPath();

            path.AddPolygon(points);

            var region = new Region(path);
            path.Dispose();

            return region;
        }
    }
}
