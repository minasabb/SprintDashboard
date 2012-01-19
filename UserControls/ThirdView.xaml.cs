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
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for ThirdView.xaml
    /// </summary>
    public partial class ThirdView : UserControl
    {
        public ThirdView()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            stack1.Visibility = Visibility.Collapsed;
            stack2.Visibility= Visibility.Visible;
            Events.IncreaseSize(this,0.5);
            this.Refresh();
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            Events.DecreaseSize(this);
            stack1.Visibility = Visibility.Visible;
            stack2.Visibility = Visibility.Collapsed;
            this.Refresh();
        }
    }
}
