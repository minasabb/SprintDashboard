using System;
using System.Windows;
using System.Windows.Controls;

namespace TextDashboard.Resource
{
    public delegate void IncreaseSizeDelegate(object control,double percent);
    public delegate void DecreaseSizeDelegate(object control);

    public interface IEvent
    {
        
    }
}
