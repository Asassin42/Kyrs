using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    interface IFigure
    {
        void Draw(Graphics g);

        void Shift(int dx, int dy, Point p);

        void Shift(int dx, int dy);

        Rectangle[] GetInnerRectangle();

        Region GetFigure();
    }
}
