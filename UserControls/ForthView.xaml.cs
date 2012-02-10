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
    /// Interaction logic for ForthView.xaml
    /// </summary>
    public partial class ForthView : ResizableContentControl
    {

        public ForthView()
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
        }

        void StackPanelFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            StackPanelSearchResult.Visibility = Visibility.Collapsed;
            StackPanelSearchResult.Opacity = 1;
            StackPanelSearchResult.BeginAnimation(OpacityProperty, null);
        }

        void CheckNumberButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            Events.ShowCurtain(this);

            Loading.Visibility = Visibility.Visible;

            var animation = AnimationFactory.CreateDoubleAnimation(button, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(200), easingFuction: EasingFunction);
            animation.Completed += ButtonClickedAnimationCompleted;
            button.BeginAnimation(OpacityProperty, animation);
        }

        void ButtonClickedAnimationCompleted(object sender, EventArgs e)
        {
            CheckNumberButton.Visibility = Visibility.Hidden;
            CheckNumberButton.Opacity = 1;
            CheckNumberButton.BeginAnimation(OpacityProperty, null);

            CheckNumberStackPanel.Opacity = 0;
            CheckNumberStackPanel.Visibility = Visibility.Visible;
            CheckNumberBorder.Visibility = Visibility.Visible;

            var animation = AnimationFactory.CreateDoubleAnimation(CheckNumberStackPanel, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(200), easingFuction: EasingFunction);
            animation.Completed += CheckNumberStackPanelFadeInAnimationCompleted;
            CheckNumberStackPanel.BeginAnimation(OpacityProperty, animation);

            Loading.Visibility = Visibility.Collapsed;
        }

        void CheckNumberStackPanelFadeInAnimationCompleted(object sender, EventArgs e)
        {
            CheckNumberStackPanel.Opacity = 1;
            CheckNumberStackPanel.BeginAnimation(OpacityProperty, null);
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var animation = AnimationFactory.CreateDoubleAnimation(CheckNumberStackPanel, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(100), easingFuction: EasingFunction);
            animation.Completed += CheckNumberStackPanelFadeOutAnimationCompleted;
            CheckNumberStackPanel.BeginAnimation(OpacityProperty, animation);

        }

        void CheckNumberStackPanelFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            CheckNumberStackPanel.Visibility = Visibility.Collapsed;
            CheckNumberStackPanel.Opacity = 1;
            CheckNumberStackPanel.BeginAnimation(OpacityProperty, null);

            var animation = AnimationFactory.CreateDoubleAnimation(CheckNumberBorder, OpacityProperty, 0, 1, durationSpan: TimeSpan.FromMilliseconds(500), easingFuction: EasingFunction);
            animation.Completed += CheckNumberBorderFadeOutAnimationCompleted;
            CheckNumberBorder.BeginAnimation(OpacityProperty, animation);

            CheckNumberButton.Opacity = 0;
            CheckNumberButton.Visibility = Visibility.Visible;
            var animation2 = AnimationFactory.CreateDoubleAnimation(CheckNumberButton, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(700), easingFuction: EasingFunction);
            animation2.Completed += CheckNumberButtonFadeInAnimationCompleted;
            CheckNumberButton.BeginAnimation(OpacityProperty, animation2);
        }

        void CheckNumberBorderFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            CheckNumberBorder.Visibility = Visibility.Collapsed;
            Events.HideCurtain(this);
            CheckNumberBorder.Opacity = 1;
            CheckNumberBorder.BeginAnimation(OpacityProperty, null);
        }

        void CheckNumberButtonFadeInAnimationCompleted(object sender, EventArgs e)
        {
            CheckNumberBorder.Visibility = Visibility.Collapsed;
            CheckNumberButton.Opacity = 1;
            CheckNumberButton.BeginAnimation(OpacityProperty, null);
        }
        
        private void SetupDesignTimeModel()
        {
            var portInNumbers = new ObservableCollection<PortInNumber>();

            var alphaTeam = new PortInNumber("604-789-5656", "some text", "12234 -data");
            portInNumbers.Add(alphaTeam);
            var betaTeam = new PortInNumber("604-789-1234", "some text ...", "k890w -data");
            portInNumbers.Add(betaTeam);
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
