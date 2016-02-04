using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Line : IFigure
    {
        private Point[] points = new Point[2];

        public Line(Point p1, Point p2)
        {
            points[0] = p1;
            points[1] = p2;
            
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(Pens.Black, points[0],points[1]);
            foreach (var i in points)
                g.FillEllipse(Brushes.Black, i.X - 3, i.Y - 3, 6, 6);
        }

        public void Shift(int dx, int dy, Point p)
        {
            for (int i = 0; i < 2; i++)
                if (p == points[i])
                {
                    points[i].X += dx;
                    points[i].Y += dy;
                }
        }

        public void Shift(int dx, int dy)
        {
            for (int i = 0; i < 2; i++)
            {
                points[i].X += dx;
                points[i].Y += dy;
            }
        }

        public Rectangle[] GetInnerRectangle()
        {
            Rectangle[] o = new Rectangle[2];
            for (int i = 0; i < 2; i++)
                o[i] = new Rectangle(points[i].X - 4, points[i].Y - 4, 8, 8);
            return o;
        }
        public Region GetFigure()
        {
            return null;
        }
    }
}
