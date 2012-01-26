using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IQ.Core.Windows.Animation;
using TextDashboard.Custom_Control;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for FirstView.xaml
    /// </summary>
    public partial class FirstView : ResizableContentControl
    {

        public FirstView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewWidth = ActualWidth +100;
            NewHeight = ActualHeight + 100;
        }

        
    }
}
