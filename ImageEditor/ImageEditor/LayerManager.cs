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

        public void DeleteLayer()
        {
            if (layerList.Count > 0)
            {
                layerList.RemoveAt(layerList.Count - 1);
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
