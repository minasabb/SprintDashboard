using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using IQ.Core.Windows.Animation;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for ThirdView.xaml
    /// </summary>
    public partial class ThirdView : SelfExpandableControl
    {

        public ThirdView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadingResult.Visibility = Visibility.Visible;
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
            StackPanelSearchResult.Visibility = Visibility.Collapsed;
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
            UpgradeButton.Visibility = Visibility.Hidden;
            UpgradeButton.Opacity = 1;
            UpgradeButton.BeginAnimation(OpacityProperty, null);

            UpgradeStackPanel.Opacity = 0;
            UpgradeStackPanel.Visibility = Visibility.Visible;
            UpgradeBorder.Visibility = Visibility.Visible;

            var animation = AnimationFactory.CreateDoubleAnimation(UpgradeStackPanel, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(200), easingFuction: EasingFunction);
            animation.Completed += UpgradeStackPanelFadeInAnimationCompleted;
            UpgradeStackPanel.BeginAnimation(OpacityProperty, animation);

            Loading.Visibility = Visibility.Collapsed;
        }

        void UpgradeStackPanelFadeInAnimationCompleted(object sender, EventArgs e)
        {
            UpgradeStackPanel.Opacity = 1;
            UpgradeStackPanel.BeginAnimation(OpacityProperty, null);
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var animation = AnimationFactory.CreateDoubleAnimation(UpgradeStackPanel, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(100), easingFuction: EasingFunction);
            animation.Completed += UpgradeStackPanelFadeOutAnimationCompleted;
            UpgradeStackPanel.BeginAnimation(OpacityProperty, animation);
            Events.UpdateControlState(this, State.Normal);

        }

        void UpgradeStackPanelFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            UpgradeStackPanel.Visibility = Visibility.Collapsed;
            UpgradeStackPanel.Opacity = 1;
            UpgradeStackPanel.BeginAnimation(OpacityProperty, null);

            var animation = AnimationFactory.CreateDoubleAnimation(UpgradeBorder, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(500), easingFuction: EasingFunction);
            animation.Completed += UpgradeBorderFadeOutAnimationCompleted;
            UpgradeBorder.BeginAnimation(OpacityProperty, animation);

            UpgradeButton.Opacity = 0;
            UpgradeButton.Visibility = Visibility.Visible;
            var animation2 = AnimationFactory.CreateDoubleAnimation(UpgradeButton, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(700), easingFuction: EasingFunction);
            animation2.Completed += UpgradeButtonFadeInAnimationCompleted;
            UpgradeButton.BeginAnimation(OpacityProperty, animation2);
        }

        void UpgradeBorderFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            UpgradeBorder.Visibility = Visibility.Collapsed;
            Events.HideCurtain(this);
            UpgradeBorder.Opacity = 1;
            UpgradeBorder.BeginAnimation(OpacityProperty, null);
        }

        void UpgradeButtonFadeInAnimationCompleted(object sender, EventArgs e)
        {
            UpgradeBorder.Visibility = Visibility.Collapsed;
            UpgradeButton.Opacity = 1;
            UpgradeButton.BeginAnimation(OpacityProperty, null);
        }

    }
}
