using System.Windows;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for ThirdView.xaml
    /// </summary>
    public partial class ThirdView : ResizableContentControl
    {
        public ThirdView()
        {
            InitializeComponent();
            Events.UpdateContentEvent += UpdateContentEvent;
        }

        void UpdateContentEvent(object sender)
        {
            var control = sender as ResizableContentControl;
            if (control == null || control.Name != Name || stack2.Visibility != Visibility.Hidden)
                return;
            stack1.Visibility = Visibility.Collapsed;
            stack2.Visibility = Visibility.Visible;
            Loading.Visibility = Visibility.Collapsed;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            
            stack2.Visibility= Visibility.Hidden;
            Loading.Visibility=Visibility.Visible;
            Events.IncreaseSize(this);
        }

        private void ButtonBackClick(object sender, RoutedEventArgs e)
        {
            stack1.Visibility = Visibility.Visible;
            stack2.Visibility = Visibility.Collapsed;
        }
    }
}
