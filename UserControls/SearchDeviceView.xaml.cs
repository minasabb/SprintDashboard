using System;
using System.Windows;
using System.Windows.Controls;
using IQ.Core.Windows.Animation;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for SearchDeviceView.xaml
    /// </summary>
    public partial class SearchDeviceView : SelfExpandableControl
    {


        public SearchDeviceView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadingResult.Visibility=Visibility.Visible;
            StackPanelSearchResult.Visibility = Visibility.Hidden;
            StackPanelSearchResult.Opacity = 0;
            StackPanelSearchResult.Visibility = Visibility.Visible;

            var animation = AnimationFactory.CreateDoubleAnimation(StackPanelSearchResult, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(600), easingFuction: EasingFunction);
            animation.Completed += StackPanelSearchResultFadeInAnimationCompleted;
            StackPanelSearchResult.BeginAnimation(OpacityProperty, animation);
            Events.UpdateControlState(this, State.Active);
        }

        void StackPanelSearchResultFadeInAnimationCompleted(object sender, EventArgs e)
        {
            LoadingResult.Visibility = Visibility.Collapsed;
            StackPanelSearchResult.Opacity = 1;
            StackPanelSearchResult.BeginAnimation(OpacityProperty, null);
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var animation = AnimationFactory.CreateDoubleAnimation(StackPanelSearchResult, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(200), easingFuction: EasingFunction);
            animation.Completed += StackPanelFadeOutAnimationCompleted;
            StackPanelSearchResult.BeginAnimation(OpacityProperty, animation);
            Events.UpdateControlState(this, State.Active);
        }

        void StackPanelFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            StackPanelSearchResult.Visibility=Visibility.Collapsed;
            StackPanelSearchResult.Opacity = 1;
            StackPanelSearchResult.BeginAnimation(OpacityProperty, null);
        }

        void DeviceScanButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) 
                return;

            Events.ShowCurtain(this);

            Loading.Visibility = Visibility.Visible;

            var animation = AnimationFactory.CreateDoubleAnimation(button, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(200), easingFuction: EasingFunction);
            animation.Completed += ButtonClickedAnimationCompleted;
            button.BeginAnimation(OpacityProperty, animation);
            Events.UpdateControlState(this, State.Active);
        }

        void ButtonClickedAnimationCompleted(object sender, EventArgs e)
        {
            ScanDeviceButton.Visibility=Visibility.Hidden;
            ScanDeviceButton.Opacity = 1;
            ScanDeviceButton.BeginAnimation(OpacityProperty, null);

            ScanDeviceStackPanel.Opacity = 0;
            ScanDeviceStackPanel.Visibility=Visibility.Visible;
            ScanDeviceBorder.Visibility = Visibility.Visible;

            var animation = AnimationFactory.CreateDoubleAnimation(ScanDeviceStackPanel, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(200), easingFuction: EasingFunction);
            animation.Completed += ScanDeviceStackPanelFadeInAnimationCompleted;
            ScanDeviceStackPanel.BeginAnimation(OpacityProperty, animation);

            Loading.Visibility = Visibility.Collapsed;
        }

        void ScanDeviceStackPanelFadeInAnimationCompleted(object sender, EventArgs e)
        {
            ScanDeviceStackPanel.Opacity = 1;
            ScanDeviceStackPanel.BeginAnimation(OpacityProperty, null);
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var animation = AnimationFactory.CreateDoubleAnimation(ScanDeviceStackPanel, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(100), easingFuction: EasingFunction);
            animation.Completed += ScanDeviceStackPanelFadeOutAnimationCompleted;
            ScanDeviceStackPanel.BeginAnimation(OpacityProperty, animation);
            Events.UpdateControlState(this, State.Normal);
            
        }

        void ScanDeviceStackPanelFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            ScanDeviceStackPanel.Visibility=Visibility.Collapsed;
            ScanDeviceStackPanel.Opacity = 1;
            ScanDeviceStackPanel.BeginAnimation(OpacityProperty,null);

            var animation = AnimationFactory.CreateDoubleAnimation(ScanDeviceBorder, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(500), easingFuction: EasingFunction);
            animation.Completed += ScanDeviceBorderFadeOutAnimationCompleted;
            ScanDeviceBorder.BeginAnimation(OpacityProperty, animation);
            
            ScanDeviceButton.Opacity = 0;
            ScanDeviceButton.Visibility = Visibility.Visible;
            var animation2 = AnimationFactory.CreateDoubleAnimation(ScanDeviceButton, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(700), easingFuction: EasingFunction);
            animation2.Completed += ScanDeviceButtonFadeInAnimationCompleted;
            ScanDeviceButton.BeginAnimation(OpacityProperty, animation2);
        }

        void ScanDeviceBorderFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            ScanDeviceBorder.Visibility = Visibility.Collapsed;
            Events.HideCurtain(this);
            ScanDeviceBorder.Opacity = 1;
            ScanDeviceBorder.BeginAnimation(OpacityProperty, null);
        }

        void ScanDeviceButtonFadeInAnimationCompleted(object sender, EventArgs e)
        {
            ScanDeviceBorder.Visibility =Visibility.Collapsed;
            ScanDeviceButton.Opacity = 1;
            ScanDeviceButton.BeginAnimation(OpacityProperty, null);
        }

    }
}
