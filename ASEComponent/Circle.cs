using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEComponent
{
    class Circle : IShapes
    {
        public int x, y, radius;

        public Circle() : base()
        {
        }
        public Circle(int x, int y, int radius)
        {
            this.radius = radius;
        }

        public void draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Yellow, 2);
                g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void set(Color c, params int[] list)
        {
            try
            {
                this.x = list[0];
                this.y = list[1];
                this.radius = list[2];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
