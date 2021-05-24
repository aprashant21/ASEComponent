using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace TestProject1
{
    /// <summary>
    /// This is unit test class which helps to test different shapes.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Making color object.
        /// </summary>
        Color c1 = Color.Black;

        /// <summary>
        /// This is the circle test method where shape of circle is tested.
        /// </summary>
        [TestMethod]
        public void circleTestMethod()
        {
            var t = new ASEComponent.Circle();
            int x = 20, y = 80, radius = 20;
            t.set(c1, x, y, radius) ;
            Assert.AreEqual(20, t.x);
        }
        /// <summary>
        /// This is the circle test method where shape of triangle is tested.
        /// </summary>

        [TestMethod]
        public void triangleTestMethod()
        {
            var t = new ASEComponent.Triangle();
            int x = 100, y = 100, height=50, width=50;
            t.set(c1, x, y, height, width);
            Assert.AreEqual(100, t.x);
        }
        /// <summary>
        /// This is the circle test method where shape of rectangle is tested.
        /// </summary>
        [TestMethod]
        public void rectangleTestMethod()
        {
            var t = new ASEComponent.Rectangle();
            int x = 150, y = 150, height = 50, width = 50;
            t.set(c1, x, y, height, width);
            Assert.AreEqual(150, t.x);
        }
    }
}
