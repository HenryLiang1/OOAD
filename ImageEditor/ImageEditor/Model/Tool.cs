using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ImageEditorSpace
{
    public enum ToolType
    {
        Pen,
        Line,
        Eraser,
    }

    public interface Tool
    {
        void Draw(double x1, double y1, double x2, double y2);
        ToolType GetToolType();
        Canvas GetCanvas();
    }
}
