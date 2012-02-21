using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using IQ.Core.Windows.Animation;
using TextDashboard.Resource;

namespace TextDashboard.Custom_Control
{
    
    [TemplatePart(Name = PartMainScrolViewer, Type = typeof(ScrollViewer))]
    public class SelfExpandableControl: ContentControl
    {

        private const string PartMainScrolViewer = "PART_MainScrolViewer";
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

        static SelfExpandableControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelfExpandableControl), new FrameworkPropertyMetadata(typeof(SelfExpandableControl)));
            
        }

        public SelfExpandableControl()
        {
            SetResourceReference(StyleProperty, "ResizableContentControlStyle");
            _currentXoffSet = 0;
            _currentYoffSet = 0;
            RenderTransform = new TranslateTransform(_currentXoffSet, _currentYoffSet);

            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };

            Loaded+=ResizableContentControlLoaded;
           Events.UpdateOriginalSizeEvent+=EventsUpdateOriginalSizeEvent; 
        }

        public static readonly DependencyProperty OriginalPointProperty =
            DependencyProperty.Register(
                "OriginalPoint",
                typeof(Point),
                typeof(SelfExpandableControl),
                new PropertyMetadata(new Point(0, 0)));

        public static readonly DependencyProperty CurrentPointProperty =
            DependencyProperty.Register(
                "CurrentPoint",
                typeof(Point),
                typeof(SelfExpandableControl),
                new PropertyMetadata(new Point(0, 0)));

        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register(
                "EndPoint",
                typeof(Point),
                typeof(SelfExpandableControl),
                new PropertyMetadata(new Point(0,0)));

        public static readonly DependencyProperty NewWidthProperty =
            DependencyProperty.Register(
                "NewWidth",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(OnNewWidthChanged));


        public static readonly DependencyProperty CurrentMaxWidthProperty =
            DependencyProperty.Register(
                "CurrentMaxWidth",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty CurrentMinWidthProperty =
            DependencyProperty.Register(
                "CurrentMinWidth",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty NewHeightProperty =
            DependencyProperty.Register(
                "NewHeight",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(OnNewHeightChanged));

        public static readonly DependencyProperty CurrentMaxHeightProperty =
            DependencyProperty.Register(
                "CurrentMaxHeight",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(0.0));


        public static readonly DependencyProperty CurrentMinHeightProperty =
            DependencyProperty.Register(
                "CurrentMinHeight",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(0.0));


        public static readonly DependencyProperty ParentWidthProperty =
            DependencyProperty.Register(
                "ParentWidth",
                typeof(double),
                typeof(SelfExpandableControl),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty ParentHeightProperty =
            DependencyProperty.Register(
                "ParentHeight",
                typeof(double),
                typeof(SelfExpandableControl),
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

            GetCurrentValues();

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
            var control = (SelfExpandableControl)sender;
            control.UpdateXTransform();

        }

        static void OnNewHeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (SelfExpandableControl)sender;
            control.UpdateYTransform();
        }

        private void UpdateXTransform()
        {
            if (NewWidth > CurrentMaxWidth)
            {
                NewWidth = CurrentMaxWidth;
                return;
            }

            if (NewWidth < CurrentMinWidth && State != State.Inactive)
            {
                NewWidth = CurrentMinWidth;
                return;
            }

            var animationTimeSpan = AnimationWidthGrowTimeSpan;
            double animationTranformTimeSpan=AnimationTranformNegativeValueTimeSpan;
            KeySpline toKeySpline;
            var diffValue = (NewWidth - ActualWidth);
            double valueChange;
                
            if( diffValue >= 0)
            {
                valueChange= GetNegativeChange(ParentWidth, EndPoint.X, diffValue, CurrentPoint.X);
                Console.WriteLine(valueChange);
                toKeySpline = new KeySpline(0.32, 0.13, 0.18, 1);
            }
            else
            {
                animationTimeSpan = AnimationWidthShrinkTimeSpan;
                animationTranformTimeSpan = AnimationTranformPositiveValueTimeSpan;
                valueChange = GetPositiveChange(ParentWidth, _currentXoffSet, NewWidth, OriginalPoint.X);
                toKeySpline = new KeySpline(0.38, 0.38, 0.15, 0.98);
            }
                               
            _currentXoffSet = _currentXoffSet + valueChange;

            ResizeWidth(animationTimeSpan,toKeySpline,null);

            //var widthChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, WidthProperty, toKeySpline, NewWidth, ActualWidth, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan), beginTimeSpan: TimeSpan.FromMilliseconds(50));
            //widthChangeAnimation.Completed += WidthChangeAnimationCompleted;

            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.XProperty, -_currentXoffSet, durationSpan: TimeSpan.FromMilliseconds(animationTranformTimeSpan), easingFuction: EasingFunction,beginTimeSpan: TimeSpan.FromMilliseconds(50));
            positionAnimation.Completed += PositionXAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, positionAnimation);

            //var storyboard = new Storyboard();
            //storyboard.Children.Add(widthChangeAnimation);
            //storyboard.Children.Add(positionAnimation);
        }

        void ResizeWidth(double animationTimeSpan,KeySpline toKeySpline,KeySpline fromKeySpline)
        {
            var widthChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, WidthProperty, toKeySpline, NewWidth, ActualWidth, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan), beginTimeSpan: TimeSpan.FromMilliseconds(50));
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

        private void UpdateYTransform()
        {
            if (NewHeight > CurrentMaxHeight)
            {
                NewHeight = CurrentMaxHeight;
                return;
            }

            if (NewHeight < CurrentMinHeight && State != State.Inactive)
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

            ResizeHeight(animationTimeSpan, toKeySpline, fromKeySpline);

            //var heightChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, HeightProperty, toKeySpline, NewHeight, ActualHeight, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan), beginTimeSpan: TimeSpan.FromMilliseconds(150));
            //heightChangeAnimation.Completed += HeightChangeAnimationCompleted;

            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.YProperty, -_currentYoffSet, durationSpan: TimeSpan.FromMilliseconds(animationTranformTimeSpan), easingFuction: EasingFunction, beginTimeSpan: TimeSpan.FromMilliseconds(150));
            positionAnimation.Completed += PositionYAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.YProperty, positionAnimation);

            //var storyboard = new Storyboard();
            //storyboard.Children.Add(heightChangeAnimation);
            //storyboard.Children.Add(positionAnimation); 
        }

        void ResizeHeight(double animationTimeSpan,KeySpline toKeySpline,KeySpline fromKeySpline)
        {
            var heightChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, HeightProperty, toKeySpline, NewHeight, ActualHeight, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan), beginTimeSpan: TimeSpan.FromMilliseconds(150));
            heightChangeAnimation.Completed += HeightChangeAnimationCompleted;
            BeginAnimation(HeightProperty, heightChangeAnimation);
        }

        void PositionYAnimationCompleted(object sender, EventArgs e)
        {
            RenderTransform = new TranslateTransform(-_currentXoffSet, -_currentYoffSet);
            BeginAnimation(TranslateTransform.XProperty, null);
            BeginAnimation(TranslateTransform.YProperty, null);
        }

        void HeightChangeAnimationCompleted(object sender, EventArgs e)
        {
            Height = NewHeight;
            BeginAnimation(HeightProperty, null);

            var scrollbar = GetTemplateChild(PartMainScrolViewer) as ScrollViewer;
            if (scrollbar != null) scrollbar.VerticalScrollBarVisibility = scrollbar.ExtentHeight - ActualHeight > 5 ? ScrollBarVisibility.Auto : ScrollBarVisibility.Hidden;
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
            CurrentMinWidth = ActualWidth;
            CurrentMinHeight = ActualHeight;

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



        private void GoToState(bool useTransitions)
        {
            if (State == State.Normal)
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            else if (State == State.Active)
            {
                VisualStateManager.GoToState(this, "Active", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Inactive", useTransitions);
            }
        }


        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(State)
            , typeof(SelfExpandableControl), new PropertyMetadata(OnStatePropertyChanged));

        static void OnStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SelfExpandableControl;
            var newValue = (State)e.NewValue;
            if (control != null) control.OnStateOfControlChange(newValue);
        }

        public State State
        {
            get
            {
                return (State)GetValue(StateProperty);
            }

            set
            {
                SetValue(StateProperty, value);
            }
        }

        protected virtual void OnStateOfControlChange(State state)
        {
           if(state==State.Active) 
               return;

            switch (state)
            {
                case State.Active:
                    IsEnabled = true;
                    break;
                case State.Inactive:
                    IsEnabled = false;
                    NewWidth = 0.95 * ActualWidth;
                    NewHeight = 0.95 * ActualHeight;
                    break;
                default:
                    IsEnabled = true;
                    NewWidth = CurrentMinWidth;
                    NewHeight = CurrentMinHeight;
                    break;
            }

        }




    }
}
