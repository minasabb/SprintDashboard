using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace TextDashboard.Resource
{
    public delegate void IncreaseSizeDelegate(object sender);

    public delegate void UpdateOriginalSizeDelegate();
    public delegate void UpdateContentDelegate(object sender);

    public static class Events
    {
        public static event IncreaseSizeDelegate IncreaseSizeEvent;
        public static void IncreaseSize(object control)
        {
            if (IncreaseSizeEvent != null)
                IncreaseSizeEvent(control);
        }

        public static event UpdateOriginalSizeDelegate UpdateOriginalSizeEvent;
        public static void UpdateOriginalSize()
        {
            if (UpdateOriginalSizeEvent != null)
                UpdateOriginalSizeEvent();
        }

        public static event UpdateContentDelegate UpdateContentEvent;
        public static void UpdateContent(object control)
        {
            if (UpdateContentEvent != null)
                UpdateContentEvent(control);
        }
    }


}
