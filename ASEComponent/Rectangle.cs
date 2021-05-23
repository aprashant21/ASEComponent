using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEComponent
{
    /// <summary>
    /// This is the Rectangle class where 'Draw' and 'draw' and , Rectangle methods are created.
    /// </summary>
    public class Rectangle : IShapes
    {
        public int x, y, width, height;


        /// <summary>
        /// base method of rectangle.
        /// </summary>
        public Rectangle() : base()
        {
            width = 0;
            height = 0;
        }
        /// <summary>
        /// This is the method to get the method using this comamnd.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// This is the method to shape of circle.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Blue, 2);
                g.DrawRectangle(p, x - (width / 2), y - (height / 2), width * 2, height * 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// This is the method to fill the circle by blue color.
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            try
            {
                SolidBrush b = new SolidBrush(Color.Blue);
                g.FillRectangle(b, x - (width / 2), y - (height / 2), width * 2, height * 2);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// This is the set method to set the x,y co-ordinate and width & height of the rectangle.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="list"></param>
        public void set(Color c, params int[] list)
        {
            try
            {
                this.x = list[0];
                this.y = list[1];
                this.width = list[2];
                this.height = list[3];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
