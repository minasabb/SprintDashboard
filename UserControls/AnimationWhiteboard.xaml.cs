using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextDashboard
{
	/// <summary>
	/// Interaction logic for AnimationWhiteboard.xaml
	/// </summary>
	public partial class AnimationWhiteboard : UserControl
	{
		public AnimationWhiteboard()
		{
			this.InitializeComponent();

            var storyboard = new Storyboard();
            var doubleAnimation = new DoubleAnimation(100, new Duration(TimeSpan.Zero));
            Storyboard.SetTarget(doubleAnimation, this);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(WidthProperty));
            storyboard.Children.Add(doubleAnimation);

            var storyboard2 = new Storyboard();
            var doubleAnimation2 = new DoubleAnimation(200, new Duration(TimeSpan.Zero));
            Storyboard.SetTarget(doubleAnimation2, this);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath(WidthProperty));
            storyboard2.Children.Add(doubleAnimation2);

            var stateGroup = new VisualStateGroup { Name = "MouseOverState" };
            stateGroup.States.Add(new VisualState { Name = "MouseOut", Storyboard = storyboard });
            stateGroup.States.Add(new VisualState { Name = "MouseOver", Storyboard = storyboard2 });

            var sgs = VisualStateManager.GetVisualStateGroups(this);
            sgs.Add(stateGroup);

		}

        private void rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
        }

        private void rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOut", true);
        }

	}
}