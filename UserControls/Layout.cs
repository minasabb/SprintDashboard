using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using TextDashboard.Interfaces;

namespace TextDashboard.UserControls
{
    public class Layout : ILayout
    {
        public Dictionary<ViewItem, Point> LayoutPositions { get; private set; }
        public double Radius { get; set; }

        public void Apply(Rect layoutRect, ViewItem rootItem)
        {
            var center = new Point(layoutRect.Width / 2, layoutRect.Height / 2);

            LayoutPositions = new Dictionary<ViewItem, Point>();

            PositionItem(rootItem, center);
        }

        private void PositionItem(ViewItem parent, Point position)
        {
            LayoutPositions[parent] = position;
        }

    }
}
