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
    public partial class NewIndividualAccountView : SelfExpandableControl
    {


        public NewIndividualAccountView()
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

            var animation = AnimationFactory.CreateDoubleAnimation(StackPanelSearchResult, OpacityProperty, 1, 0, TimeSpan.FromMilliseconds(AnimationBeginTimeWidthGrowMs), TimeSpan.FromMilliseconds(AnimationWidthGrowTimeMs), EasingFunction);
            animation.Completed += StackPanelSearchResultFadeInAnimationCompleted;
            StackPanelSearchResult.BeginAnimation(OpacityProperty, animation);

            Events.UpdateControlState(this,State.Activated);
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
            Events.UpdateControlState(this, State.Activated);
        }

        void StackPanelFadeOutAnimationCompleted(object sender, EventArgs e)
        {
            StackPanelSearchResult.Visibility = Visibility.Collapsed;
            StackPanelSearchResult.Opacity = 1;
            StackPanelSearchResult.BeginAnimation(OpacityProperty, null);
        }


        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            StackPanelSearchResult.Visibility = Visibility.Collapsed;
            Events.UpdateControlState(this, State.Normal);
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
