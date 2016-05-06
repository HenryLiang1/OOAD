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
    public class Pen : Tool
    {
        private List<Line> lines;
        // WPF not allow used lines in another canvas
        // here, we make an ouput canvas for future use for show or hide.
        // only until mouse up and this instance is overused.
        private Canvas outputCanvas;

        public Pen()
        {
            lines = new List<Line>();
            outputCanvas = new Canvas();
        }

        public void Draw(double x1, double y1, double x2, double y2)
        {
            Line line = new Line();
            line.Stroke = SystemColors.WindowFrameBrush;
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            lines.Add(line);
            //canvas.Children.Add(line);
        }

        public Canvas GetCanvas()
        {
            outputCanvas.Children.Clear();
            foreach (Line line in lines)
                outputCanvas.Children.Add(line);
            return outputCanvas;
        }

        public ToolType GetToolType()
        {
            return ToolType.Pen;
        }
    }
}
