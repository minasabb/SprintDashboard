using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace TextDashboard.Resource
{
    public delegate void IncreaseSizeDelegate(object sender, double percent);

    public delegate void DecreaseSizeDelegate(object sender);

    public static class Events
    {
        public static event IncreaseSizeDelegate IncreaseSizeEvent;
        public static void IncreaseSize(object control, double percent)
        {
            if (IncreaseSizeEvent != null)
                IncreaseSizeEvent(control, percent);
        }

        public static event DecreaseSizeDelegate DecreaseSizeEvent;
        public static void DecreaseSize(object control)
        {
            if (DecreaseSizeEvent != null)
                DecreaseSizeEvent(control);
        }

        private static Action EmptyDelegate = delegate() { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }


}
