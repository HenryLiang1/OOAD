using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageEditorSpace
{
    public class FileSystem
    {
        public void SaveFile(string fileName, Canvas currentCanvas)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)currentCanvas.RenderSize.Width,
                                                            (int)currentCanvas.RenderSize.Height,
                                                            96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(currentCanvas);

            var crop = new CroppedBitmap(rtb, new System.Windows.Int32Rect(0, 0, (int)currentCanvas.RenderSize.Width, (int)currentCanvas.RenderSize.Height));

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));

            using (var fs = System.IO.File.OpenWrite(fileName))
            {
                pngEncoder.Save(fs);
            }
        }

        public void LoadFile(string fileName, Layer currentLayer)
        {
            File file = new File(fileName);
            currentLayer.Draw(file);
        }

    }
}
