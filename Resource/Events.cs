﻿using System.Windows;
using TextDashboard.Custom_Control;

namespace TextDashboard.Resource
{
    public delegate void UpdateControlStateDelegate(object sender,State state);

    //public delegate void MoveControlToTopDelegate(object sender);
    //public delegate void ShowCurtainDelegate(object sender);
    //public delegate void HideCurtainDelegate(object sender);

    public delegate void UpdateOriginalSizeDelegate(double tileBaseSize,Thickness tileMargin);
    //public delegate void UpdateContentDelegate(object sender);

    public static class Events
    {
        public static event UpdateControlStateDelegate UpdateControlStateEvent;
        public static void UpdateControlState(object control, State state)
        {
            if (UpdateControlStateEvent != null)
                UpdateControlStateEvent(control,state);
        }

        //public static event MoveControlToTopDelegate MoveControlToTopEvent;
        //public static void MoveControlToTop(object control)
        //{
        //    if (MoveControlToTopEvent != null)
        //        MoveControlToTopEvent(control);
        //}

        //public static event ShowCurtainDelegate ShowCurtainEvent;
        //public static void ShowCurtain(object control)
        //{
        //    if (ShowCurtainEvent != null)
        //        ShowCurtainEvent(control);
        //}

        //public static event HideCurtainDelegate HideCurtainEvent;
        //public static void HideCurtain(object control)
        //{
        //    if (HideCurtainEvent != null)
        //        HideCurtainEvent(control);
        //}

        public static event UpdateOriginalSizeDelegate UpdateOriginalSizeEvent;
        public static void UpdateOriginalSize(double tileBaseSize,Thickness tileMargin)
        {
            if (UpdateOriginalSizeEvent != null)
                UpdateOriginalSizeEvent(tileBaseSize, tileMargin);
        }

        //public static event UpdateContentDelegate UpdateContentEvent;
        //public static void UpdateContent(object control)
        //{
        //    if (UpdateContentEvent != null)
        //        UpdateContentEvent(control);
        //}
    }


}
