using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ImageEditorSpace
{
    public class StraightLine : Tool
    {
        private Line line;
        private bool firstClicked;
        // only until mouse up and this instance is overused.
        Canvas outputCanvas;

        public StraightLine()
        {
            line = new Line();
            outputCanvas = new Canvas();
            firstClicked = true;
        }

        public void Draw(double x1, double y1, double x2, double y2)
        {
            line.Stroke = SystemColors.WindowFrameBrush;
            if (firstClicked)
            {
                line.X1 = x1;
                line.Y1 = y1;
            }
            line.X2 = x2;
            line.Y2 = y2;
            firstClicked = false;
        }

        public Canvas GetCanvas()
        {
            outputCanvas.Children.Clear();
            outputCanvas.Children.Add(line);
            return outputCanvas;
        }

        public ToolType GetToolType()
        {
            return ToolType.Line;
        }
    }
}
