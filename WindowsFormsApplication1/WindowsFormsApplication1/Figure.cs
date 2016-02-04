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

        private int N;
        public Figure(int n)
        {
            N = n;
            points = new Point[N];
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
            for (int i = 0; i < N; i++)
                if (p == points[i])
                {
                    points[i].X += dx;
                    points[i].Y += dy;
                }
        }
        public void Shift(int dx, int dy)
        {
            for (int i = 0; i < N; i++)
                {
                    points[i].X += dx;
                    points[i].Y += dy;
                }
        }

        public void SetRandomCoordinates()
        {
            Random rnd = new Random();
            for (int i = 0; i < N;i++ )
            {
                points[i].X = rnd.Next(50, 500);
                points[i].Y = rnd.Next(50, 500);
            }
        }

        public Rectangle[] GetInnerRectangle()
        {
            Rectangle[] o = new Rectangle[N];
            for (int i = 0; i < N; i++)
                o[i] = new Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8);
            return o;
        }

        public Region GetFigure()
        {
            GraphicsPath path = new GraphicsPath();

            path.AddPolygon(points);

            Region region = new Region(path);
            path.Dispose();

            return region;
        }
    }
}
