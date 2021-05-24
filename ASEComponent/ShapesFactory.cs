using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEComponent
{
    /// <summary>
    /// This is class that define factory design pattern.
    /// </summary>
    class ShapesFactory : CreateShapes
    {
        /// <summary>
        /// This public override Ishapes method helps to override the getshape method.
        /// </summary>
        /// <param name="ShapeType"></param>
        /// <returns></returns>
        public override IShapes getShape(string ShapeType)
        {
            ShapeType = ShapeType.ToLower().Trim();
            if (ShapeType.Equals("circle"))
            {
                return new Circle();
            }
            else if (ShapeType.Equals("rectangle"))
            {
                return new Rectangle();
            }

            else if (ShapeType.Equals("triangle"))
            {
                return new Triangle();
            }
            else if (ShapeType.Equals("fillrectangle"))
            {
                return new Rectangle();
            }
            else if (ShapeType.Equals("filltriangle"))
            {
                return new Triangle();
            }
            else if (ShapeType.Equals("fillcircle"))
            {
                return new Circle();
            }
           
            else
            {
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + ShapeType + " does not exist");
                throw argEx;
            }
        }
    }
}
