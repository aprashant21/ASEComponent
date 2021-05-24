using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEComponent
{
    /// <summary>
    /// This is abstract class named createshapes.
    /// </summary>
    abstract class CreateShapes
    {
        /// <summary>
        /// This is abstract method getshape which helps to get shapes like rectangle, circle and triangle.
        /// </summary>
        /// <param name="ShapeType"></param>
        /// <returns></returns>
        public abstract IShapes getShape(string ShapeType);
    }
}
