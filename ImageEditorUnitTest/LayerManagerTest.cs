using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEditorSpace;

namespace ImageEditorUnitTest
{
    /// <summary>
    /// LayerManagerTest 的摘要描述
    /// </summary>
    [TestClass]
    public class LayerManagerTest
    {

        LayerManager _layerManager;
        PrivateObject _target; //Access private object
        const string TEST_NAME1 = "layer0";
        const string TEST_NAME2 = "layer1";
        const bool TEST_ISVISIBLE = true;
        const bool TEST_ISNOTVISIBLE = false;
        
        [TestInitialize()]
        public void TestInitialize() 
        {
            _layerManager = new LayerManager();
            _target = new PrivateObject(_layerManager);
        }
        
        [TestMethod]
        public void TestCreateNewLayer()
        {
            _layerManager.CreateNewLayer();
            List<Layer> layerList = (List<Layer>)_target.GetFieldOrProperty("layerList");
            int indexOfLayer = (int)_target.GetFieldOrProperty("indexOfLayer");
            Assert.IsNotNull(layerList);
            Assert.AreEqual(1, layerList.Count);
            Assert.AreEqual("layer0", layerList[0].Name);
            Assert.AreEqual(1, indexOfLayer);
        }

        [TestMethod]
        public void TestDeleteLayer()
        {
            _layerManager.CreateNewLayer();
            List<Layer> layerList = (List<Layer>)_target.GetFieldOrProperty("layerList");
            int indexOfLayer = (int)_target.GetFieldOrProperty("indexOfLayer");
            Assert.IsNotNull(layerList);
            Assert.AreEqual(1, layerList.Count);
            Assert.AreEqual("layer0", layerList[0].Name);
            Assert.AreEqual(1, indexOfLayer);

            _layerManager.DeleteLayer(layerList[0]);
            Assert.AreEqual(0, layerList.Count);
        }

        [TestMethod]
        public void TestGetLayers()
        {
             List<Layer> layerList1 = _layerManager.GetLayers();
             List<Layer> layerList2 = (List<Layer>)_target.GetFieldOrProperty("layerList");
             Assert.IsInstanceOfType(layerList1, typeof(List<Layer>));
             Assert.AreSame(layerList1, layerList2);
             Assert.AreEqual(layerList1, layerList2);
        }

        [TestMethod]
        public void TestSetLayer()
        {
            _layerManager.CreateNewLayer();
            List<Layer> layerList = (List<Layer>)_target.GetFieldOrProperty("layerList");
            int indexOfLayer = (int)_target.GetFieldOrProperty("indexOfLayer");
            Assert.IsNotNull(layerList);
            Assert.AreEqual(1, layerList.Count);
            Assert.AreEqual("layer0", layerList[0].Name);
            Assert.AreEqual(1, indexOfLayer);

            _layerManager.SetLayer(TEST_NAME1);
            Layer layer = (Layer)_target.GetFieldOrProperty("currentLayer");
            Assert.AreEqual("layer0", layer.Name);
            
        }

        [TestMethod]
        public void TestGetCurrentLayer()
        {
            _layerManager.CreateNewLayer();
            List<Layer> layerList = (List<Layer>)_target.GetFieldOrProperty("layerList");
            int indexOfLayer = (int)_target.GetFieldOrProperty("indexOfLayer");
            Assert.IsNotNull(layerList);
            Assert.AreEqual(1, layerList.Count);
            Assert.AreEqual("layer0", layerList[0].Name);
            Assert.AreEqual(1, indexOfLayer);

            _layerManager.SetLayer(TEST_NAME1);
            Layer layer = (Layer)_target.GetFieldOrProperty("currentLayer");
            Assert.IsNotNull(layer);
            Assert.AreEqual("layer0", layer.Name);

            Layer currentLayer = _layerManager.GetCurrentLayer();
            Assert.IsNotNull(currentLayer);
            Assert.AreEqual("layer0", layer.Name);
        }

        [TestMethod]
        public void TestSetLayerVisible()
        {
            _layerManager.CreateNewLayer();
            List<Layer> layerList = (List<Layer>)_target.GetFieldOrProperty("layerList");
            int indexOfLayerForFirstCreate = (int)_target.GetFieldOrProperty("indexOfLayer");
            Assert.IsNotNull(layerList);
            Assert.AreEqual(1, layerList.Count);
            Assert.AreEqual("layer0", layerList[0].Name);
            Assert.AreEqual(1, indexOfLayerForFirstCreate);

            _layerManager.CreateNewLayer();
            int indexOfLayerForSecondCreate = (int)_target.GetFieldOrProperty("indexOfLayer");
            Assert.AreEqual(2, layerList.Count);
            Assert.AreEqual("layer1", layerList[1].Name);
            Assert.AreEqual(2, indexOfLayerForSecondCreate);

            _layerManager.SetLayerVisible(TEST_NAME1, TEST_ISVISIBLE);
            Assert.IsTrue(layerList[0].IsVisible);
            _layerManager.SetLayerVisible(TEST_NAME1, TEST_ISNOTVISIBLE);
            Assert.IsFalse(layerList[0].IsVisible);
            _layerManager.SetLayerVisible(TEST_NAME2, TEST_ISVISIBLE);
            Assert.IsTrue(layerList[1].IsVisible);
            _layerManager.SetLayerVisible(TEST_NAME2, TEST_ISNOTVISIBLE);
            Assert.IsFalse(layerList[1].IsVisible);
        }

        [TestMethod]
        public void TestGetVisibleLayer()
        {
            _layerManager.CreateNewLayer();
            List<Layer> layerList = (List<Layer>)_target.GetFieldOrProperty("layerList");
            int indexOfLayerForFirstCreate = (int)_target.GetFieldOrProperty("indexOfLayer");
            Assert.IsNotNull(layerList);
            Assert.AreEqual(1, layerList.Count);
            Assert.AreEqual("layer0", layerList[0].Name);
            Assert.AreEqual(1, indexOfLayerForFirstCreate);

            _layerManager.CreateNewLayer();
            int indexOfLayerForSecondCreate = (int)_target.GetFieldOrProperty("indexOfLayer");
            Assert.AreEqual(2, layerList.Count);
            Assert.AreEqual("layer1", layerList[1].Name);
            Assert.AreEqual(2, indexOfLayerForSecondCreate);

            _layerManager.SetLayerVisible(TEST_NAME1, TEST_ISVISIBLE);
            Assert.IsTrue(layerList[0].IsVisible);
            _layerManager.SetLayerVisible(TEST_NAME1, TEST_ISNOTVISIBLE);
            Assert.IsFalse(layerList[0].IsVisible);
            _layerManager.SetLayerVisible(TEST_NAME2, TEST_ISVISIBLE);
            Assert.IsTrue(layerList[1].IsVisible);
            _layerManager.SetLayerVisible(TEST_NAME2, TEST_ISNOTVISIBLE);
            Assert.IsFalse(layerList[1].IsVisible);

            _layerManager.SetLayerVisible(TEST_NAME2, TEST_ISVISIBLE);
            List<Layer> visibleLayer = _layerManager.GetVisibleLayers();
            Assert.AreEqual(1, visibleLayer.Count);
            Assert.AreEqual("layer1", visibleLayer[0].Name);
        }
    }
}
