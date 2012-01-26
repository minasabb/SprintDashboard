using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IQ.Core.Windows.Animation;

namespace TextDashboard.Custom_Control
{
    public class ResizableContentControl: ContentControl
    {
        bool _widthChanged;
        bool _heightChanged;
        double _maximumControlWidth;
        double _maximumControlHeight;
        double _currentXoffSet;
        double _currentYoffSet;

        const int AnimationTimeSpan = 300;

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
        }

        public static readonly DependencyProperty OriginalPointProperty =
            DependencyProperty.Register(
                "OriginalPoint",
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

        static void OnNewWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (ResizableContentControl)sender;
            control.UpdateXTransform();
        }

        public static readonly DependencyProperty NewHeightProperty =
            DependencyProperty.Register(
                "NewHeight",
                typeof(double),
                typeof(ResizableContentControl),
                new UIPropertyMetadata(OnNewHeightChanged));

        static void OnNewHeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (ResizableContentControl)sender;
            control.UpdateYTransform();
        }

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
        public double NewHeight
        {
            get { return (double)GetValue(NewHeightProperty); }
            set { SetValue(NewHeightProperty, value); }
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

        private void GetData()
        {
            //figure out new width and height
        }

        void UpdateXTransform()
        {
            _widthChanged = true;
            GetCurrentValues();

            if (NewWidth > _maximumControlWidth)
            {
                NewWidth = _maximumControlWidth;
                return;
            }

            var negativeChange = GetNegativeChange(ParentWidth, EndPoint.X, (NewWidth - ActualWidth), OriginalPoint.X);

            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.XProperty, -negativeChange, 0, durationSpan: TimeSpan.FromMilliseconds(AnimationTimeSpan));
            positionAnimation.Completed += PositionXAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.XProperty, positionAnimation);

            _currentXoffSet = _currentXoffSet + negativeChange;
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
            Width = NewWidth > _maximumControlWidth ? _maximumControlWidth : NewWidth;
            BeginAnimation(WidthProperty, null);

            if (_heightChanged)
                UpdateYTransform();
        }

        private void GetCurrentValues()
        {
            var positionInParent = GetPositionInParent();

            OriginalPoint = new Point(positionInParent.X, positionInParent.Y);
            EndPoint = new Point(positionInParent.X + ActualWidth, positionInParent.Y + ActualHeight);
            ParentWidth = (double)Parent.GetValue(ActualWidthProperty);
            ParentHeight = (double)Parent.GetValue(ActualHeightProperty);
            _maximumControlWidth = ParentWidth - Margin.Left - Margin.Right;
            _maximumControlHeight = ParentHeight - Margin.Top - Margin.Bottom;

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

        void UpdateYTransform()
        {
            _heightChanged = true;
            if (_widthChanged)
                return;


            if (NewHeight > _maximumControlHeight)
            {    
                NewHeight = _maximumControlHeight;
                return;
            }

            var negativeChange = GetNegativeChange(ParentHeight, EndPoint.Y, (NewHeight - ActualHeight), OriginalPoint.Y);

            //RenderTransform = new TranslateTransform();
            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.YProperty, -negativeChange, 0, durationSpan: TimeSpan.FromMilliseconds(AnimationTimeSpan));
            positionAnimation.Completed += PositionYAnimationCompleted;
            RenderTransform.BeginAnimation(TranslateTransform.YProperty, positionAnimation);

            _currentYoffSet = _currentYoffSet +negativeChange;
            
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
        }

        private double GetNegativeChange(double parentValue,double endValue, double differenceValue,double originalValue)
        {
            if (parentValue < (endValue + differenceValue))
            {
                differenceValue = Math.Abs(parentValue - (endValue + differenceValue));
                var maxNegativeChange = parentValue - originalValue;
                if(maxNegativeChange<0)
                    maxNegativeChange = 0;
                return differenceValue < maxNegativeChange ? differenceValue : maxNegativeChange;
            }

            if (parentValue - endValue < differenceValue)
                return differenceValue - (parentValue - endValue);

            return 0.0;
        }
    }
}
