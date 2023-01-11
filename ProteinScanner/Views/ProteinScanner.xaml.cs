using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace ProteinScanner
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScannerViewModel scannerViewModel;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ThresholdSlider.AddHandler(MouseUpEvent, new RoutedEventHandler(ThresholdSlider_MouseUp), true);
            this.scannerViewModel = base.DataContext as ScannerViewModel;
            this.scannerViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == "FilePath" || e.PropertyName == "Threshold")
                {
                    this.scannerViewModel.ProcessImage();
                }
            };
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.scannerViewModel.FilePath = openFileDialog.FileName;
            }
        }

        // manullly binding
        private void ThresholdSlider_MouseUp(object sender, RoutedEventArgs e)
        {
            this.scannerViewModel.Threshold = (float) this.ThresholdSlider.Value;
        }
    }
}
