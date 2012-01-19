using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace TextDashboard.UserControls
{
    public class ViewItem : ContentControl
    {
        private bool _isDragging;
        private Point _startPosition;

        private Storyboard _locationAnimator;
        private PointAnimation _pointAnimation;
        private bool _isAnimating;

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(ViewItem), new PropertyMetadata(OnItemsSourceChanged));

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof(bool), typeof(ViewItem), new PropertyMetadata(OnIsSelectedChanged));

        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(
            "Location", typeof(Point), typeof(ViewItem), new PropertyMetadata(OnLocationChanged));

        private Dictionary<object, ViewItem> ItemMap { get; set; }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public Point Location
        {
            get { return (Point)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        internal PanelControl Owner { get; set; }

        public bool IsMouseOver { get; set; }


        public ViewItem()
        {
            DefaultStyleKey = typeof(ViewItem);

            InitializeStoryboard();
        }

        private void InitializeStoryboard()
        {
            _pointAnimation = new PointAnimation()
            {
                To = new Point(),
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                EasingFunction = new CubicEase()
            };
            Storyboard.SetTarget(_pointAnimation, this);
            Storyboard.SetTargetProperty(_pointAnimation, new PropertyPath("Location"));

            _locationAnimator = new Storyboard();
            _locationAnimator.Children.Add(_pointAnimation);
            _locationAnimator.Completed += delegate
            {
                _isAnimating = false;
                var pt = _pointAnimation.To.Value;
                Location = pt;
            };
        }

        private static void OnLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as ViewItem;
            item.MoveItemTo((Point)e.NewValue);
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ViewItem;
            var newSource = e.NewValue as IEnumerable;
            var oldSource = e.OldValue as IEnumerable;
            control.PopulateItems(oldSource, newSource);
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ViewItem;
            control.UpdateVisualStates();
        }

        private void PopulateItems(IEnumerable oldSource, IEnumerable newSource)
        {
            if (oldSource != null)
            {
                ItemMap.Clear();
                ItemMap = null;

                if (oldSource is INotifyCollectionChanged)
                {
                    var incc = oldSource as INotifyCollectionChanged;
                    incc.CollectionChanged -= OnItemsCollectionChanged;
                }
            }

            if (newSource != null)
            {
                if (newSource is INotifyCollectionChanged)
                {
                    var incc = newSource as INotifyCollectionChanged;
                    incc.CollectionChanged += OnItemsCollectionChanged;
                }
                ItemMap = new Dictionary<object, ViewItem>();
                foreach (var modelItem in newSource)
                {
                    PrepareContainer(modelItem);
                }
            }
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in e.NewItems)
                    {
                        PrepareContainer(newItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var oldItem in e.OldItems)
                    {
                        var container = ItemMap[oldItem];
                        ClearContainer(container);
                    }
                    break;
            }
        }

        private void ClearContainer(ViewItem container)
        {
            ItemMap.Remove(container.Content);

            container.Content = null;
        }

        private void PrepareContainer(object modelItem)
        {
            var container = new ViewItem
            {
                Content = modelItem,
                Owner = Owner
            };

            // Template
            if (ContentTemplate is HierarchicalDataTemplate)
            {
                var hTemplate = ContentTemplate as HierarchicalDataTemplate;
                container.ContentTemplate = hTemplate.ItemTemplate;

                var binding = hTemplate.ItemsSource;
                BindingOperations.SetBinding(container, ItemsSourceProperty, binding);

                container.DataContext = modelItem;
            }

            // Positioning
            //Dispatcher.BeginInvoke(() =>
            //{
            //    var location = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
            //    location.X += ActualWidth;
            //    location.Y += ActualHeight;
            //    container.MoveItemBy(location, true);
            //});
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            IsMouseOver = true;
            UpdateVisualStates();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsMouseOver = false;
            UpdateVisualStates();
        }


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _isDragging = true;

            CaptureMouse();

            UpdateVisualStates();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _isDragging = false;
            ReleaseMouseCapture();
        }

        private void UpdateVisualStates()
        {
            if (IsMouseOver)
            {
                VisualStateManager.GoToState(this, "Hover", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "NoHover", true);
            }

            if (IsSelected)
            {
                VisualStateManager.GoToState(this, "Selected", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Unselected", true);
            }
        }

        private void MoveItemBy(Point delta, bool moveGroup)
        {
            var x = Canvas.GetLeft(this) + delta.X;
            var y = Canvas.GetTop(this) + delta.Y;

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            if (!_isAnimating)
            {
                Location = new Point(x, y);
            }

            if (moveGroup && ItemMap != null)
            {
                foreach (var childItem in ItemMap.Values)
                {
                    childItem.MoveItemBy(delta, moveGroup);
                }
            }
        }

        private void MoveItemTo(Point point)
        {
            var currX = Canvas.GetLeft(this);
            var currY = Canvas.GetTop(this);
            var delta = new Point(point.X - currX, point.Y - currY);

            MoveItemBy(delta, false);
        }

        internal void AnimationPosition(Point point)
        {
            if (_isAnimating)
            {
                _locationAnimator.Pause();
            }

            _isAnimating = true;
            _pointAnimation.To = point;
            _locationAnimator.Begin();
        }

    }
}
