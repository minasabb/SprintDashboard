using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for FirstView.xaml
    /// </summary>
    public partial class FirstView : UserControl
    {
        public FirstView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Test.Visibility == Visibility.Collapsed)
                Test.Visibility = Visibility.Visible;
            else
                Test.Visibility = Visibility.Collapsed;
        }
    }
}
