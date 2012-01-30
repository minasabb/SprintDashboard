using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IQ.Core.Windows.Animation;
using TextDashboard.Resource;

namespace TextDashboard.Custom_Control
{
    [TemplatePart(Name = PART_MainScrolViewer, Type = typeof(ScrollViewer))]
    public class ResizableContentControl: ContentControl
    {

        private const string PART_MainScrolViewer = "PART_MainScrolViewer";

        bool _widthChanged;
        bool _heightChanged;
        bool _updateOrigianlValues;
        double _currentXoffSet;
        double _currentYoffSet;
        double _currentScrollbarExtentWidth;
        double _currentScrollbarExtentHeight;

        const int AnimationTimeSpan = 100;

        static ResizableContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizableContentControl), new FrameworkPropertyMetadata(typeof(ResizableContentControl)));
        }

        public ResizableContentControl()
        {
            SetResourceReference(StyleProperty, "ResizableContentControlStyle");
            _currentXoffSet = 0;
            _currentYoffSet = 0;
            RenderTransform = new TranslateTransform(_currentXoffSet, _currentYoffSet);
           Loaded+=ResizableContentControlLoaded;
            SizeChanged+=ResizableContentControlSizeChanged;
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

        //Events
        void ResizableContentControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_updateOrigianlValues)
                UpdateOrigianlSettings();
            _updateOrigianlValues = false;
        }

        void EventsUpdateOriginalSizeEvent()
        {
            _updateOrigianlValues = true;
        }

        void ScrollerViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollbar = sender as ScrollViewer;
            if (scrollbar == null || (Math.Abs(scrollbar.ExtentWidth - _currentScrollbarExtentWidth) < 2 && Math.Abs(scrollbar.ExtentHeight - _currentScrollbarExtentHeight) < 2)) return;
            _currentScrollbarExtentWidth = scrollbar.ExtentWidth;
            _currentScrollbarExtentHeight = scrollbar.ExtentHeight;

            var scrollViewer = GetTemplateChild(PART_MainScrolViewer) as ScrollViewer;

            if (scrollViewer == null) return;

            Events.IncreaseSize(this);

            NewWidth = scrollViewer.ExtentWidth;
            NewHeight = scrollViewer.ExtentHeight;
            scrollViewer.MaxWidth = NewWidth;
            scrollViewer.MaxHeight = NewHeight;
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
            if (!control._widthChanged)
                control.UpdateYTransform();
            control._heightChanged = true;
        }

        void UpdateXTransform()
        {
            _widthChanged = true;
            GetCurrentValues();

            if (NewWidth > CurrentMaxWidth)
            {
                NewWidth = CurrentMaxWidth;
                return;
            }

            if (NewWidth < CurrentMinWidth)
            {
                NewWidth = CurrentMinWidth;
                return;
            }

            var diffValue = (NewWidth - ActualWidth);
            double valueChange = diffValue >= 0
                                     ? GetNegativeChange(ParentWidth, EndPoint.X, diffValue, CurrentPoint.X)
                                     : GetPositiveChange(_currentXoffSet, diffValue, OriginalPoint.X);

            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.XProperty, -valueChange, 0, durationSpan: TimeSpan.FromMilliseconds(AnimationTimeSpan));
            positionAnimation.Completed += PositionXAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, positionAnimation);

            if (diffValue >= 0)
                _currentXoffSet = _currentXoffSet + valueChange;
            else
                _currentXoffSet = _currentXoffSet - valueChange;
        }

        void ResizeWidth()
        {
            var widthChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, WidthProperty, NewWidth, ActualWidth, durationSpan: TimeSpan.FromMilliseconds(AnimationTimeSpan));
            widthChangeAnimation.Completed += WidthChangeAnimationCompleted;
            BeginAnimation(WidthProperty, widthChangeAnimation);
        }

        void PositionXAnimationCompleted(object sender, EventArgs e)
        {
            if (!_heightChanged)
            {
                RenderTransform = new TranslateTransform(-_currentXoffSet, -_currentYoffSet);
                BeginAnimation(TranslateTransform.XProperty, null);
            }
            _widthChanged = false;

            ResizeWidth();
            
        }

        void WidthChangeAnimationCompleted(object sender, EventArgs e)
        {
            Width = NewWidth > CurrentMaxWidth ? CurrentMaxWidth : NewWidth;
            BeginAnimation(WidthProperty, null);

            if (_heightChanged)
            {
                UpdateYTransform();
                _heightChanged = false;
            }
            else
                Events.UpdateContent(this);
        }

        void UpdateYTransform()
        {
            if (_widthChanged || !_heightChanged)
                return;

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



            var diffValue = (NewHeight - ActualHeight);
            double valueChange = diffValue >=0 
                                     ? GetNegativeChange(ParentHeight, EndPoint.Y, diffValue, CurrentPoint.Y) 
                                     : GetPositiveChange(_currentYoffSet, diffValue, OriginalPoint.Y);

            //RenderTransform = new TranslateTransform();
            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.YProperty, -valueChange, 0, durationSpan: TimeSpan.FromMilliseconds(AnimationTimeSpan));
            positionAnimation.Completed += PositionYAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.YProperty, positionAnimation);

            if(diffValue >=0 )
                _currentYoffSet = _currentYoffSet + valueChange;
            else
                _currentYoffSet = _currentYoffSet - valueChange;
            
        }

        void ResizeHeight()
        {
            var heightChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, HeightProperty, NewHeight, ActualHeight, durationSpan: TimeSpan.FromMilliseconds(AnimationTimeSpan));
            heightChangeAnimation.Completed += HeightChangeAnimationCompleted;
            BeginAnimation(HeightProperty, heightChangeAnimation);
        }

        void PositionYAnimationCompleted(object sender, EventArgs e)
        {
            RenderTransform = new TranslateTransform(- _currentXoffSet, -_currentYoffSet);
            BeginAnimation(TranslateTransform.XProperty, null);
            BeginAnimation(TranslateTransform.YProperty, null);
            _heightChanged = false;

            ResizeHeight();
        }

        void HeightChangeAnimationCompleted(object sender, EventArgs e)
        {
            Height = NewHeight;
            BeginAnimation(HeightProperty, null);
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

        private double GetPositiveChange(double currentValue, double differenceValue, double originalValue)
        {
            if (originalValue>0 && Math.Abs(currentValue + differenceValue) < originalValue)
                return -differenceValue;
            return 0.0;
        }

        private void UpdateOrigianlSettings()
        {
            var positionInParent = GetPositionInParent();
            OriginalPoint = new Point(positionInParent.X, positionInParent.Y);
            CurrentMinWidth = ActualWidth;
            CurrentMinHeight = ActualHeight;
            var scrollViewer = GetTemplateChild(PART_MainScrolViewer) as ScrollViewer;

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
