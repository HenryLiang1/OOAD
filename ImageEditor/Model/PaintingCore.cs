using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ImageEditorSpace
{
    public class PaintingCore
    {
        public event UpdateScreenEventHandler UpdateScreenEvent;
        public delegate void UpdateScreenEventHandler(IEnumerable<Canvas> canvas);

        //Update Layer
        public event UpdateLayerEventHandler UpdateLayerEvent;
        public delegate void UpdateLayerEventHandler(List<Layer> layerList, Layer currentLayer);

        //Update MenuItem
        public event UpdateMenuEventHandler UpdateMenuEvent;
        public delegate void UpdateMenuEventHandler();

        public Tool currentTool;
        public LayerManager layerManager;
        public CommandManager commandManager;
        public FileSystem fileSystem;


        private bool pressed = false;
        private System.Windows.Point currentPoint;
        private SolidColorBrush currentBrushColor;
        

        public PaintingCore()
        {
            currentTool = ToolBox.GetTool(ToolType.Pen);
            layerManager = new LayerManager();
            commandManager = new CommandManager();
            fileSystem = new FileSystem();
            currentBrushColor = new SolidColorBrush(Colors.Black);
        }

        public void ClickMouseDown(double x, double y)
        {
            Layer layer = layerManager.GetCurrentLayer();
            currentTool.SetColor(currentBrushColor);
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
            if (currentTool is Pen)
            {
                ICommand usePenCommand = new UsePenCommand(layer, currentTool as Pen);
                commandManager.Execute(usePenCommand);
            }
            else if (currentTool is StraightLine)
            {
                ICommand useStraightLineCommand = new UseStraightLineCommand(layer, currentTool as StraightLine);
                commandManager.Execute(useStraightLineCommand);
            }

            pressed = false;
            currentTool = ToolBox.GetTool(currentTool.GetToolType()); // change NEW pen
            NotifyUpdateMenu();
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

        public void SelectTool(ToolType toolCategory)
        {
            currentTool = ToolBox.GetTool(toolCategory);
        }

        public void SetBrushColor(SolidColorBrush brushColor)
        {
            currentBrushColor = brushColor;
            currentTool.SetColor(currentBrushColor);
        }


        public void CreateNewLayer()
        {
            layerManager.CreateNewLayer();
            NotifyUpdateLayer(); 
        }

        public void DeleteLayer(Layer currentLayer)
        {
            layerManager.DeleteLayer(currentLayer);
            NotifyUpdateScreen();
            NotifyUpdateLayer();
            NotifyUpdateMenu();
        }

        public Layer GetCurrentLayer()
        {
            return layerManager.GetCurrentLayer();
        }

        public void SelectLayer(string id)
        {
            layerManager.SetLayer(id);
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
            NotifyUpdateMenu();
        }

        public void Redo()
        {
            commandManager.Redo();
            NotifyUpdateScreen();
            NotifyUpdateMenu();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return commandManager.IsRedoEnabled;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return commandManager.IsUndoEnabled;
            }
        }

        public void MoveLayer(string orention)
        {
            layerManager.MoveLayer(GetCurrentLayer(), orention);
            NotifyUpdateLayer();
        }

        public void LoadFile(string fileName)
        {
            
            layerManager = new LayerManager();
            layerManager.CreateNewLayer();
            Layer currentLayer = layerManager.GetCurrentLayer();
            fileSystem.LoadFile(fileName, currentLayer);
            NotifyUpdateLayer();
            NotifyUpdateScreen();
            NotifyUpdateMenu();
        }

        public void SaveFile(string fileName, Canvas currentCanvas)
        {
            fileSystem.SaveFile(fileName, currentCanvas);
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

        public void NotifyUpdateMenu()
        {
            if (UpdateMenuEvent != null)
            {
                UpdateMenuEvent();
            }
        }
    }
}
