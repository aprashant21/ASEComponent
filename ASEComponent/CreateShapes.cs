using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEComponent
{
    abstract class CreateShapes
    {
        public abstract IShapes getShape(string ShapeType);
    }
}
