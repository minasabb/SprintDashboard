using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using IQ.Core.Windows.Animation;
using System.Linq;
using TextDashboard.Resource;

namespace TextDashboard.Custom_Control
{
    
    [TemplatePart(Name = PartMainScrolViewer, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = PartTile, Type = typeof(Button))]
    public class SelfExpandableControl: SelfExpandableBase
    {

        private const string PartMainScrolViewer = "PART_MainScrolViewer";
        private const string PartTile= "PART_Tile";
        double _currentXoffSet;
        double _currentYoffSet;
        double _currentScrollbarExtentWidth;
        double _currentScrollbarExtentHeight;
        const int AnimationWidthGrowTimeSpan =1000 ;
        const int AnimationHeightGrowTimeSpan = 700;
        const int AnimationWidthShrinkTimeSpan = 400;
        const int AnimationHeightShrinkTimeSpan = 400;
        const int AnimationTranformNegativeValueTimeSpan = 800;
        const int AnimationTranformPositiveValueTimeSpan = 400;
        const int AnimationStandardTimeSpan = 300;
        const double InactiveScaleSize = 0.95;
        const int ExtraSpacing = 5;

        static SelfExpandableControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelfExpandableControl), new FrameworkPropertyMetadata(typeof(SelfExpandableControl)));
        }

        public SelfExpandableControl()
        {
            SetResourceReference(StyleProperty, "SelfExpandableControlStyle");
            _currentXoffSet = 0;
            _currentYoffSet = 0;
            TransformGroupHelper.CreateTransformGroup(this);

            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };
            Loaded+=ResizableContentControlLoaded;
           Events.UpdateOriginalSizeEvent+=EventsUpdateOriginalSizeEvent; 
        }

        public static readonly DependencyProperty NewWidthProperty =
            DependencyProperty.Register(
                "NewWidth",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(OnNewWidthChanged));

        public double NewWidth
        {
            get { return (double)GetValue(NewWidthProperty); }
            set { SetValue(NewWidthProperty, value); }
        }
        
        public static readonly DependencyProperty NewHeightProperty =
            DependencyProperty.Register(
                "NewHeight",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(OnNewHeightChanged));

        public double NewHeight
        {
            get { return (double)GetValue(NewHeightProperty); }
            set { SetValue(NewHeightProperty, value); }
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(State)
            , typeof(SelfExpandableControl), new PropertyMetadata(State.Normal,OnStatePropertyChanged));

        public State State
        {
            get { return (State)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public IEasingFunction EasingFunction { get; set; }

        void EventsUpdateOriginalSizeEvent(double tileBaseSize)
        {
            UpdateOrigianlSettings(tileBaseSize);

            var tileButton = GetTemplateChild(PartTile) as Button;
            if (tileButton != null) tileButton.Click += OnTileButtonClicked;
        }

        void OnTileButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
 
            OpacityAnimation(button,0);
        }

        void ScrollerViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollbar = sender as ScrollViewer;
            if (scrollbar == null) return;

            scrollbar.HorizontalScrollBarVisibility=ScrollBarVisibility.Hidden;
            scrollbar.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            GetCurrentValues();

            if (Math.Abs(scrollbar.ExtentWidth - _currentScrollbarExtentWidth) > 5)
                NewWidth = scrollbar.ExtentWidth;

            if (Math.Abs(scrollbar.ExtentHeight - _currentScrollbarExtentHeight) > 5)
                NewHeight = scrollbar.ExtentHeight;

            _currentScrollbarExtentWidth = scrollbar.ExtentWidth;
            _currentScrollbarExtentHeight = scrollbar.ExtentHeight;

        }

        void ResizableContentControlLoaded(object sender, RoutedEventArgs e)
        {
            //UpdateOrigianlSettings();
        }

        private void UpdateTileSize(double baseTileSize)
        {
            switch (Size)
            {
                case TileSize.Double:
                    MinWidth = baseTileSize * 2;
                    MinHeight = baseTileSize;
                    break;
                case TileSize.Quad:
                    MinWidth = baseTileSize * 2;
                    MinHeight = baseTileSize * 2;
                    break;
                default:
                    MinWidth = baseTileSize;
                    MinHeight = baseTileSize;
                    break;

            }
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

            if (NewWidth < CurrentMinWidth && State != State.Deactivated)
            {
                NewWidth = CurrentMinWidth;
                return;
            }

            var beginTimeDelay = AnimationStandardTimeSpan;
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
                beginTimeDelay = 0;
            }
                               
            _currentXoffSet = _currentXoffSet + valueChange;

            ResizeWidth(animationTimeSpan, toKeySpline, null, beginTimeDelay);

            //var widthChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, WidthProperty, toKeySpline, NewWidth, ActualWidth, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan));
            //widthChangeAnimation.Completed += WidthChangeAnimationCompleted;

            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.XProperty, -_currentXoffSet, durationSpan: TimeSpan.FromMilliseconds(animationTranformTimeSpan), easingFuction: EasingFunction);
            positionAnimation.Completed += PositionXAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, positionAnimation);

            //var storyboard = new Storyboard();
            //storyboard.Children.Add(widthChangeAnimation);
            //storyboard.Children.Add(positionAnimation);
            //storyboard.Begin();
        }

        void ResizeWidth(double animationTimeSpan, KeySpline toKeySpline, KeySpline fromKeySpline, int beginTimeDelay)
        {
            var widthChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, WidthProperty, toKeySpline, NewWidth, ActualWidth, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan), beginTimeSpan: TimeSpan.FromMilliseconds(beginTimeDelay));
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
            if (scrollbar != null) scrollbar.HorizontalScrollBarVisibility = scrollbar.ExtentWidth - ActualWidth > ExtraSpacing ? ScrollBarVisibility.Auto : ScrollBarVisibility.Hidden;
        }

        private void UpdateYTransform()
        {
            if (NewHeight > CurrentMaxHeight)
            {
                NewHeight = CurrentMaxHeight;
                return;
            }

            if (NewHeight < CurrentMinHeight && State != State.Deactivated)
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

            //var heightChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, HeightProperty, toKeySpline, NewHeight, ActualHeight, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan));
            //heightChangeAnimation.Completed += HeightChangeAnimationCompleted;

            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.YProperty, -_currentYoffSet, durationSpan: TimeSpan.FromMilliseconds(animationTranformTimeSpan), easingFuction: EasingFunction);
            positionAnimation.Completed += PositionYAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.YProperty, positionAnimation);

            //var storyboard = new Storyboard();
            //storyboard.Children.Add(heightChangeAnimation);
            //storyboard.Children.Add(positionAnimation); 
            //storyboard.Begin();
        }

        private void OpacityAnimation(UIElement uiElement, int toValue, int fromValue = 1)
        {
            var animation = AnimationFactory.CreateDoubleAnimation(uiElement, OpacityProperty, toValue, fromValue, durationSpan: TimeSpan.FromMilliseconds(AnimationStandardTimeSpan), easingFuction: EasingFunction);
            animation.Completed += OpacityAnimationAnimationCompleted;
            uiElement.BeginAnimation(OpacityProperty, animation);
        }

        void OpacityAnimationAnimationCompleted(object sender, EventArgs e)
        {
            Events.UpdateControlState(this, State.Activated);
            var tileButton = GetTemplateChild(PartTile) as Button;
            if (tileButton == null) return;
            tileButton.Opacity = 1;
            tileButton.BeginAnimation(OpacityProperty, null);
        }

        private void TransformScaleAnimation(double fromValue,double toValue)
        {
            var storyboard = new Storyboard();
            var scale = new ScaleTransform(1.0, 1.0);
            RenderTransformOrigin = new Point(0.5, 0.5);
            RenderTransform = scale;

            var scaleXAnimation = AnimationFactory.CreateDoubleAnimation(this, ScaleTransform.ScaleXProperty, toValue, fromValue, durationSpan: TimeSpan.FromMilliseconds(AnimationStandardTimeSpan), easingFuction: EasingFunction);
            storyboard.Children.Add(scaleXAnimation);

            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(scaleXAnimation, this);

            var scaleYAnimation = AnimationFactory.CreateDoubleAnimation(this, ScaleTransform.ScaleYProperty, toValue, fromValue, durationSpan: TimeSpan.FromMilliseconds(AnimationStandardTimeSpan), easingFuction: EasingFunction);
            storyboard.Children.Add(scaleYAnimation);

            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(scaleYAnimation, this);

            storyboard.Begin();
        }

        void ResizeHeight(double animationTimeSpan,KeySpline toKeySpline,KeySpline fromKeySpline)
        {
            var heightChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, HeightProperty, toKeySpline, NewHeight, ActualHeight, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan));
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
            if (scrollbar != null) scrollbar.VerticalScrollBarVisibility = scrollbar.ExtentHeight - ActualHeight > ExtraSpacing ? ScrollBarVisibility.Auto : ScrollBarVisibility.Hidden;
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
                if (maxNegativeChange < 0)
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

        private void UpdateOrigianlSettings(double baseTileSize)
        {
            var positionInParent = GetPositionInParent();
            OriginalPoint = positionInParent;
            
            //Update Tile size
            UpdateTileSize(baseTileSize);

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

        static void OnStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SelfExpandableControl;
            var newValue = (State)e.NewValue;
            if (control != null) control.OnStateOfControlChange(newValue);
        }

        protected virtual void OnStateOfControlChange(State state)
        {
            switch (state)
            {
                case State.Activated:
                    //Activated State
                    //Change from button/tile content to search/response content
                    IsEnabled = true;
                    break;
                case State.Deactivated:
                    IsEnabled = false;
                    //Run scale animation (down in scale)
                    TransformScaleAnimation(1,InactiveScaleSize);
                    break;
                default:
                    //Change to normal
                    //Reset Scale
                    //Reset Size
                    IsEnabled = true;
                    TransformScaleAnimation(InactiveScaleSize, 1);
                    break;
            }

        }

    }
}
