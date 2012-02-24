using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for SelfExpandablePanel.xaml
    /// </summary>
    public partial class SelfExpandablePanel : UserControl
    {
        public SelfExpandablePanel()
        {
            InitializeComponent();
            Events.UpdateControlStateEvent += OnUpdateStateEvent;
        }


        public static readonly DependencyProperty BaseTileSizeProperty =
            DependencyProperty.Register(
                "BaseTileSize",
                typeof(double),
                typeof(SelfExpandablePanel),
                new PropertyMetadata(100.0));

        public double BaseTileSize
        {
            get { return (double)GetValue(BaseTileSizeProperty); }
            set { SetValue(BaseTileSizeProperty, value); }
        }


        void OnUpdateStateEvent(object sender, State state)
        {
            var currentControl = sender as SelfExpandableControl;
            if (currentControl == null)
                return;
            for (var index = 0; index < RootCanvas.Children.Count; index++)
            {
                var control = RootCanvas.Children[index] as SelfExpandableControl;
                if (control == null || currentControl.Name == control.Name)
                {
                    currentControl.State = state;
                    if (currentControl.State == State.Activated && Panel.GetZIndex(currentControl) < 1)
                        Panel.SetZIndex(currentControl, Panel.GetZIndex(currentControl) + 1);
                }
                else
                {
                    control.State = state == State.Activated ? State.Deactivated : State.Normal;
                    Panel.SetZIndex(control, 0);
                }
            }
        }


        private void UpdateControls(object sender, RoutedEventArgs e)
        {
            Events.UpdateOriginalSize(BaseTileSize);
        }
    }
}
