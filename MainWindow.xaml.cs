using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using TextDashboard.UserControls;

namespace TextDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double MarginValue = 5;
        private const double SmallSizePercent = 0.5;
        private const double LargeSizePercent = 0.75;
        private const double PositionMovePercent = 0.25;
        private const double AnimationDuration = 0.5;
        private double _originalXpostion = MarginValue;
        private double _originalYpostion = MarginValue;
        //private Control _shrinkControl;
        private Control _growControl;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateControls(object sender, RoutedEventArgs e)
        {
            var centerPointX = RootCanvas.ActualWidth / 2;
            var centerPointY = RootCanvas.ActualHeight / 2;

            foreach (Control control in RootCanvas.Children)
            {
                control.BeginAnimation(Canvas.LeftProperty, null);
                control.BeginAnimation(Canvas.TopProperty, null);
                control.BeginAnimation(WidthProperty, null);
                control.BeginAnimation(HeightProperty, null);

                if (Canvas.GetLeft(control) > MarginValue)
                    Canvas.SetLeft(control, centerPointX + MarginValue);

                if (Canvas.GetTop(control) > MarginValue)
                    Canvas.SetTop(control, centerPointY + MarginValue);

                control.Width = RootCanvas.ActualWidth * SmallSizePercent - MarginValue * 2;
                control.Height = RootCanvas.ActualHeight * SmallSizePercent - MarginValue * 2;
            }
        }

        private void LoadControls(object sender, RoutedEventArgs e)
        {
            var centerPointX = RootCanvas.ActualWidth / 2;
            var centerPointY = RootCanvas.ActualHeight / 2;

            var firstControl = new FirstView { Width = RootCanvas.ActualWidth * SmallSizePercent - MarginValue * 2, Height = RootCanvas.ActualHeight * SmallSizePercent - MarginValue * 2 };
            firstControl.MouseDown += Grow;
            RootCanvas.Children.Add(firstControl);
            Canvas.SetLeft(firstControl, MarginValue);
            Canvas.SetTop(firstControl, MarginValue);

            var secondControl = new SecondView { Width = RootCanvas.ActualWidth * SmallSizePercent - MarginValue * 2, Height = RootCanvas.ActualHeight * SmallSizePercent - MarginValue * 2 };
            secondControl.MouseDown += Grow;
            RootCanvas.Children.Add(secondControl);
            Canvas.SetLeft(secondControl, centerPointX + MarginValue);
            Canvas.SetTop(secondControl, MarginValue);

            var thirdControl = new ThirdView { Width = RootCanvas.ActualWidth * SmallSizePercent - MarginValue * 2, Height = RootCanvas.ActualHeight * SmallSizePercent - MarginValue * 2 };
            thirdControl.MouseDown += Grow;
            RootCanvas.Children.Add(thirdControl);
            Canvas.SetLeft(thirdControl, MarginValue);
            Canvas.SetTop(thirdControl, centerPointY + MarginValue);

            var forthControl = new ForthView { Width = RootCanvas.ActualWidth * SmallSizePercent - MarginValue * 2, Height = RootCanvas.ActualHeight * SmallSizePercent - MarginValue * 2 };
            forthControl.MouseDown += Grow;
            RootCanvas.Children.Add(forthControl);
            Canvas.SetLeft(forthControl, centerPointX + MarginValue);
            Canvas.SetTop(forthControl, centerPointY + MarginValue); 
        }

        private void Grow(object sender, RoutedEventArgs routedEventArgs)
        {

            var content = (Control)sender;

            content.MouseRightButtonDown += Shrink;

            foreach (Control control in RootCanvas.Children)
            {
                if (control != content && _growControl == control)
                {
                    var originalX = MarginValue;
                    if (Math.Abs(Canvas.GetLeft(_growControl) - MarginValue) > 1)
                        originalX = Canvas.GetLeft(_growControl) +
                                     RootCanvas.ActualWidth * PositionMovePercent;
                    var originalY = MarginValue;
                    if (Math.Abs(Canvas.GetTop(_growControl) - MarginValue) > 1)
                        originalY = Canvas.GetTop(_growControl) +
                                    RootCanvas.ActualHeight * PositionMovePercent;
                    Shrink(control, originalX,originalY);
                }
            }
            
           Grow(content);

        }

        private void Grow(Control content)
        {
            _growControl = content;

            var controlWidth = content.ActualWidth;
            var controlHeight = content.ActualHeight;

            foreach (Control control in RootCanvas.Children)
                Panel.SetZIndex(control, 0);

            Panel.SetZIndex(content, Panel.GetZIndex(content) + 1);

            var newControlWidth = RootCanvas.ActualWidth * LargeSizePercent - MarginValue * 2;
            var newControlHeight = RootCanvas.ActualHeight * LargeSizePercent - MarginValue * 2;

            var widthDiff = newControlWidth - controlWidth;

            var widthMove = RootCanvas.ActualWidth * PositionMovePercent;
            var heightMove = RootCanvas.ActualHeight * PositionMovePercent;

            if (widthDiff < 1)
                return;

            //content.Width = newControlWidth;
            //content.Height = newControlHeight;
            //if (Canvas.GetLeft(content) > Margin)
            //Canvas.SetLeft(content, Canvas.GetLeft(content) - widthDiff);
            //if (Canvas.GetTop(content) > Margin)
            //Canvas.SetTop(content, Canvas.GetTop(content) - heightDiff);

            _originalXpostion = Canvas.GetLeft(content);
            _originalYpostion = Canvas.GetTop(content);
            //Canvas positions
            if (Canvas.GetLeft(content) > MarginValue)
            {
                var buttonAnimation = new DoubleAnimation
                {
                    From = Canvas.GetLeft(content),
                    To = Canvas.GetLeft(content) - widthMove,
                    Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
                };
                //buttonAnimation.Completed += CanvasLargeLeftAnimationCompleted;
                content.BeginAnimation(Canvas.LeftProperty, buttonAnimation);

            }

            if (Canvas.GetTop(content) > MarginValue)
            {
                var buttonAnimation = new DoubleAnimation
                {
                    From = Canvas.GetTop(content),
                    To = Canvas.GetTop(content) - heightMove,
                    Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
                };
                //buttonAnimation.Completed += CanvasLargeTopAnimationCompleted;
                content.BeginAnimation(Canvas.TopProperty, buttonAnimation);

            }


            var myDoubleAnimation = new DoubleAnimation { From = controlHeight, To = newControlHeight, Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration)) };
            var myDoubleAnimation2 = new DoubleAnimation { From = controlWidth, To = newControlWidth, Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration)) };
            //myDoubleAnimation.Completed += HeightLargeAnimationCompleted;
            myDoubleAnimation2.Completed += WidthLargeAnimationCompleted;

            content.BeginAnimation(HeightProperty, myDoubleAnimation);
            content.BeginAnimation(WidthProperty, myDoubleAnimation2);

            
        }

        private void Shrink(object sender, RoutedEventArgs routedEventArgs)
        {
            var content = (Control)sender;

            Shrink(content,_originalXpostion,_originalYpostion);
        }


        private void Shrink(Control content,double originalX,double originalY)
        {
            //_shrinkControl = content;

            var controlWidth = content.ActualWidth;
            var controlHeight = content.ActualHeight;

            var newControlWidth = RootCanvas.ActualWidth * SmallSizePercent - MarginValue * 2;
            var newControlHeight = RootCanvas.ActualHeight * SmallSizePercent - MarginValue * 2;

            var widthDiff = controlWidth - newControlWidth;

            var widthMove = RootCanvas.ActualWidth * PositionMovePercent;
            var heightMove = RootCanvas.ActualHeight * PositionMovePercent;

            if (widthDiff < 1)
                return;

            //content.Width = newControlWidth;
            //content.Height = newControlHeight;
            //if (originalXpostion > Margin)
            //Canvas.SetLeft(content,Canvas.GetLeft(content) + widthDiff);
            //if (originalYpostion > Margin)
            //Canvas.SetTop(content, Canvas.GetTop(content) + heightDiff);
            var myDoubleAnimation = new DoubleAnimation { From = controlHeight, To = newControlHeight, Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration)) };
            var myDoubleAnimation2 = new DoubleAnimation { From = controlWidth, To = newControlWidth, Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration)) };

            content.BeginAnimation(HeightProperty, myDoubleAnimation);
            content.BeginAnimation(WidthProperty, myDoubleAnimation2);
            //myDoubleAnimation.Completed += HeightSmallAnimationCompleted;
            //myDoubleAnimation2.Completed += WidthSmallAnimationCompleted;

            //Canvas positions
            if (originalX > MarginValue)
            {
                var buttonAnimation = new DoubleAnimation
                {
                    From = Canvas.GetLeft(content),
                    To = Canvas.GetLeft(content) + widthMove,
                    Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
                };
                //buttonAnimation.Completed += CanvasSmallLeftAnimationCompleted;
                content.BeginAnimation(Canvas.LeftProperty, buttonAnimation);

            }

            if (originalY > MarginValue)
            {
                var buttonAnimation = new DoubleAnimation
                {
                    From = Canvas.GetTop(content),
                    To = Canvas.GetTop(content) + heightMove,
                    Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
                };
                //buttonAnimation.Completed += CanvasSmallTopAnimationCompleted;
                content.BeginAnimation(Canvas.TopProperty, buttonAnimation);

            }
        }

        //void CanvasLargeLeftAnimationCompleted(object sender, EventArgs e)
        //{
        //    var left = _originalXpostion - (RootCanvas.ActualWidth * PositionMovePercent); 
        //    _growControl.BeginAnimation(Canvas.LeftProperty, null);
        //    Canvas.SetLeft(_growControl, left);
        //}

        //void CanvasLargeTopAnimationCompleted(object sender, EventArgs e)
        //{

        //    var top = _originalYpostion - (RootCanvas.ActualHeight * PositionMovePercent); 
        //    _growControl.BeginAnimation(Canvas.TopProperty, null);
        //    Canvas.SetTop(_growControl,top);
        //}

        //void CanvasSmallLeftAnimationCompleted(object sender, EventArgs e)
        //{
        //    var left = RootCanvas.ActualWidth/2 + MarginValue;
        //    _shrinkControl.BeginAnimation(Canvas.LeftProperty, null);
        //    Canvas.SetLeft(_shrinkControl, left);
        //}

        //void CanvasSmallTopAnimationCompleted(object sender, EventArgs e)
        //{

        //    var top = RootCanvas.ActualHeight/2 + MarginValue;
        //    _shrinkControl.BeginAnimation(Canvas.TopProperty, null);
        //    Canvas.SetTop(_shrinkControl, top);
        //}
        void WidthLargeAnimationCompleted(object sender, EventArgs e)
        {
            //var width = RootCanvas.ActualWidth * LargeSizePercent - MarginValue * 2;
            //_growControl.BeginAnimation(WidthProperty, null);
            //_growControl.Width = width;
            _growControl.MouseRightButtonDown += Shrink;
        }

        //void HeightLargeAnimationCompleted(object sender, EventArgs e)
        //{
        //    var height = RootCanvas.ActualHeight * LargeSizePercent - MarginValue * 2;
        //    _growControl.BeginAnimation(HeightProperty, null);
        //    _growControl.Height = height;
        //}

        //void WidthSmallAnimationCompleted(object sender, EventArgs e)
        //{
        //    var width = RootCanvas.ActualWidth * SmallSizePercent - MarginValue * 2;
        //    _shrinkControl.BeginAnimation(WidthProperty, null);
        //    _shrinkControl.Width = width;
        //}

        //void HeightSmallAnimationCompleted(object sender, EventArgs e)
        //{
        //    var height = RootCanvas.ActualHeight * SmallSizePercent - MarginValue * 2;
        //    _shrinkControl.BeginAnimation(HeightProperty, null);
        //    _shrinkControl.Height = height;
        //}

  
    }
}
