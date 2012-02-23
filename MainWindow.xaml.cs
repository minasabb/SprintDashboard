using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;
using TextDashboard.UserControls;

namespace TextDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double MarginValue = 5;
        //private const double AnimationDuration = 0.2;
        //private const double OpacityChange = 0.8;

        readonly List<ControlPlacement> _contolList;

        public MainWindow()
        {
            InitializeComponent();
            //_contolList = new List<ControlPlacement>
            //                 {
            //                     new ControlPlacement {Name="V1",HeightPercentage = 0.4, WidthPercentage = 0.2, InitialXPercentage = 0, InitialYPercentage = 0,GrowPercentage = 0.50, ViewControl = new SearchDeviceView {Margin = new Thickness(MarginValue)}}, 
            //                     new ControlPlacement {Name="V2",HeightPercentage = 0.4, WidthPercentage = 0.8, InitialXPercentage = 0.2, InitialYPercentage = 0,GrowPercentage = 0.50, ViewControl = new FindExisitngAccountView {Margin = new Thickness(MarginValue)}}, 
            //                     new ControlPlacement {Name="V3",HeightPercentage = 0.35, WidthPercentage = 0.5, InitialXPercentage = 0, InitialYPercentage = 0.4,GrowPercentage = 0.25, ViewControl = new ThirdView {Margin = new Thickness(MarginValue)}}, 
            //                     new ControlPlacement {Name="V4",HeightPercentage = 0.35, WidthPercentage = 0.5, InitialXPercentage = 0.5, InitialYPercentage = 0.4,GrowPercentage = 0.20, ViewControl = new ThirdView {Margin = new Thickness(MarginValue)}}, 
            //                     new ControlPlacement {Name="V5",HeightPercentage = 0.25, WidthPercentage = 0.8, InitialXPercentage = 0, InitialYPercentage = 0.75,GrowPercentage = .80, ViewControl = new ForthView {Margin = new Thickness(MarginValue)}},
            //                     new ControlPlacement {Name="V6",HeightPercentage = 0.25, WidthPercentage = 0.2, InitialXPercentage = .8, InitialYPercentage = 0.75,GrowPercentage = .30, ViewControl = new ForthView {Margin = new Thickness(MarginValue)}}
            //                 };
            //_contolList = new List<ControlPlacement>
            //                 {
            //                     new ControlPlacement {Name="V1",HeightPercentage = 0.6, WidthPercentage = 0.4, InitialXPercentage = 0, InitialYPercentage = 0,GrowPercentage = 0.50, ViewControl = new SearchDeviceView()}, 
            //                     new ControlPlacement {Name="V2",HeightPercentage = 0.6, WidthPercentage = 0.6, InitialXPercentage = 0.4, InitialYPercentage = 0,GrowPercentage = 0.50, ViewControl = new FindExisitngAccountView()}, 
            //                     new ControlPlacement {Name="V3",HeightPercentage = 0.4, WidthPercentage = 0.5, InitialXPercentage = 0, InitialYPercentage = 0.6,GrowPercentage = 0.25, ViewControl = new ThirdView()}, 
            //                     new ControlPlacement {Name="V4",HeightPercentage = 0.4, WidthPercentage = 0.5, InitialXPercentage = 0.5, InitialYPercentage = 0.6,GrowPercentage = 0.20, ViewControl = new ForthView()}, 
            //                 };

            //_contolList = new List<ControlPlacement>
            //                 {
            //                     new ControlPlacement {Name="V1",HeightPercentage = 0.5, WidthPercentage = 0.5, InitialXPercentage = 0, InitialYPercentage = 0,GrowPercentage = 0.50, ViewControl = new SearchDeviceView {Margin = new Thickness(MarginValue)}}, 
            //                     new ControlPlacement {Name="V2",HeightPercentage = 0.5, WidthPercentage = 0.5, InitialXPercentage = 0.5, InitialYPercentage = 0,GrowPercentage = 0.50, ViewControl = new FindExisitngAccountView {Margin = new Thickness(MarginValue)}}, 
            //                     new ControlPlacement {Name="V3",HeightPercentage = 0.5, WidthPercentage = 0.5, InitialXPercentage = 0, InitialYPercentage = 0.5,GrowPercentage = 0.25, ViewControl = new ThirdView {Margin = new Thickness(MarginValue)}}, 
            //                     new ControlPlacement {Name="V4",HeightPercentage = 0.5, WidthPercentage = 0.5, InitialXPercentage = 0.5, InitialYPercentage = 0.5,GrowPercentage = 0.20, ViewControl = new ForthView {Margin = new Thickness(MarginValue)}}, 
            //                 };

            Events.MoveControlToTopEvent += OnMoveControlToTopEvent;
            //Events.HideCurtainEvent += OnHideCurtainEvent;
            //Events.ShowCurtainEvent += OnShowCurtainEvent;
            Events.UpdateControlStateEvent += OnUpdateStateEvent;
        }

        void OnUpdateStateEvent(object sender, State state)
        {
            var currentControl = sender as SelfExpandableControl;
            if(currentControl==null)
                return;
            for (var index = 0; index < RootCanvas.Children.Count; index++)
            {
                var control = RootCanvas.Children[index] as SelfExpandableControl;
                if (control == null || currentControl.Name == control.Name)
                    currentControl.State = state;
                else
                    control.State = state == State.Activated ? State.Deactivated : State.Normal;
            }
        }

        void OnShowCurtainEvent(object sender)
        {
            CurtainScreenBorder.Visibility = Visibility.Visible;
        }

        void OnHideCurtainEvent(object sender)
        {
            for (var index = 0; index < RootCanvas.Children.Count; index++)
            {
                var control = RootCanvas.Children[index] as Control;
                if (control != null) Panel.SetZIndex(control, 0);
            }
            CurtainScreenBorder.Visibility = Visibility.Collapsed;
        }

        void OnMoveControlToTopEvent(object control)
        {
            var view = control as Control;
            if (view != null)
                UpdateZOrder(view);
        }

        void UpdateZOrder(Control currentControl)
        {
            for (var index = 0; index < RootCanvas.Children.Count; index++)
            {
                var control = RootCanvas.Children[index] as Control;
                if (control == null || currentControl.Name == control.Name)
                {
                    if (Panel.GetZIndex(currentControl)<1)
                        Panel.SetZIndex(currentControl, Panel.GetZIndex(currentControl) + 1);
                    continue;
                }
                Panel.SetZIndex(control, 0);
            }
        }

        private void UpdateControls(object sender, RoutedEventArgs e)
        {
            //for (var index = 0; index < RootCanvas.Children.Count; index++)
            //{
            //    var control = RootCanvas.Children[index] as Control;
            //    if (control == null) continue;
            //    UpdateControlPlacement(control, index);
            //}
            Events.UpdateOriginalSize();
        }

        private void UpdateControlPlacement(Control control,int index)
        {
            control.BeginAnimation(Canvas.LeftProperty, null);
            control.BeginAnimation(Canvas.TopProperty, null);
            control.BeginAnimation(WidthProperty, null);
            control.BeginAnimation(HeightProperty, null);

            control.Width = RootCanvas.ActualWidth * _contolList[index].WidthPercentage - MarginValue * 2;
            control.Height = RootCanvas.ActualHeight * _contolList[index].HeightPercentage - MarginValue * 2;
            Canvas.SetLeft(control, _contolList[index].InitialXPercentage * RootCanvas.ActualWidth + MarginValue);
            Canvas.SetTop(control, _contolList[index].InitialYPercentage * RootCanvas.ActualHeight + MarginValue);
        }

        private void LoadControls(object sender, RoutedEventArgs e)
        {
            foreach (var controlPlacement in _contolList)
            {
                controlPlacement.ViewControl.Width = RootCanvas.ActualWidth * controlPlacement.WidthPercentage - MarginValue * 2;
                controlPlacement.ViewControl.Height = RootCanvas.ActualHeight * controlPlacement.HeightPercentage - MarginValue * 2;
                controlPlacement.ViewControl.Name = controlPlacement.Name;
                RegisterName(controlPlacement.Name, controlPlacement.ViewControl);
                RootCanvas.Children.Add(controlPlacement.ViewControl);
                Canvas.SetLeft(controlPlacement.ViewControl, controlPlacement.InitialXPercentage * RootCanvas.ActualWidth + MarginValue);
                Canvas.SetTop(controlPlacement.ViewControl, controlPlacement.InitialYPercentage * RootCanvas.ActualHeight + MarginValue);
            }
        }

        //private void Grow(object sender, RoutedEventArgs routedEventArgs)
        //{

        //    var content = (Control)sender;

        //    content.MouseRightButtonDown += Shrink;

        //    Grow(content);

        //}

        //private void ShrinkControls(Control currentControl)
        //{
        //    for (var index = 0; index < RootCanvas.Children.Count; index++)
        //    {
        //        var control = RootCanvas.Children[index] as Control;
        //        if (control == null || currentControl.Name == control.Name)
        //            continue;

        //        Panel.SetZIndex(control, 0);

        //        var originalWidth = RootCanvas.ActualWidth * _contolList[index].WidthPercentage - MarginValue * 2;
        //        if (control.Width != originalWidth)
        //            Shrink(control);

        //    }
        //}

        //private void Grow(Control control,double percent=0)
        //{
        //    var controlPlacement = _contolList.FirstOrDefault(c => c.Name == control.Name);
        //    if (controlPlacement == null)
        //        return;

        //    if (percent == 0)
        //        percent = controlPlacement.GrowPercentage;

        //    var controlWidth = control.ActualWidth;
        //    var controlHeight = control.ActualHeight;

        //    Panel.SetZIndex(control, Panel.GetZIndex(control) + 1);

        //    var newControlWidth = control.ActualWidth * (1 + percent);
        //    var newControlHeight = control.ActualHeight * (1 + percent);

        //    if (newControlWidth > RootCanvas.ActualWidth - MarginValue * 2)
        //        newControlWidth = RootCanvas.ActualWidth - MarginValue * 2;

        //    if (newControlHeight > RootCanvas.ActualHeight - MarginValue * 2)
        //        newControlHeight = RootCanvas.ActualHeight - MarginValue * 2;


        //    var widthMove = control.ActualWidth * percent;
        //    var heightMove = control.ActualHeight * percent;

        //    var originalWidth = RootCanvas.ActualWidth * controlPlacement.WidthPercentage - MarginValue * 2;
        //    if (widthMove > originalWidth * percent)
        //        return;

        //    var newX = Canvas.GetLeft(control) - widthMove;
        //    if (newX < MarginValue)
        //        newX = MarginValue;

        //    var newY = Canvas.GetTop(control) - heightMove;
        //    if (newY < MarginValue)
        //        newY = MarginValue;

        //    //Canvas positions
        //    var pointXAnimation = new DoubleAnimation
        //    {
        //        From = Canvas.GetLeft(control),
        //        To = newX,
        //        Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
        //    };
        //    control.BeginAnimation(Canvas.LeftProperty, pointXAnimation);

        //    var pointYAnimation = new DoubleAnimation
        //    {
        //        From = Canvas.GetTop(control),
        //        To = newY,
        //        Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
        //    };
        //    control.BeginAnimation(Canvas.TopProperty, pointYAnimation);


        //    var myDoubleAnimation = new DoubleAnimation { From = controlHeight, To = newControlHeight, Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration)) };
        //    var myDoubleAnimation2 = new DoubleAnimation { From = controlWidth, To = newControlWidth, Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration)) };

        //    control.BeginAnimation(HeightProperty, myDoubleAnimation);
        //    control.BeginAnimation(WidthProperty, myDoubleAnimation2);



        //    ShrinkControls(control);
        //}

        //private void Shrink(object sender, RoutedEventArgs routedEventArgs)
        //{
        //    var content = (Control)sender;

        //    Shrink(content);
        //}


        //private void Shrink(Control control)
        //{
        //    var controlPlacement = _contolList.Where(c => c.Name == control.Name).FirstOrDefault();
        //    if(controlPlacement==null)
        //        return;

        //    var originalWidth = RootCanvas.ActualWidth * controlPlacement.WidthPercentage - MarginValue * 2;
        //    if (control.Width == originalWidth)
        //        return;

        //    //for (var index = 0; index < RootCanvas.Children.Count; index++)
        //    //{
        //    //    var otherControls = RootCanvas.Children[index] as Control;
        //    //    if (otherControls == null) continue;
        //    //    otherControls.BeginAnimation(OpacityProperty, null);
        //    //    otherControls.Opacity = 1;
        //    //}

        //    var newControlWidth = RootCanvas.ActualWidth * controlPlacement.WidthPercentage - MarginValue * 2;
        //    var newControlHeight = RootCanvas.ActualHeight * controlPlacement.HeightPercentage - MarginValue * 2;
        //    var newX = controlPlacement.InitialXPercentage * RootCanvas.ActualWidth + MarginValue;
        //    var newY = controlPlacement.InitialYPercentage * RootCanvas.ActualHeight + MarginValue;

        //    var controlWidth = control.ActualWidth;
        //    var controlHeight = control.ActualHeight;

        //    var myDoubleAnimation = new DoubleAnimation { From = controlHeight, To = newControlHeight, Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration)) };
        //    var myDoubleAnimation2 = new DoubleAnimation { From = controlWidth, To = newControlWidth, Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration)) };

        //    control.BeginAnimation(HeightProperty, myDoubleAnimation);
        //    control.BeginAnimation(WidthProperty, myDoubleAnimation2);

        //    //Canvas positions
        //    var pointXAnimation = new DoubleAnimation
        //    {
        //        From = Canvas.GetLeft(control),
        //        To = newX,
        //        Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
        //    };
        //    control.BeginAnimation(Canvas.LeftProperty, pointXAnimation);

        //    var pointYAnimation = new DoubleAnimation
        //    {
        //        From = Canvas.GetTop(control),
        //        To = newY,
        //        Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
        //    };
        //    control.BeginAnimation(Canvas.TopProperty, pointYAnimation);
        //}

  
    }

    public class ControlPlacement
    {
        public string Name { get; set; }
        public double WidthPercentage { get; set; }
        public double HeightPercentage { get; set; }
        public double InitialXPercentage { get; set; }
        public double InitialYPercentage { get; set; }
        public double GrowPercentage { get; set; }
        public Control ViewControl { get; set; }
    }
}
