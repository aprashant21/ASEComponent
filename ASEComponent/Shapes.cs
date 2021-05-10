using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ASEComponent
{
    abstract class Shapes : IShapes
    {
        protected Color colour;
        protected int x, y;
        public Shapes()
        {
            colour = Color.Red;
            x = y = 100;
        }
        public Shapes(Color colour, int x, int y)
        {
            this.colour = colour;
            this.x = x;
            this.y = y;

        }
        public abstract void draw(Graphics g);
        public virtual void set(Color colour, params int[] list)
        {
            this.colour = colour;
            this.x = list[0];
            this.y = list[1];
        }

        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }
    }
}
