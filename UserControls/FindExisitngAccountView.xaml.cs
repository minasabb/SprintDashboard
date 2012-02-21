using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using IQ.Core.Windows.Animation;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for FindExisitngAccountView.xaml
    /// </summary>
    public partial class FindExisitngAccountView : SelfExpandableControl
    {
        

        public FindExisitngAccountView()
        {
            InitializeComponent();
            SetupDesignTimeModel();
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

            Events.UpdateControlState(this,State.Active);
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

        void FindAccountButton_OnClick(object sender, RoutedEventArgs e)
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
            FindAccountButton.Visibility = Visibility.Hidden;
            FindAccountButton.Opacity = 1;
            FindAccountButton.BeginAnimation(OpacityProperty, null);

            FindAccountStackPanel.Opacity = 0;
            FindAccountStackPanel.Visibility = Visibility.Visible;
            FindAccountBorder.Visibility = Visibility.Visible;

            var animation = AnimationFactory.CreateDoubleAnimation(FindAccountStackPanel, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(200), easingFuction: EasingFunction);
            animation.Completed += FindAccountStackPanelFadeInAnimationCompleted;
            FindAccountStackPanel.BeginAnimation(OpacityProperty, animation);

            Loading.Visibility = Visibility.Collapsed;
        }

        void FindAccountStackPanelFadeInAnimationCompleted(object sender, EventArgs e)
        {
            FindAccountStackPanel.Opacity = 1;
            FindAccountStackPanel.BeginAnimation(OpacityProperty, null);
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var animation = AnimationFactory.CreateDoubleAnimation(FindAccountStackPanel, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(100), easingFuction: EasingFunction);
            animation.Completed += FindAccountStackPanelFadeOutAnimationCompleted;
            FindAccountStackPanel.BeginAnimation(OpacityProperty, animation);
            Events.UpdateControlState(this, State.Normal);
        }

        void FindAccountStackPanelFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            FindAccountStackPanel.Visibility = Visibility.Collapsed;
            FindAccountStackPanel.Opacity = 1;
            FindAccountStackPanel.BeginAnimation(OpacityProperty, null);

            var animation = AnimationFactory.CreateDoubleAnimation(FindAccountBorder, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(500), easingFuction: EasingFunction);
            animation.Completed += FindAccountBorderFadeOutAnimationCompleted;
            FindAccountBorder.BeginAnimation(OpacityProperty, animation);

            FindAccountButton.Opacity = 0;
            FindAccountButton.Visibility = Visibility.Visible;
            var animation2 = AnimationFactory.CreateDoubleAnimation(FindAccountButton, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(700), easingFuction: EasingFunction);
            animation2.Completed += FindAccountButtonFadeInAnimationCompleted;
            FindAccountButton.BeginAnimation(OpacityProperty, animation2);
        }

        void FindAccountBorderFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            FindAccountBorder.Visibility = Visibility.Collapsed;
            Events.HideCurtain(this);
            FindAccountBorder.Opacity = 1;
            FindAccountBorder.BeginAnimation(OpacityProperty, null);
        }

        void FindAccountButtonFadeInAnimationCompleted(object sender, EventArgs e)
        {
            FindAccountBorder.Visibility = Visibility.Collapsed;
            FindAccountButton.Opacity = 1;
            FindAccountButton.BeginAnimation(OpacityProperty, null);
        }

        private void SetupDesignTimeModel()
        {
            var portInNumbers = new ObservableCollection<PortInNumber>();

            var alphaTeam = new PortInNumber("604-789-5656", "some text", "12234 -data");
            portInNumbers.Add(alphaTeam);
            var betaTeam = new PortInNumber("604-789-1234", "some text ...", "k890w -data");
            portInNumbers.Add(betaTeam);
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            DataContext = new AllNumbers { PortInNumbers = portInNumbers };
        }

        public class PortInNumber
        {
            #region Construction

            public PortInNumber(string id, string text, string data)
            {
                PhoneNumber = id;
                Text = text;
                Data = data;
            }

            #endregion

            #region Implementation of IPersonAge
            public string PhoneNumber { get; set; }
            public string Text { get; set; }
            public string Data { get; set; }

            #endregion
        }

        public class AllNumbers
        {
            #region Properties

            public ObservableCollection<PortInNumber> PortInNumbers { get; set; }

            #endregion
        }
    }
}
