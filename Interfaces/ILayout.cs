using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TextDashboard.UserControls;

namespace TextDashboard.Interfaces
{
    public interface ILayout
    {
        void Apply(Rect layoutRect, ViewItem item);
        Dictionary<ViewItem, Point> LayoutPositions { get; }
    }
}
