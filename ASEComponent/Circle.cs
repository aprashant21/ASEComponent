using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEComponent
{
    /// <summary>
    /// This is the circle class where circle method is created.
    /// </summary>
    public class Circle : IShapes
    {
        /// <summary>
        /// variables are declared.
        /// </summary>
        public int x, y, radius;

        /// <summary>
        /// This is base class of circle where value of radius = 0.
        /// </summary>
        public Circle() : base()
        {
            radius = 0;
        }
        /// <summary>
        /// This is the circle method where radius variable get value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public Circle(int x, int y, int radius)
        {
            this.radius = radius;
        }
        /// <summary>
        /// This is the Draw method where shape of circle is initialize.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Green, 2);
                g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// This is the method to fill the circle by color.
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            try
            {
                SolidBrush b = new SolidBrush(Color.Green);
                g.FillEllipse(b, x - radius, y - radius, radius * 2, radius * 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// This is the set method to set the value of x,y co-ordinate and radius.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="list"></param>
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
