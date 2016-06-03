using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
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
            /*BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fileName);
            bitmap.EndInit();*/

            BitmapImage bitmapImage = new BitmapImage();

            // BitmapSource objects like BitmapImage can only have their properties
            // changed within a BeginInit/EndInit block.
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(fileName, UriKind.Relative);

            // To save significant application memory, set the DecodePixelWidth or  
            // DecodePixelHeight of the BitmapImage value of the image source to the desired 
            // height or width of the rendered image. If you don't do this, the application will 
            // cache the image as though it were rendered as its normal size rather then just 
            // the size that is displayed.
            // Note: In order to preserve aspect ratio, set DecodePixelWidth
            // or DecodePixelHeight but not both.
            bitmapImage.DecodePixelWidth = 600;
            bitmapImage.EndInit();

            ////////// Convert the BitmapSource to a new format ////////////
            // Use the BitmapImage created above as the source for a new BitmapSource object
            // which is set to a gray scale format using the FormatConvertedBitmap BitmapSource.                                               
            // Note: New BitmapSource does not cache. It is always pulled when required.

            FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();

            // BitmapSource objects like FormatConvertedBitmap can only have their properties
            // changed within a BeginInit/EndInit block.
            newFormatedBitmapSource.BeginInit();

            // Use the BitmapSource object defined above as the source for this new 
            // BitmapSource (chain the BitmapSource objects together).
            newFormatedBitmapSource.Source = bitmapImage;

            // Set the new format to Gray32Float (grayscale).
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Gray32Float;
            newFormatedBitmapSource.EndInit();

            //set image source
            image.Source = newFormatedBitmapSource;
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

        public void SetColor(SolidColorBrush color)
        {
            throw new NotImplementedException();
        }
    }
}
