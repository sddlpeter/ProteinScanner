using System;
using System.Drawing;

namespace ProteinScanner
{
    class ScannerModel
    {
        public float Threshold { get; set; } = 50.0f;
        public string FilePath { get; set; }

        public Tuple<Bitmap, float> ProcessImage()
        {
            // missing filePath
            if (string.IsNullOrEmpty(this.FilePath))
            {
                return null;
            }

            // original image
            Bitmap oriImg = (Bitmap)Image.FromFile(this.FilePath);

            // processing
            Size size = oriImg.Size;
            int numPixelsTotal = size.Height * size.Width;
            int numPixelsHighBrightness = 0;
            Bitmap proImg = new Bitmap(oriImg);
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    Color oriColor = oriImg.GetPixel(x, y);
                    if (oriColor.GetBrightness() * 100.0f >= this.Threshold)
                    {
                                                       //  Alpha     R   G  B
                        Color anaColor = Color.FromArgb(oriColor.A, 255, 0, 0);
                        // mark the color as red if brightness >= threshold
                        proImg.SetPixel(x, y, anaColor);
                        numPixelsHighBrightness++;
                    }
                }
            }
            float ratioPixelsHighBrightness = 100.0f * numPixelsHighBrightness / numPixelsTotal;

            return Tuple.Create(proImg, ratioPixelsHighBrightness);
        }
    }
}
