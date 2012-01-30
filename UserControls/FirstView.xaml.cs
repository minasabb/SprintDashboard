using System.Windows;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;

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
            Events.UpdateContentEvent += UpdateContentEvent;
        }

        void UpdateContentEvent(object sender)
        {
            var control = sender as ResizableContentControl;
            if (control == null || control.Name != Name || stack2.Visibility != Visibility.Hidden)
                return;
            stack2.Visibility = Visibility.Visible;
            Loading.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stack2.Visibility=Visibility.Hidden;
            Loading.Visibility=Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stack2.Visibility = Visibility.Collapsed;
        }

        
    }
}
