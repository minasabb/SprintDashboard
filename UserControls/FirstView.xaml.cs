using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AnimatinLibrary;
using TextDashboard.Resource;


namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for FirstView.xaml
    /// </summary>
    public partial class FirstView : UserControl
    {
        public Point EndPoint
        {
            get
            {
                var startPoint = GetPositionInParent();
                return new Point(startPoint.X + this.ActualWidth, startPoint.Y + ActualHeight);
            }
        }
        public double NewWidth { get; set; }
        public double NewHeight { get; set; }
        public double ParentWidth { get; set; }
        public double ParentHeight { get; set; }

        public FirstView()
        {
            InitializeComponent();
        }

        private Point GetPositionInParent()
        {
            return new Point();
        }

        private void GetData()
        {
            //figure out new width and height
        }

        private void ChangeSize()
        {
            var negativeWidthChange = GetNegativeChange(ParentWidth, EndPoint.X, (NewWidth - ActualWidth));
            var negativeHeightChange = GetNegativeChange(ParentHeight, EndPoint.Y, (NewHeight - ActualHeight));

            ResizeWidth(negativeWidthChange, NewWidth);
            ResizeHeight(negativeHeightChange, NewHeight);
        }

        private void ResizeWidth(double negativeChange, double newWidth)
        {
            
        }

        private void ResizeHeight(double negativeChange, double newHeight)
        {
            var heightChangeAnimation = AnimationFactory.CreateDoubleAnimation(this, HeightProperty, newHeight, ActualHeight, durationSpan:TimeSpan.FromMilliseconds(100));
            var translateTransform = TransformGroupHelper.GetTransformGroup(this, Enums.TransformType.Translate);
            var positionAnimation = AnimationFactory.CreateDoubleAnimation(this, TranslateTransform.YProperty, -negativeChange, 0, durationSpan: TimeSpan.FromMilliseconds(200));
        }
        

        private double GetNegativeChange(double parentValue, double endValue, double differenceValue)
        {
            if (parentValue < (endValue + differenceValue))
                differenceValue = parentValue - (endValue + differenceValue);

            if (parentValue - endValue < differenceValue)
                return differenceValue - (parentValue - endValue);

            return 0.0;
        }
    }
}
