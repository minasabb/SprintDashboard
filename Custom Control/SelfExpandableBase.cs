using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IQ.Core.Windows.Animation;
using TextDashboard.Resource;

namespace TextDashboard.Custom_Control
{
    public class SelfExpandableBase: ContentControl
    {
        public static readonly DependencyProperty OriginalPointProperty =
            DependencyProperty.Register(
                "OriginalPoint",
                typeof(Point),
                typeof(SelfExpandableControl),
                new PropertyMetadata(new Point(0, 0)));

        public Point OriginalPoint
        {
            get { return (Point)GetValue(OriginalPointProperty); }
            set { SetValue(OriginalPointProperty, value); }
        }

        public static readonly DependencyProperty CurrentPointProperty =
            DependencyProperty.Register(
                "CurrentPoint",
                typeof(Point),
                typeof(SelfExpandableControl),
                new PropertyMetadata(new Point(0, 0)));

        public Point CurrentPoint
        {
            get { return (Point)GetValue(CurrentPointProperty); }
            set { SetValue(CurrentPointProperty, value); }
        }

        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register(
                "EndPoint",
                typeof(Point),
                typeof(SelfExpandableControl),
                new PropertyMetadata(new Point(0, 0)));


        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        public static readonly DependencyProperty CurrentMaxWidthProperty =
            DependencyProperty.Register(
                "CurrentMaxWidth",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(0.0));

        public double CurrentMaxWidth
        {
            get { return (double)GetValue(CurrentMaxWidthProperty); }
            set { SetValue(CurrentMaxWidthProperty, value); }
        }


        public static readonly DependencyProperty CurrentMinWidthProperty =
            DependencyProperty.Register(
                "CurrentMinWidth",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(0.0));

        public double CurrentMinWidth
        {
            get { return (double)GetValue(CurrentMinWidthProperty); }
            set { SetValue(CurrentMinWidthProperty, value); }
        }

        public static readonly DependencyProperty CurrentMaxHeightProperty =
            DependencyProperty.Register(
                "CurrentMaxHeight",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(0.0));

        public double CurrentMaxHeight
        {
            get { return (double)GetValue(CurrentMaxHeightProperty); }
            set { SetValue(CurrentMaxHeightProperty, value); }
        }


        public static readonly DependencyProperty CurrentMinHeightProperty =
            DependencyProperty.Register(
                "CurrentMinHeight",
                typeof(double),
                typeof(SelfExpandableControl),
                new UIPropertyMetadata(0.0));

        public double CurrentMinHeight
        {
            get { return (double)GetValue(CurrentMinHeightProperty); }
            set { SetValue(CurrentMinHeightProperty, value); }
        }

        public static readonly DependencyProperty ParentWidthProperty =
            DependencyProperty.Register(
                "ParentWidth",
                typeof(double),
                typeof(SelfExpandableControl),
                new PropertyMetadata(0.0));

        public double ParentWidth
        {
            get { return (double)GetValue(ParentWidthProperty); }
            set { SetValue(ParentWidthProperty, value); }
        }

        public static readonly DependencyProperty ParentHeightProperty =
            DependencyProperty.Register(
                "ParentHeight",
                typeof(double),
                typeof(SelfExpandableControl),
                new PropertyMetadata(0.0));

        public double ParentHeight
        {
            get { return (double)GetValue(ParentHeightProperty); }
            set { SetValue(ParentHeightProperty, value); }
        }

        //Add Icon, Title, Tile Size, Tile Color, Content Color
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                "Icon",
                typeof(string),
                typeof(SelfExpandableControl),
                new PropertyMetadata(string.Empty));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(SelfExpandableControl),
                new PropertyMetadata(string.Empty));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TileIconSizeProperty =
            DependencyProperty.Register(
                "TileIconSize",
                typeof(double),
                typeof(SelfExpandableControl),
                new PropertyMetadata(50.0));

        public double TileIconSize
        {
            get { return (double)GetValue(TileIconSizeProperty); }
            set { SetValue(TileIconSizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(
                "Size",
                typeof(TileSize),
                typeof(SelfExpandableControl),
                new PropertyMetadata(TileSize.Single));

        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty TileBackgroundColorProperty =
            DependencyProperty.Register(
                "TileBackgroundColor",
                typeof(Brush),
                typeof(SelfExpandableControl),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush TileBackgroundColor
        {
            get { return (Brush)GetValue(TileBackgroundColorProperty); }
            set { SetValue(TileBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty ContentBackgroundColorProperty =
            DependencyProperty.Register(
                "ContentBackgroundColor",
                typeof(Brush),
                typeof(SelfExpandableControl),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush ContentBackgroundColor
        {
            get { return (Brush)GetValue(ContentBackgroundColorProperty); }
            set { SetValue(ContentBackgroundColorProperty, value); }
        }

    }
}
