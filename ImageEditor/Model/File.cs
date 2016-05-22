using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageEditorSpace
{
    public class File : Tool
    {
        Image image;
        string fileName;
        // WPF not allow used lines in another canvas
        // here, we make an ouput canvas for future use for show or hide.
        // only until mouse up and this instance is overused.
        private Canvas outputCanvas;

        public File(string fileName)
        {
            image = new Image();
            outputCanvas = new Canvas();
            this.fileName = fileName;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fileName);
            bitmap.EndInit();
            image.Source = bitmap;
        }

        public Canvas GetCanvas()
        {


            outputCanvas.Children.Clear();
            outputCanvas.Children.Add(image);
            return outputCanvas;
        }

        public void Draw(double x1, double y1, double x2, double y2)
        {
            throw new NotImplementedException();
        }

        public ToolType GetToolType()
        {
            throw new NotImplementedException();
        }
    }
}
