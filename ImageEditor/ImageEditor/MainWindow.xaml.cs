using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageEditorSpace
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        PaintingCore core;

        public MainWindow()
        {
            InitializeComponent();
            core = new PaintingCore();
            core.UpdateScreenEvent += UpdateScreen;
            core.UpdateLayerEvent += UpdateLayer;
            core.UpdateMenuEvent += UpdateMenu;
            core.CreateNewLayer();
        }

        private void SelectPenTool(object sender, MouseButtonEventArgs e)
        {
            core.SelectTool(ToolType.Pen);
        }

        private void SelectLineTool(object sender, MouseButtonEventArgs e)
        {
            core.SelectTool(ToolType.Line);
        }

        private void OpenFilesystem(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("OP");
        }

        private void HandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double x, y;
            x = e.GetPosition(currentCanvas).X;
            y = e.GetPosition(currentCanvas).Y;
            core.ClickMouseDown(x, y);
        }

        private void HandleCanvasMouseMove(object sender, MouseEventArgs e)
        {
            double x, y;
            x = e.GetPosition(currentCanvas).X;
            y = e.GetPosition(currentCanvas).Y;
            core.ClickMouseMove(x, y);
        }

        private void UpdateScreen(IEnumerable<Canvas> canvasList)
        {
            //add the same object to the canvas' children will cause error.
            //clear and add again
            this.currentCanvas.Children.Clear();
            foreach (Canvas canvas in canvasList)
                this.currentCanvas.Children.Add(canvas);
        }

        private void UpdateLayer(List<Layer> layerList, Layer currentLayer)
        {
            layerView.Children.Clear();
            foreach (Layer layer in layerList)
            {
                Brush color = currentLayer == layer ? Brushes.AliceBlue : Brushes.White;
                layerView.Children.Add(CreateLayerView(layer.Name, new Canvas(), color, layer.IsVisible));
                //core.SelectLayer(layer.Name); // current canvas is always point to the last layer
            }            
        }

        private void UpdateMenu()
        {
            UndoMenuItem.IsEnabled = core.IsUndoEnabled;
            RedoMenuItem.IsEnabled = core.IsRedoEnabled;
        }

        private void HandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double x, y;
            x = e.GetPosition(currentCanvas).X;
            y = e.GetPosition(currentCanvas).Y;
            core.ClickMouseUp(x, y);
        }

        private void CreateNewLayer(object sender, RoutedEventArgs e)
        {
            core.CreateNewLayer();
            
        }

        private void DeleteLayer(object sender, RoutedEventArgs e)
        {
            Layer currentLayer = core.GetCurrentLayer();
            core.DeleteLayer(currentLayer);
        }

        private void SelectLayer(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                Grid grid = sender as Grid;
                string id = grid.Name;
                core.SelectLayer(grid.Name);
                

            }
        }

        private void ClickUndoItem(object sender, MouseButtonEventArgs e)
        {
            core.Undo();
            Console.WriteLine("Undo");
        }

        private void ClickRedoItem(object sender, MouseButtonEventArgs e)
        {
            core.Redo();
            Console.WriteLine("Redo");
        }

        private void MoveUpLayer(object sender, RoutedEventArgs e)
        {
            Layer currentLayer = core.GetCurrentLayer();
            core.MoveLayer(currentLayer, "Up");
            Console.WriteLine("MoveUp");
        }

        private void MoveDownLayer(object sender, RoutedEventArgs e)
        {
            Layer currentLayer = core.GetCurrentLayer();
            core.MoveLayer(currentLayer, "Down");
            Console.WriteLine("MoveDown");
        }

        private void CheckLayerVisible(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox visibleBox = sender as CheckBox;
                Grid grid = visibleBox.Parent as Grid;
                
                bool isVisible = Convert.ToBoolean(visibleBox.IsChecked);
                core.SetLayerVisible(grid.Name, isVisible);

                Console.WriteLine(Convert.ToBoolean(visibleBox.IsChecked) + "Check");
            }
        }

        private void UpdateLayerView(IEnumerable<Canvas> canvas, IEnumerable<bool> visible, IEnumerable<Brush> gridColor)
        {
             //更新小圖
             
             //var xaml = System.Windows.Markup.XamlWriter.Save(child);
             //var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
        }

        private Grid CreateLayerView(string id, Canvas icon, Brush gridColor, bool visible)
        {
            // container -> content
            Grid containerGrid = new Grid();
            //containerGrid.Name = id;
            //containerGrid.Background = gridColor;
            Border contentBorder = new Border();
           
            contentBorder.BorderBrush = Brushes.Gray;
            contentBorder.BorderThickness = new Thickness(1.0);
            Grid contentGrid = new Grid();
            contentGrid.Background = gridColor;
            contentGrid.Name = id;
            contentGrid.MouseDown += SelectLayer;
            Border iconBorder = new Border();
            iconBorder.BorderBrush = Brushes.Black;
            iconBorder.BorderThickness = new Thickness(0.5);
            iconBorder.HorizontalAlignment = HorizontalAlignment.Left;
            iconBorder.Width = 30;
            iconBorder.Height = 30;
            iconBorder.Padding = new Thickness(0.0);
            iconBorder.Margin = new Thickness(5.0);
            Canvas iconCanvas = new Canvas();
            iconCanvas.Children.Add(icon); //*=

            TextBlock layerName = new TextBlock();
            layerName.HorizontalAlignment = HorizontalAlignment.Right;
            layerName.VerticalAlignment = VerticalAlignment.Top;
            layerName.Margin = new Thickness(0.0, 0.0, 17.0, 0.0);
            layerName.FontSize = 8.0;
            layerName.Text = id;

            TextBlock iconName = new TextBlock();
            iconName.HorizontalAlignment = HorizontalAlignment.Right;
            iconName.VerticalAlignment = VerticalAlignment.Center;
            iconName.Margin = new Thickness(0.0, 0.0, 17.0, 0.0);
            iconName.FontSize = 8.0;
            iconName.Text = "visible";
            CheckBox visibleBox = new CheckBox();
            visibleBox.VerticalAlignment = VerticalAlignment.Center;
            visibleBox.HorizontalAlignment = HorizontalAlignment.Right;
            visibleBox.IsChecked = visible;
            visibleBox.Checked += CheckLayerVisible;
            visibleBox.Unchecked += CheckLayerVisible;
            containerGrid.Children.Add(contentBorder);
            contentBorder.Child = contentGrid;
            contentGrid.Children.Add(iconBorder);
            iconBorder.Child = iconCanvas;
            contentGrid.Children.Add(layerName);
            contentGrid.Children.Add(iconName);
            contentGrid.Children.Add(visibleBox);
            return containerGrid;
        }
    }
}
