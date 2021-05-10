using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEComponent
{
    public interface IShapes
    {
        void set(Color c, params int[] list);
        void draw(Graphics g);
    }
}
}
