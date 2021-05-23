using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Color c1 = Color.Black;

        [TestMethod]
        public void circleTestMethod()
        {
            var t = new ASEComponent.Circle();
            int x = 20, y = 80, radius = 50;
            t.set(c1, x, y, radius) ;
            Assert.AreEqual(20, t.x);
        }

        [TestMethod]
        public void triangleTestMethod()
        {
            var t = new ASEComponent.Triangle();
            int x = 100, y = 100, height=50, width=50;
            t.set(c1, x, y, height, width);
            Assert.AreEqual(100, t.x);
        }
        public void rectangleTestMethod()
        {
            var t = new ASEComponent.Rectangle();
            int x = 150, y = 150, height = 50, width = 50;
            t.set(c1, x, y, height, width);
            Assert.AreEqual(150, t.x);
        }
    }
}
