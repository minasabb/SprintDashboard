using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using IQ.Core.Windows.Animation;
using TextDashboard.Resource;

namespace TextDashboard.Custom_Control
{
    [TemplatePart(Name = PartMainScrolViewer, Type = typeof(ScrollViewer))]
    public class ResizableContentControl: ContentControl
    {

        private const string PartMainScrolViewer = "PART_MainScrolViewer";

        bool _widthChanged;
        bool _heightChanged;
        double _currentXoffSet;
        double _currentYoffSet;
        double _currentScrollbarExtentWidth;
        double _currentScrollbarExtentHeight;
        const int AnimationWidthGrowTimeSpan = 800;
        const int AnimationHeightGrowTimeSpan = 1700;
        const int AnimationWidthShrinkTimeSpan = 400;
        const int AnimationHeightShrinkTimeSpan = 800;
        const int AnimationTranformNegativeValueTimeSpan = 800;
        const int AnimationTranformPositiveValueTimeSpan = 600;
        const int ExtraSpacing = 5;

        static ResizableContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizableContentControl), new FrameworkPropertyMetadata(typeof(ResizableContentControl)));
        }

        public ResizableContentControl()
        {
            SetResourceReference(StyleProperty, "ResizableContentControlStyle");
            _currentXoffSet = 0;
            _currentYoffSet = 0;
            CurrentMinWidth = ActualWidth;
            CurrentMinHeight = ActualHeight;
            RenderTransform = new TranslateTransform(_currentXoffSet, _currentYoffSet);

            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };

            Loaded+=ResizableContentControlLoaded;
           Events.UpdateOriginalSizeEvent+=EventsUpdateOriginalSizeEvent; 
        }

        public static readonly DependencyProperty OriginalPointProperty =
            DependencyProperty.Register(
                "OriginalPoint",
                typeof(Point),
                typeof(ResizableContentControl),
                new PropertyMetadata(new Point(0, 0)));

        public static readonly DependencyProperty CurrentPointProperty =
            DependencyProperty.Register(
                "CurrentPoint",
                typeof(Point),
                typeof(ResizableContentControl),
                new PropertyMetadata(new Point(0, 0)));

        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register(
                "EndPoint",
                typeof(Point),
                typeof(ResizableContentControl),
                new PropertyMetadata(new Point(0,0)));

        public static readonly DependencyProperty NewWidthProperty =
            DependencyProperty.Register(
                "NewWidth",
                typeof(double),
                typeof(ResizableContentControl),
                new UIPropertyMetadata(OnNewWidthChanged));


        public static readonly DependencyProperty CurrentMaxWidthProperty =
            DependencyProperty.Register(
                "CurrentMaxWidth",
                typeof(double),
                typeof(ResizableContentControl),
                new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty CurrentMinWidthProperty =
            DependencyProperty.Register(
                "CurrentMinWidth",
                typeof(double),
                typeof(ResizableContentControl),
                new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty NewHeightProperty =
            DependencyProperty.Register(
                "NewHeight",
                typeof(double),
                typeof(ResizableContentControl),
                new UIPropertyMetadata(OnNewHeightChanged));

        public static readonly DependencyProperty CurrentMaxHeightProperty =
            DependencyProperty.Register(
                "CurrentMaxHeight",
                typeof(double),
                typeof(ResizableContentControl),
                new UIPropertyMetadata(0.0));


        public static readonly DependencyProperty CurrentMinHeightProperty =
            DependencyProperty.Register(
                "CurrentMinHeight",
                typeof(double),
                typeof(ResizableContentControl),
                new UIPropertyMetadata(0.0));


        public static readonly DependencyProperty ParentWidthProperty =
            DependencyProperty.Register(
                "ParentWidth",
                typeof(double),
                typeof(ResizableContentControl),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty ParentHeightProperty =
            DependencyProperty.Register(
                "ParentHeight",
                typeof(double),
                typeof(ResizableContentControl),
                new PropertyMetadata(0.0));

        public Point OriginalPoint
        {
            get { return (Point)GetValue(OriginalPointProperty); }
            set { SetValue(OriginalPointProperty, value); }
        }

        public Point CurrentPoint
        {
            get { return (Point)GetValue(CurrentPointProperty); }
            set { SetValue(CurrentPointProperty, value); }
        }

        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        public double NewWidth
        {
            get { return (double)GetValue(NewWidthProperty); }
            set { SetValue(NewWidthProperty, value); }
        }

        public double CurrentMaxWidth
        {
            get { return (double)GetValue(CurrentMaxWidthProperty); }
            set { SetValue(CurrentMaxWidthProperty, value); }
        }

        public double CurrentMinWidth
        {
            get { return (double)GetValue(CurrentMinWidthProperty); }
            set { SetValue(CurrentMinWidthProperty, value); }
        }
        public double NewHeight
        {
            get { return (double)GetValue(NewHeightProperty); }
            set { SetValue(NewHeightProperty, value); }
        }
        public double CurrentMaxHeight
        {
            get { return (double)GetValue(CurrentMaxHeightProperty); }
            set { SetValue(CurrentMaxHeightProperty, value); }
        }
        public double CurrentMinHeight
        {
            get { return (double)GetValue(CurrentMinHeightProperty); }
            set { SetValue(CurrentMinHeightProperty, value); }
        }
        public double ParentWidth
        {
            get { return (double)GetValue(ParentWidthProperty); }
            set { SetValue(ParentWidthProperty, value); }
        }
        public double ParentHeight
        {
            get { return (double)GetValue(ParentHeightProperty); }
            set { SetValue(ParentHeightProperty, value); }
        }

        public IEasingFunction EasingFunction { get; set; }

        void EventsUpdateOriginalSizeEvent()
        {
            UpdateOrigianlSettings();
        }

        void ScrollerViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollbar = sender as ScrollViewer;
            if (scrollbar == null) return;

            if (Math.Abs(scrollbar.ExtentWidth - _currentScrollbarExtentWidth) > 1)
            {  
                Events.MoveControlToTop(this);
                NewWidth = scrollbar.ExtentWidth + ExtraSpacing;
                
            }

            if (Math.Abs(scrollbar.ExtentHeight - _currentScrollbarExtentHeight) > 1)
            {
                Events.MoveControlToTop(this);
                NewHeight = scrollbar.ExtentHeight + ExtraSpacing;
            }

            _currentScrollbarExtentWidth = scrollbar.ExtentWidth;
            _currentScrollbarExtentHeight = scrollbar.ExtentHeight;

        }

        void ResizableContentControlLoaded(object sender, RoutedEventArgs e)
        {
            UpdateOrigianlSettings();
        }

        static void OnNewWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (ResizableContentControl)sender;
            control.UpdateXTransform();

        }

        static void OnNewHeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (ResizableContentControl)sender;
            
           control.UpdateYTransform();
        }

        void UpdateXTransform()
        {
            _widthChanged = true;

            GetCurrentValues();

            if (NewWidth > CurrentMaxWidth)
            {
                NewWidth = CurrentMaxWidth;
               
            }

            if (NewWidth < CurrentMinWidth)
            {
                NewWidth = CurrentMinWidth;
                return;
            }

            var animationTimeSpan = AnimationWidthGrowTimeSpan;
            double animationTranformTimeSpan=AnimationTranformNegativeValueTimeSpan;
            KeySpline fromKeySpline;
            KeySpline toKeySpline;
            var diffValue = (NewWidth - ActualWidth);
            double valueChange;
                
            if( diffValue >= 0)
            {
                valueChange= GetNegativeChange(ParentWidth, EndPoint.X, diffValue, CurrentPoint.X);
                Console.WriteLine(valueChange);
                fromKeySpline = null;
                toKeySpline = new KeySpline(0.32, 0.13, 0.18, 1);
            }
            else
            {
                animationTimeSpan = AnimationWidthShrinkTimeSpan;
                animationTranformTimeSpan = AnimationTranformPositiveValueTimeSpan;
                valueChange = GetPositiveChange(ParentWidth, _currentXoffSet, NewWidth, OriginalPoint.X);
                fromKeySpline = null;
                toKeySpline = new KeySpline(0.38, 0.38, 0.15, 0.98);
            }
                               
            _currentXoffSet = _currentXoffSet + valueChange;

            ResizeWidth(animationTimeSpan,toKeySpline,fromKeySpline);

            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.XProperty, -_currentXoffSet, 0, durationSpan: TimeSpan.FromMilliseconds(animationTranformTimeSpan), easingFuction: EasingFunction,beginTimeSpan: TimeSpan.FromMilliseconds(50));
            positionAnimation.Completed += PositionXAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, positionAnimation);

        }

        void ResizeWidth(double animationTimeSpan,KeySpline toKeySpline,KeySpline fromKeySpline)
        {
            var widthChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, WidthProperty, toKeySpline, NewWidth, ActualWidth, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan), beginTimeSpan: TimeSpan.FromMilliseconds(100));
            widthChangeAnimation.Completed += WidthChangeAnimationCompleted;
            BeginAnimation(WidthProperty, widthChangeAnimation);
        }

        void PositionXAnimationCompleted(object sender, EventArgs e)
        {
            //if (!_heightChanged)
            //{
            //    RenderTransform = new TranslateTransform(-_currentXoffSet, -_currentYoffSet);
            //    BeginAnimation(TranslateTransform.XProperty, null);
            //}
            //_widthChanged = false;
        }

        void WidthChangeAnimationCompleted(object sender, EventArgs e)
        {
            Width = NewWidth > CurrentMaxWidth ? CurrentMaxWidth : NewWidth;
            BeginAnimation(WidthProperty, null);

            var scrollbar = GetTemplateChild(PartMainScrolViewer) as ScrollViewer;
            if (scrollbar != null) scrollbar.HorizontalScrollBarVisibility = scrollbar.ExtentWidth - ActualWidth > 5 ? ScrollBarVisibility.Auto : ScrollBarVisibility.Hidden;
        }

        void UpdateYTransform()
        {
            GetCurrentValues();


            if (NewHeight > CurrentMaxHeight)
            {    
                NewHeight = CurrentMaxHeight;
                return;
            }

            if (NewHeight < CurrentMinHeight)
            {
                NewHeight = CurrentMinHeight;
                return;
            }

            var diffValue = NewHeight - ActualHeight ;
            var animationTimeSpan = AnimationHeightGrowTimeSpan;
            double animationTranformTimeSpan = AnimationTranformNegativeValueTimeSpan;
            KeySpline fromKeySpline;
            KeySpline toKeySpline;
            double valueChange;
            if (diffValue >= 0)
            {
                valueChange=GetNegativeChange(ParentHeight, EndPoint.Y, diffValue, CurrentPoint.Y);
                fromKeySpline = new KeySpline(0.2, 0.16, 0.07, 1);
                toKeySpline = new KeySpline(0.32, 0.13, 0.18, 1);
            }
            else
            {
                animationTimeSpan = AnimationHeightShrinkTimeSpan;
                animationTranformTimeSpan = AnimationTranformPositiveValueTimeSpan;
                valueChange=GetPositiveChange(ParentHeight, _currentYoffSet, NewHeight, OriginalPoint.Y);
                fromKeySpline = new KeySpline(0.38, 0.38, 0.15, 0.98);
                toKeySpline = new KeySpline(0.23, 0.12, 0, 1);
            }

            _currentYoffSet = _currentYoffSet + valueChange;

            ResizeHeight(animationTimeSpan,toKeySpline,fromKeySpline);

            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.YProperty, -_currentYoffSet, 0, durationSpan: TimeSpan.FromMilliseconds(animationTranformTimeSpan), easingFuction: EasingFunction, beginTimeSpan: TimeSpan.FromMilliseconds(100));
            positionAnimation.Completed += PositionYAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.YProperty, positionAnimation);

            
            
        }

        void ResizeHeight(double animationTimeSpan,KeySpline toKeySpline,KeySpline fromKeySpline)
        {
            var heightChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, HeightProperty, toKeySpline, NewHeight, ActualHeight, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan), beginTimeSpan: TimeSpan.FromMilliseconds(100));
            heightChangeAnimation.Completed += HeightChangeAnimationCompleted;
            BeginAnimation(HeightProperty, heightChangeAnimation);
        }

        void PositionYAnimationCompleted(object sender, EventArgs e)
        {
            RenderTransform = new TranslateTransform(-_currentXoffSet, -_currentYoffSet);
            BeginAnimation(TranslateTransform.XProperty, null);
            BeginAnimation(TranslateTransform.YProperty, null);
            _heightChanged = false;
        }

        void HeightChangeAnimationCompleted(object sender, EventArgs e)
        {
            Height = NewHeight;
            BeginAnimation(HeightProperty, null);

            var scrollbar = GetTemplateChild(PartMainScrolViewer) as ScrollViewer;
            if (scrollbar != null) scrollbar.VerticalScrollBarVisibility = scrollbar.ExtentHeight - ActualHeight > 5 ? ScrollBarVisibility.Auto : ScrollBarVisibility.Hidden;

            _widthChanged = false;
            Events.UpdateContent(this);
        }

        private void GetCurrentValues()
        {
            var positionInParent = GetPositionInParent();

            CurrentPoint = new Point(positionInParent.X, positionInParent.Y);
            EndPoint = new Point(positionInParent.X + ActualWidth, positionInParent.Y + ActualHeight);
            ParentWidth = (double)Parent.GetValue(ActualWidthProperty);
            ParentHeight = (double)Parent.GetValue(ActualHeightProperty);
            CurrentMaxWidth = ParentWidth - Margin.Left - Margin.Right;
            CurrentMaxHeight = ParentHeight - Margin.Top - Margin.Bottom;

        }

        private Point GetPositionInParent()
        {
            var ancestor = Parent as Visual;
            if (ancestor != null)
            {
                var transform = TransformToAncestor(ancestor);
                return transform.Transform(new Point(0, 0));
            }

            return new Point(0, 0);
        }

        private double GetNegativeChange(double parentValue,double endValue, double differenceValue,double currentValue)
        {
            if (parentValue < (endValue + differenceValue))
            {
                differenceValue = Math.Abs(parentValue - (endValue + differenceValue));
                var maxNegativeChange = parentValue - currentValue;
                if(maxNegativeChange<0)
                    maxNegativeChange = 0;
                return differenceValue < maxNegativeChange ? differenceValue : maxNegativeChange;
            }

            if (parentValue - endValue < differenceValue)
                return differenceValue - (parentValue - endValue);

            return 0.0;
        }

        private double GetPositiveChange(double parentValue,double currentTransformValue ,double newValue, double originalValue)
        {
            if (parentValue < originalValue + newValue)
                return -(currentTransformValue - (originalValue + newValue - parentValue));
            return -currentTransformValue;
        }

        private void UpdateOrigianlSettings()
        {
            var positionInParent = GetPositionInParent();
            OriginalPoint = positionInParent;
            
            var scrollViewer = GetTemplateChild(PartMainScrolViewer) as ScrollViewer;

            if (scrollViewer != null)
            {
                scrollViewer.MinWidth = ActualWidth;
                scrollViewer.MinHeight = ActualHeight;

                _currentScrollbarExtentWidth = scrollViewer.ExtentWidth;
                _currentScrollbarExtentHeight = scrollViewer.ExtentHeight;
                scrollViewer.ScrollChanged += ScrollerViewerScrollChanged;
            }
        }

        

    }
}
