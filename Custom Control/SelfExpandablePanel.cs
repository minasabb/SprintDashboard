﻿using System.Windows;
using System.Windows.Controls;
using TextDashboard.Resource;

namespace TextDashboard.Custom_Control
{
    [TemplatePart(Name = PartRootPanel, Type = typeof(Canvas))]
    public class SelfExpandablePanel: Canvas
    {
        const string PartRootPanel = "PART_RootPanel";

        static SelfExpandablePanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelfExpandableControl), new FrameworkPropertyMetadata(typeof(SelfExpandableControl)));
            
        }

        public SelfExpandablePanel()
        {
            //SetResourceReference(StyleProperty, "SelfExpandablePanelStyle");
            Events.UpdateControlStateEvent += OnUpdateStateEvent;
        }

        void OnUpdateStateEvent(object sender, State state)
        {
            var currentControl = sender as SelfExpandableControl;
            if (currentControl == null)
                return;

            var panel = GetTemplateChild(PartRootPanel) as Canvas;
            if(panel==null)
                return;
            for (var index = 0; index < panel.Children.Count; index++)
            {
                var control = panel.Children[index] as SelfExpandableControl;
                if (control == null || currentControl.Name == control.Name)
                {
                    currentControl.State = state;
                    if (currentControl.State == State.Activated && Panel.GetZIndex(currentControl) < 1)
                        Panel.SetZIndex(currentControl, Panel.GetZIndex(currentControl) + 1);
                }
                else
                {
                    control.State = state == State.Activated ? State.Deactivated : State.Normal;
                    Panel.SetZIndex(control, 0);
                }
            }
        }
    }
}
