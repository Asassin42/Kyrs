using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Triangle : IFigure
    {
        private Point[] points = new Point[3];

        public Triangle(Point p1, Point p2, Point p3)
        {
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
        }

        public void Draw(Graphics g)
        {
            g.DrawPolygon(Pens.Red, points);
            foreach (var i in points)
                g.FillEllipse(Brushes.Black, i.X - 3, i.Y - 3, 6, 6);
        }

        public void Shift(int dx, int dy, Point p)
        {
            for(int i=0;i<3;i++)
                if (p == points[i])
                {
                    points[i].X += dx;
                    points[i].Y += dy;
                }
        }

        public void Shift(int dx, int dy)
        {
            for (int i = 0; i < 3; i++)
            {
                points[i].X += dx;
                points[i].Y += dy;
            }
        }

        public Rectangle[] GetInnerRectangle()
        {
            var o = new Rectangle[3];
            for (int i = 0; i < 3; i++)
               o[i] = new Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8);
            return o;
        }

        public Region GetFigure()
        {
            var path = new GraphicsPath();

            path.AddPolygon(points);
           
            Region region = new Region(path);
            path.Dispose();

            return region;
        }
    }
}
