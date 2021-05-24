using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEComponent
{
    /// <summary>
    /// This is interface which is used by different shapes.
    /// </summary>
    public interface IShapes
    {
        /// <summary>
        /// This is set method to set color and parameter lists.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="list"></param>
        void set(Color c, params int[] list);
        /// <summary>
        /// This is the draw method to set the graphics.
        /// </summary>
        /// <param name="g"></param>
        void draw(Graphics g);
    }
}

