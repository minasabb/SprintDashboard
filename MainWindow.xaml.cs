using System.Windows;
using System.Windows.Controls;

namespace TextDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class ControlPlacement
    {
        public string Name { get; set; }
        public double WidthPercentage { get; set; }
        public double HeightPercentage { get; set; }
        public double InitialXPercentage { get; set; }
        public double InitialYPercentage { get; set; }
        public double GrowPercentage { get; set; }
        public Control ViewControl { get; set; }
    }
}
