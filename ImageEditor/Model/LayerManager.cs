using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEditorSpace
{
    public class LayerManager
    {
        private List<Layer> layerList;
        private int indexOfLayer = 0;
        private Layer currentLayer;

        public LayerManager()
        {
            layerList = new List<Layer>();
        }

        public void CreateNewLayer()
        {
            Layer layer = new Layer();

            //In order to set unique id for each layer
            //we set layer name in layer manager
            layer.Name = "layer" + indexOfLayer++;
            layer.IsVisible = true;
            Console.WriteLine(layerList.Count);
            //Console.WriteLine(layer.Name);
            
            layerList.Add(layer);
            if (layerList.Count == 1) //if exist only one layer, current layer is that one
            {
                SetLayer(layer.Name);
            }
            
        }

        public void DeleteLayer(Layer currentLayer)
        {
            int currentIndex = layerList.IndexOf(currentLayer);
            layerList.Remove(currentLayer);
            //detect the deleted layer whether it is in the tail of list or not
            //if currentIndex greater than last index in list, then currentlayer is the last index in list
            if (layerList.Count > 0)
            {
                this.currentLayer = currentIndex > layerList.Count - 1 ? layerList[layerList.Count - 1] : layerList[currentIndex];
            }
            else if (currentIndex == -1)
            {
                System.Windows.MessageBox.Show("No Layer to Delete");
            }
        }

        public void MoveLayer(Layer currentLayer, string oriention)
        {
            Layer tempLayer;
            int currentIndex = layerList.IndexOf(currentLayer);

            if (oriention.Equals("Up") && currentIndex > 0)
            {
                tempLayer = layerList[currentIndex];
                layerList[currentIndex] = layerList[currentIndex - 1];
                layerList[currentIndex - 1] = tempLayer;
            }
            else if (oriention.Equals("Down") && currentIndex < layerList.Count - 1)
            {
                tempLayer = layerList[currentIndex];
                layerList[currentIndex] = layerList[currentIndex + 1];
                layerList[currentIndex + 1] = tempLayer;
            }
            else
            {
                System.Windows.MessageBox.Show("No Layer to Move");
            }
        }


        public List<Layer> GetLayers()
        {
            return layerList;
        }

        public void SetLayer(string id)
        {
            foreach (Layer layer in layerList)
            {
                if (layer.Name.Equals(id))
                {
                    currentLayer = layer;
                    Console.WriteLine(currentLayer.Name);
                }
            }
        }

        public Layer GetCurrentLayer()
        {
            return currentLayer;
        }

        public void SetLayerVisible(string id, bool isVisible)
        {
            foreach (Layer layer in layerList)
            {
                if (layer.Name.Equals(id))
                {
                    layer.IsVisible = isVisible;
                    Console.WriteLine(layer.IsVisible);
                }
            }
        }

        public List<Layer> GetVisibleLayers()
        {
            List<Layer> visibleLayers = new List<Layer>();
            foreach (Layer layer in layerList)
            {
                if (layer.IsVisible == true)
                {
                    visibleLayers.Add(layer);
                }
            }
            return visibleLayers;
        }
    }
}
