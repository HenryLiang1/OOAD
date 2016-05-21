using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEditorSpace;

namespace ImageEditorUnitTest
{
    [TestClass]
    public class LayerTest
    {

        private Layer _layer;
        const bool TEST_ISVISIBLE = true;
        const string TEST_NAME = "layer0";
        
        [TestInitialize()]
        public void TestInitialize()
        {
            _layer = new Layer();
        }

        [TestMethod]
        public void TestIsVisible()
        {
            _layer.IsVisible = TEST_ISVISIBLE;
            Assert.IsTrue(_layer.IsVisible.Equals(TEST_ISVISIBLE));                
        }

        [TestMethod]
        public void TestName()
        {
            _layer.Name = TEST_NAME;
            Assert.AreEqual("layer0", _layer.Name);
        }
    }
}
