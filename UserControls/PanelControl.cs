using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using TextDashboard.Interfaces;

namespace TextDashboard.UserControls
{
    [TemplatePart(Name = "PanningContainer", Type = typeof(Canvas))]
    [TemplatePart(Name = "ItemsContainer", Type = typeof(Canvas))]
    public class PanelControl : Control
    {
        public static readonly DependencyProperty ConnectorStyleProperty = DependencyProperty.Register(
                                            "ConnectorStyle", typeof(Style), typeof(PanelControl), null);

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(object), typeof(PanelControl), new PropertyMetadata(OnHeaderChanged));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(PanelControl), null);

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate), typeof(PanelControl), null);

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof(object), typeof(PanelControl), null);

        private ViewItem _selectedContainer;
        private bool _isPanning;
        private Point _startPosition;

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        protected ViewItem RootItem { get; set; }

        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        protected Canvas ItemsContainer { get; set; }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        protected Canvas PanningContainer { get; set; }

        public PanelControl()
        {
            DefaultStyleKey = typeof(PanelControl);
        }


        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PanelControl;
            var header = e.NewValue;

            control.SetRootItem(header);
        }

        private void SetRootItem(object newValue)
        {
            if (RootItem != null)
            {
                RootItem.Owner = null;
                RootItem.ClearValue(ViewItem.ItemsSourceProperty);
                ItemsContainer.Children.Remove(RootItem);
            }

            if (ItemsContainer != null)
            {
                RootItem = new ViewItem()
                {
                    Content = newValue,
                    ContentTemplate = ItemTemplate,
                    Owner = this
                };

                if (ItemTemplate is HierarchicalDataTemplate)
                {
                    var binding = (ItemTemplate as HierarchicalDataTemplate).ItemsSource;
                    BindingOperations.SetBinding(RootItem, ViewItem.ItemsSourceProperty, binding);
                }

                ItemsContainer.Children.Add(RootItem);
            }
        }

        private void OnPanStart(object sender, MouseButtonEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                _isPanning = true;
                _startPosition = e.GetPosition(this);
                PanningContainer.CaptureMouse();
            }
        }

        private void OnPanEnd(object sender, MouseButtonEventArgs e)
        {
            _isPanning = false;
            PanningContainer.ReleaseMouseCapture();
        }

        private void OnPanMove(object sender, MouseEventArgs e)
        {
            var isControlPressed = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
            if (_isPanning && isControlPressed)
            {
                var currentPosition = e.GetPosition(this);
                var delta = new Point(currentPosition.X - _startPosition.X, currentPosition.Y - _startPosition.Y);
                _startPosition = currentPosition;

                var itemXform = ItemsContainer.RenderTransform as TranslateTransform;

                itemXform.X += delta.X;
                itemXform.Y += delta.Y;
            }
        }

        public override void OnApplyTemplate()
        {
            PanningContainer = GetTemplateChild("PanningContainer") as Canvas;
            PanningContainer.MouseLeftButtonDown += OnPanStart;
            PanningContainer.MouseMove += OnPanMove;
            PanningContainer.MouseLeftButtonUp += OnPanEnd;

            ItemsContainer = GetTemplateChild("ItemsContainer") as Canvas;
            ItemsContainer.RenderTransform = new TranslateTransform();

            SetRootItem(Header);
        }

        internal void AddItemContainer(ViewItem container)
        {
            ItemsContainer.Children.Add(container);
        }

        public void RemoveItemContainer(ViewItem container)
        {
            ItemsContainer.Children.Remove(container);
        }

        public void SetSelection(ViewItem ViewItem)
        {
            if (_selectedContainer == ViewItem) return; // clicked on already selected instance

            if (_selectedContainer != null)
            {
                _selectedContainer.IsSelected = false;
            }

            _selectedContainer = ViewItem;
            _selectedContainer.IsSelected = true;
            SelectedItem = ViewItem.Content;
        }

        public void ApplyLayout(ILayout layout)
        {
            if (layout != null)
            {
                layout.Apply(new Rect(new Point(), new Size(ActualWidth, ActualHeight)), RootItem);
                foreach (var pair in layout.LayoutPositions)
                {
                //    var item = pair.Key;
                //    var position = pair.Value;

                //    item.AnimationPosition(position);
                }
            }
        }
    }
}
