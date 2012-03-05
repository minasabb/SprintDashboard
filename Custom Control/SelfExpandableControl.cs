using System;
using System.Diagnostics;
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
    [TemplatePart(Name = PartContentPresenter, Type = typeof(ContentPresenter))]
    public class SelfExpandableControl : SelfExpandableBase
    {

        private const string PartMainScrolViewer = "PART_MainScrolViewer";
        private const string PartTile = "PART_Tile";
        private const string PartContentPresenter = "PART_ContentPresenter";
        double _currentXoffSet;
        double _currentYoffSet;
        double _currentScrollbarExtentWidth;
        double _currentScrollbarExtentHeight;
        public const int AnimationWidthGrowTimeMs =700 ;
        public const int AnimationHeightGrowTimeMs = 400;
        public const int AnimationWidthShrinkTimeMs = 300;
        public const int AnimationHeightShrinkTimeMs = 300;
        public const int AnimationTranformNegativeValueTimeMs = 600;
        public const int AnimationTranformPositiveValueTimeMs = 300;
        public const int AnimationBeginTimeWidthGrowMs = 450;
        public const int AnimationScaleTransformMs = 300;
        public const int  AnimationFadeInOutTimeMs =1000;

        public const double InactiveScaleSize = 0.95;
        private const int Delta = 5;

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

            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut };
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

        void EventsUpdateOriginalSizeEvent(double tileBaseSize,Thickness tileMargin)
        {
            UpdateOrigianlSettings(tileBaseSize,tileMargin);

            var tileButton = GetTemplateChild(PartTile) as Button;
            if (tileButton != null) tileButton.Click += OnTileButtonClicked;
        }

        void OnTileButtonClicked(object sender, RoutedEventArgs e)
        {
            Events.UpdateControlState(this, State.Activated);
            e.Handled = true;
        }

        void MainScrollerViewerChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollbar = sender as ScrollViewer;
            if (scrollbar == null) return;

            GetCurrentValues();

            if (Math.Abs(scrollbar.ExtentWidth - _currentScrollbarExtentWidth) > Delta)
            {
                var newWidth = scrollbar.ExtentWidth;
                if (CurrentMaxWidth > CurrentMinWidth)
                {
                    if (newWidth > CurrentMaxWidth)
                        newWidth = CurrentMaxWidth;

                    if (newWidth < CurrentMinWidth && State != State.Deactivated)
                        newWidth = CurrentMinWidth;
                }

                NewWidth = newWidth;
            }

            if (Math.Abs(scrollbar.ExtentHeight - _currentScrollbarExtentHeight) > Delta)
            {
                var newHeight = scrollbar.ExtentHeight;
                if (CurrentMaxHeight > CurrentMinHeight)
                {
                    if (newHeight > CurrentMaxHeight)
                        newHeight = CurrentMaxHeight;

                    if (newHeight < CurrentMinHeight && State != State.Deactivated)
                        newHeight = CurrentMinHeight;
                }

                NewHeight = newHeight;
            }

            _currentScrollbarExtentWidth = scrollbar.ExtentWidth;
            _currentScrollbarExtentHeight = scrollbar.ExtentHeight;

        }

        private void UpdateTileSize(double baseTileSize,Thickness tileMargin)
        {
            switch (Size)
            {
                case TileSize.Double:
                    MinWidth = baseTileSize * 2 - tileMargin.Left - tileMargin.Right;
                    MinHeight = baseTileSize - tileMargin.Top - tileMargin.Bottom;
                    break;
                case TileSize.Quad:
                    MinWidth = baseTileSize * 2 - tileMargin.Left - tileMargin.Right;
                    MinHeight = baseTileSize * 2 - tileMargin.Top - tileMargin.Bottom;
                    break;
                default:
                    MinWidth = baseTileSize - tileMargin.Left - tileMargin.Right;
                    MinHeight = baseTileSize - tileMargin.Top - tileMargin.Bottom;
                    break;

            }
        }

        static void OnNewWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (SelfExpandableControl)sender;
            control.UpdateControlHorizontally();

        }

        static void OnNewHeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (SelfExpandableControl)sender;
            control.UpdateControlVertically();
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
                    IsEnabled = true;
                    break;
                case State.Deactivated:
                    IsEnabled = false;
                    TileTransformScaleAnimation(1, InactiveScaleSize);
                    break;
                default:
                    IsEnabled = true;
                    var content = GetTemplateChild(PartContentPresenter) as ContentPresenter;
                    if (content != null) content.Opacity = 1;
                    TileTransformScaleAnimation(InactiveScaleSize, 1);
                    break;
            }

        }

        private void UpdateControlHorizontally()
        {
            
            var beginTimeDelay = AnimationBeginTimeWidthGrowMs;
            var animationTimeSpan = AnimationWidthGrowTimeMs;
            double animationTranformTimeSpan=AnimationTranformNegativeValueTimeMs;
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
                animationTimeSpan = AnimationWidthShrinkTimeMs;
                animationTranformTimeSpan = AnimationTranformPositiveValueTimeMs;
                valueChange = GetPositiveChange(ParentWidth, _currentXoffSet, NewWidth, OriginalPoint.X);
                toKeySpline = new KeySpline(0.38, 0.38, 0.15, 0.98);
                beginTimeDelay = 0;
            }

            if (_currentXoffSet + valueChange > CurrentMaxWidth)
                valueChange = 0; 
            //Update current X value
            _currentXoffSet = _currentXoffSet + valueChange;

            ResizeWidthAnimation(animationTimeSpan, toKeySpline, null, beginTimeDelay);

            UpdateTransformXAnimation(animationTranformTimeSpan,beginTimeDelay);

            var content = GetTemplateChild(PartContentPresenter) as ContentPresenter;
            if (content != null && content.Opacity == 0)
                FadeInContentAnimation(beginTimeDelay);

        }

        void ResizeWidthAnimation(double animationTimeSpan, KeySpline toKeySpline, KeySpline fromKeySpline, int beginTimeDelay)
        {
            var widthChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, WidthProperty, toKeySpline, NewWidth, ActualWidth, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan), beginTimeSpan: TimeSpan.FromMilliseconds(beginTimeDelay));
            widthChangeAnimation.Completed += WidthChangeAnimationCompleted;
            BeginAnimation(WidthProperty, widthChangeAnimation);
        }

        void UpdateTransformXAnimation(double animationTranformTimeSpan, int beginTimeDelay)
        {
            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.XProperty, -_currentXoffSet, durationSpan: TimeSpan.FromMilliseconds(animationTranformTimeSpan), easingFuction: EasingFunction,beginTimeSpan: TimeSpan.FromMilliseconds(beginTimeDelay));
            positionAnimation.Completed += PositionXAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, positionAnimation);
        }

        void PositionXAnimationCompleted(object sender, EventArgs e)
        {
            //if (!_heightChanged)
            //{
            //    RenderTransform = new TranslateTransform(-_currentXoffSet, -_currentYoffSet);
            //    BeginAnimation(TranslateTransform.XProperty, null);
            //}
            //_widthChanged = false;
            RenderTransform = new TranslateTransform(-_currentXoffSet, -_currentYoffSet);
            BeginAnimation(TranslateTransform.XProperty, null);
            BeginAnimation(TranslateTransform.YProperty, null);
        }

        void WidthChangeAnimationCompleted(object sender, EventArgs e)
        {
            Width = NewWidth > CurrentMaxWidth ? CurrentMaxWidth : NewWidth;
            BeginAnimation(WidthProperty, null);
        }

        private void UpdateControlVertically()
        {
            var diffValue = NewHeight - ActualHeight ;
            var animationTimeSpan = AnimationHeightGrowTimeMs;
            double animationTranformTimeSpan = AnimationTranformNegativeValueTimeMs;
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
                animationTimeSpan = AnimationHeightShrinkTimeMs;
                animationTranformTimeSpan = AnimationTranformPositiveValueTimeMs;
                valueChange=GetPositiveChange(ParentHeight, _currentYoffSet, NewHeight, OriginalPoint.Y);
                fromKeySpline = new KeySpline(0.38, 0.38, 0.15, 0.98);
                toKeySpline = new KeySpline(0.23, 0.12, 0, 1);
            }

            if (_currentYoffSet + valueChange > CurrentMaxHeight)
                valueChange = 0;

            _currentYoffSet = _currentYoffSet + valueChange;

            ResizeHeightAnimation(animationTimeSpan, toKeySpline, fromKeySpline);

            UpdateTransformYAnimation(animationTranformTimeSpan);

        }

        void ResizeHeightAnimation(double animationTimeSpan, KeySpline toKeySpline, KeySpline fromKeySpline)
        {
            var heightChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, HeightProperty, toKeySpline, NewHeight, ActualHeight, fromKeySpline, durationSpan: TimeSpan.FromMilliseconds(animationTimeSpan));
            heightChangeAnimation.Completed += HeightChangeAnimationCompleted;
            BeginAnimation(HeightProperty, heightChangeAnimation);
        }

        void UpdateTransformYAnimation(double animationTranformTimeSpan)
        {
            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.YProperty, -_currentYoffSet, durationSpan: TimeSpan.FromMilliseconds(animationTranformTimeSpan), easingFuction: EasingFunction);
            positionAnimation.Completed += PositionYAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.YProperty, positionAnimation);
        }

        void PositionYAnimationCompleted(object sender, EventArgs e)
        {
            //RenderTransform = new TranslateTransform(-_currentXoffSet, -_currentYoffSet);
            //BeginAnimation(TranslateTransform.XProperty, null);
            //BeginAnimation(TranslateTransform.YProperty, null);
        }

        void HeightChangeAnimationCompleted(object sender, EventArgs e)
        {
            Height = NewHeight;
            BeginAnimation(HeightProperty, null);
        }

        private void FadeInContentAnimation(int beginTimeDelay = 0)
        {
            
            var content = GetTemplateChild(PartContentPresenter) as ContentPresenter;
            if (content == null) return;
            var animation = AnimationFactory.CreateDoubleAnimation(content, OpacityProperty, 1, 0, durationSpan: TimeSpan.FromMilliseconds(AnimationFadeInOutTimeMs), easingFuction: EasingFunction, beginTimeSpan: TimeSpan.FromMilliseconds(beginTimeDelay));
            content.BeginAnimation(OpacityProperty, animation);
            
        }

        private void TileTransformScaleAnimation(double fromValue,double toValue)
        {
            var storyboard = new Storyboard();
            var scale = new ScaleTransform(1.0, 1.0);
            var originX= ((ParentWidth / 2) - EndPoint.X)/ ActualWidth +1;
            var originY =((ParentHeight / 2) - EndPoint.Y)/ ActualHeight +1;

            Debug.WriteLine(originX + " and " + originY + " and Name" +Name);
            RenderTransformOrigin = new Point(originX,originY);
            RenderTransform = scale;

            var scaleXAnimation = AnimationFactory.CreateDoubleAnimation(this, ScaleTransform.ScaleXProperty, toValue, fromValue, durationSpan: TimeSpan.FromMilliseconds(AnimationScaleTransformMs), easingFuction: EasingFunction);
            storyboard.Children.Add(scaleXAnimation);
            scaleXAnimation.Completed+=ScaleXAnimationCompleted;

            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(scaleXAnimation, this);

            var scaleYAnimation = AnimationFactory.CreateDoubleAnimation(this, ScaleTransform.ScaleYProperty, toValue, fromValue, durationSpan: TimeSpan.FromMilliseconds(AnimationScaleTransformMs), easingFuction: EasingFunction);
            storyboard.Children.Add(scaleYAnimation);
            scaleYAnimation.Completed += ScaleYAnimationCompleted;

            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(scaleYAnimation, this);

            storyboard.Begin();
        }

        void ScaleYAnimationCompleted(object sender, EventArgs e)
        {
            var scale = RenderTransform as ScaleTransform;
            if (scale == null) return;
            scale.ScaleY = State == State.Deactivated ? InactiveScaleSize : 1;
        }

        void ScaleXAnimationCompleted(object sender, EventArgs e)
        {
            var scale = RenderTransform as ScaleTransform;
            if (scale == null) return;
            scale.ScaleX = State==State.Deactivated ? InactiveScaleSize : 1;
        }

        private void GetCurrentValues()
        {
            var positionInParent = GetPositionInParent();
            if (_currentXoffSet == 0 && _currentYoffSet == 0)
                RenderTransform = new TranslateTransform(-_currentXoffSet, -_currentYoffSet);
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
                var maxNegativeChange = currentValue;
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

        private void UpdateOrigianlSettings(double baseTileSize,Thickness tileMargin)
        {
            var positionInParent = GetPositionInParent();
            OriginalPoint = positionInParent;

            //Update Tile size
            UpdateTileSize(baseTileSize,tileMargin);

            CurrentMinWidth = MinWidth;
            CurrentMinHeight = MinHeight;

            var scrollViewer = GetTemplateChild(PartMainScrolViewer) as ScrollViewer;

            if (scrollViewer != null)
            {
                scrollViewer.MinWidth = MinWidth;
                scrollViewer.MinHeight = MinHeight;

                _currentScrollbarExtentWidth = scrollViewer.ExtentWidth;
                _currentScrollbarExtentHeight = scrollViewer.ExtentHeight;
                scrollViewer.ScrollChanged += MainScrollerViewerChanged;
            }
        }

        

    }
}
