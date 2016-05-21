using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Shapes;
using ImageEditorSpace;
using System.Windows.Controls;

namespace ImageEditorUnitTest
{
    /// <summary>
    /// StraightLineTest 的摘要描述
    /// </summary>
    [TestClass]
    public class StraightLineTest
    {
        private StraightLine _line;
        private PrivateObject _target;
        private const double TEST_X1 = 0;
        private const double TEST_X2 = 0;
        private const double TEST_Y1 = 50;
        private const double TEST_Y2 = 50;

        [TestInitialize()]
        public void TestInitialize()
        {
            _line = new StraightLine();
            _target = new PrivateObject(_line);
        }

        [TestMethod]
        public void TestDraw()
        {
            _line.Draw(TEST_X1, TEST_X2, TEST_Y1, TEST_Y2);
            Line line = (Line)_target.GetFieldOrProperty("line");
            Assert.IsNotNull(line);
        }

        [TestMethod]
        public void TestGetCanvas()
        {
            Canvas canvas1 = _line.GetCanvas();
            Canvas canvas2 = (Canvas)_target.GetFieldOrProperty("outputCanvas");
            Assert.AreSame(canvas1, canvas2);
            Assert.AreEqual(canvas1, canvas2);
        }

        [TestMethod]
        public void TestGetType()
        {
            ToolType Tool = _line.GetToolType();
            Assert.AreEqual(ToolType.Line, Tool);
        }
    }
}
