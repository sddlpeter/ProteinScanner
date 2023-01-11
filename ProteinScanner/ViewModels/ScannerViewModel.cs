using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ProteinScanner
{
    class ScannerViewModel : INotifyPropertyChanged
    {
        private ScannerModel scannerModel;

        public ScannerViewModel()
        {
            this.scannerModel = new ScannerModel();
        }

        public string FilePath
        {
            get
            {
                return this.scannerModel.FilePath;
            }
            set
            {
                this.scannerModel.FilePath = value;
                this.RaisePropertyChanged("FilePath");
            }
        }

        public float Threshold
        {
            get
            {
                return this.scannerModel.Threshold;
            }
            set
            {
                this.scannerModel.Threshold = value;
                this.RaisePropertyChanged("Threshold");
            }
        }

        private BitmapSource proImgSrc;
   
        public BitmapSource ProImgSrc
        {
            get
            {
                return this.proImgSrc;
            }
            set
            {
                this.proImgSrc = value;
                this.RaisePropertyChanged("ProImgSrc");
            }
        }

        private float ratioPixelsHighBrightness;
        
        public float RatioPixelsHighBrightness
        {
            get
            {
                return this.ratioPixelsHighBrightness;
            }
            set
            {
                this.ratioPixelsHighBrightness = value;
                this.RaisePropertyChanged("RatioPixelsHighBrightness");
            }
        }

        public void ProcessImage()
        {
            Tuple<Bitmap, float> rets = this.scannerModel.ProcessImage();
            if (rets == null)
            {
                return;
            }

            // update property value
            this.ProImgSrc = Imaging.CreateBitmapSourceFromHBitmap(rets.Item1.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            this.RatioPixelsHighBrightness = rets.Item2;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
