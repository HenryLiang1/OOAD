using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEditorSpace;

namespace ImageEditorUnitTest
{
    /// <summary>
    /// ToolBoxTest 的摘要描述
    /// </summary>
    [TestClass]
    public class ToolBoxTest
    {
        [TestMethod]
        public void TestGetTool()
        {
            Assert.IsInstanceOfType(ToolBox.GetTool(ToolType.Line), typeof(StraightLine));
            Assert.IsInstanceOfType(ToolBox.GetTool(ToolType.Pen), typeof(Pen));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "A Tool is not support")]
        public void TestException()
        {
            var obj = ToolBox.GetTool(ToolType.Eraser);
        }
    }
  
}
