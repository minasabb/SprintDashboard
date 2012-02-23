using System.Windows;
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
                if(control==null)
                    continue;
                if (currentControl.Name == control.Name)
                {
                    currentControl.State = state;
                }
                else
                    control.State = state == State.Activated ? State.Deactivated : State.Normal;

                if (state == State.Activated)
                {
                    if (Panel.GetZIndex(control) < 1)
                        Panel.SetZIndex(control, Panel.GetZIndex(control) + 1);
                }
                else
                    Panel.SetZIndex(control, 0);
            }
        }
    }
}
