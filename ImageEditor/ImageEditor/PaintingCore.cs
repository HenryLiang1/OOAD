using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ImageEditorSpace
{
    public class PaintingCore
    {
        public event UpdateScreenEventHandler UpdateScreenEvent;
        public delegate void UpdateScreenEventHandler(IEnumerable<Canvas> canvas);

        //Update Layer
        public event UpdateLayerEventHandler UpdateLayerEvent;
        public delegate void UpdateLayerEventHandler(List<Layer> layerList, Layer currentLayer);

        public Tool currentTool;
        public Canvas currentCanvas;
        public LayerManager layerManager;
        public CommandManager commandManager;

        private bool pressed = false;
        private System.Windows.Point currentPoint;

        public PaintingCore()
        {
            currentCanvas = new Canvas(); //Temp
            currentTool = ToolBox.GetTool(ToolCategory.Pen);
            layerManager = new LayerManager();
            commandManager = new CommandManager();
        }

        public void ClickMouseDown(double x, double y)
        {
            Layer layer = layerManager.GetCurrentLayer();
            currentPoint.X = x;
            currentPoint.Y = y;

            if (layer.IsVisible == true)
            {
                pressed = true;
            }
            
        }

        public void ClickMouseUp(double x, double y)
        {
            Layer layer = layerManager.GetCurrentLayer(); // get current layer
            //layer.Draw(currentTool); // add new tool
            if (currentTool.GetType() == typeof(Pen))
            {
                ICommand usePenCommand = new UsePenCommand(layer, currentTool as Pen);
                commandManager.Execute(usePenCommand);
            }
            else if (currentTool.GetType() == typeof(StraightLine))
            {
                ICommand useStraightLineCommand = new UseStraightLineCommand(layer, currentTool as StraightLine);
                commandManager.Execute(useStraightLineCommand);
            }

            pressed = false;
            currentTool = ToolBox.GetTool(currentTool.GetCategory()); // change NEW pen
        }

        public void ClickMouseMove(double x, double y)
        {
            if (!pressed)
                return;
            double x1 = currentPoint.X;
            double y1 = currentPoint.Y;
            double x2 = x;
            double y2 = y;
            currentTool.Draw(x1, y1, x2, y2);
            currentPoint.X = x2;
            currentPoint.Y = y2;
            NotifyUpdateScreen();
        }

        public void SelectTool(ToolCategory toolCategory)
        {
            currentTool = ToolBox.GetTool(toolCategory);
        }


        public void CreateNewLayer()
        {
            layerManager.CreateNewLayer();
            NotifyUpdateLayer(); 
        }

        public void DeleteLayer()
        {
            layerManager.DeleteLayer();
            NotifyUpdateScreen();
            NotifyUpdateLayer();
        }

        public Layer GetCurrentLayer()
        {
            return layerManager.GetCurrentLayer();
        }

        public void SelectLayer(string id)
        {
            layerManager.SetLayer(id);
            //NotifyUpdateLayer();
            NotifyUpdateScreen();
            NotifyUpdateLayer();
        }

        public void SetLayerVisible(string id, bool isVisible)
        {
            layerManager.SetLayerVisible(id, isVisible);
            NotifyUpdateScreen();
        }

        public void Undo()
        {
            commandManager.Undo();
            NotifyUpdateScreen();

        }

        public void Redo()
        {
            commandManager.Redo();
            NotifyUpdateScreen();
        }


        public void NotifyUpdateScreen()
        {
            if (UpdateScreenEvent != null)
            {
                List<Layer> visiblelayers = layerManager.GetVisibleLayers();
                List<Canvas> canvasList = new List<Canvas>(); // Layer
                foreach (Layer layer in visiblelayers)
                {
                    if (layer.IsVisible == true)
                    {

                        foreach (Tool tool in layer.toolList)  //record all painting action   
                            canvasList.Add(tool.GetCanvas());

                        //動態繪製
                        if (!canvasList.Contains(currentTool.GetCanvas()))
                            canvasList.Add(currentTool.GetCanvas());
                    }
                }
                UpdateScreenEvent(canvasList);
            }
        }

        public void NotifyUpdateLayer()
        {
            if (UpdateLayerEvent != null)
            {
                List<Layer> layerList = new List<Layer>();
                layerList = layerManager.GetLayers();
                UpdateLayerEvent(layerList, layerManager.GetCurrentLayer());
            }
        }
    }
}
