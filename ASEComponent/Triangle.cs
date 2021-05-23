using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEComponent
{
    /// <summary>
    /// This is the Triangle class where 'Draw' and 'draw' and , Triangle methods are created.
    /// </summary>
    public class Triangle : IShapes
    {
        public int x, y, width, height;

        public Triangle() : base()
        {
            width = 0;
            height = 0;
        }
        /// <summary>
        /// To get triangle width, height and x and y co-ordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Triangle(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        /// <summary>
        /// Draw method to draw the shape of the triangle.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            try
            {
                Point[] p = new Point[3];
                p[0].X = x;
                p[0].Y = y - (height / 2);

                p[1].X = x - (width / 2);
                p[1].Y = y + (height / 2);

                p[2].X = x + (width / 2);
                p[2].Y = y + (height / 2);
                Pen po = new Pen(Color.Red, 2);
                g.DrawPolygon(po, p);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// draw method is method to fill the triangle by color.
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            try
            {
                Point[] p = new Point[3];
                p[0].X = x;
                p[0].Y = y - (height / 2);

                p[1].X = x - (width / 2);
                p[1].Y = y + (height / 2);

                p[2].X = x + (width / 2);
                p[2].Y = y + (height / 2);

                SolidBrush b = new SolidBrush(Color.Red);
                g.FillPolygon(b, p);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Set method is for set the values for the x,y co-ordinate and width and height of the triangle.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="list"></param>

        public void set(Color c, params int[] list)
        {
            this.x = list[0]; 
            this.y = list[1];
            this.width = list[2];
            this.height = list[3];
        }
    }
}
