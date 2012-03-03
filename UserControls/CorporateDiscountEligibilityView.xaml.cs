using System;
using System.Windows;
using System.Windows.Controls;
using IQ.Core.Windows.Animation;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for CoverageView.xaml
    /// </summary>
    public partial class CorporateDiscountEligibilityView : SelfExpandableControl
    {


        public CorporateDiscountEligibilityView()
        {
            InitializeComponent();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    LoadingResult.Visibility=Visibility.Visible;
        //    StackPanelSearchResult.Visibility = Visibility.Hidden;
        //    StackPanelSearchResult.Opacity = 0;
        //    StackPanelSearchResult.Visibility = Visibility.Visible;

        //    var animation = AnimationFactory.CreateDoubleAnimation(StackPanelSearchResult, OpacityProperty, 1, 0, TimeSpan.FromMilliseconds(AnimationBeginTimeWidthGrowMs), TimeSpan.FromMilliseconds(AnimationWidthGrowTimeMs), EasingFunction);
        //    animation.Completed += StackPanelSearchResultFadeInAnimationCompleted;
        //    StackPanelSearchResult.BeginAnimation(OpacityProperty, animation);
        //    Events.UpdateControlState(this, State.Activated);
        //}

        //void StackPanelSearchResultFadeInAnimationCompleted(object sender, EventArgs e)
        //{
        //    LoadingResult.Visibility = Visibility.Collapsed;
        //    StackPanelSearchResult.Opacity = 1;
        //    StackPanelSearchResult.BeginAnimation(OpacityProperty, null);
        //}

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            //StackPanelSearchResult.Visibility = Visibility.Collapsed;
            Events.UpdateControlState(this, State.Normal);
        }



    }
}
