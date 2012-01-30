using System;
using System.Collections.ObjectModel;
using System.Windows;
using TextDashboard.Custom_Control;
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for SecondView.xaml
    /// </summary>
    public partial class SecondView : ResizableContentControl
    {
        public SecondView()
        {
            InitializeComponent();
            SetupDesignTimeModel();
            Events.UpdateContentEvent+= UpdateContentEvent;
        }

        void UpdateContentEvent(object sender)
        {
            var control = sender as ResizableContentControl;
            if (control == null || control.Name != Name || PlainDataGridSprintStyle.Visibility != Visibility.Hidden)
                return;
            PlainDataGridSprintStyle.Visibility = Visibility.Visible;
            Loading.Visibility = Visibility.Collapsed;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(field.Text)) //&& PlainDataGridSprintStyle.Visibility == Visibility.Collapsed)
            {
                Loading.Visibility = Visibility.Visible;
                PlainDataGridSprintStyle.Visibility = Visibility.Hidden;

                //Loading.Visibility = Visibility.Collapsed;
            }
        }


        private void SetupDesignTimeModel()
        {
            var portInNumbers = new ObservableCollection<PortInNumber>();

            var alphaTeam = new PortInNumber("604-789-5656", "some text", "12234 -data");
            portInNumbers.Add(alphaTeam);
            var betaTeam = new PortInNumber("604-789-1234", "some text ...", "k890w -data");
            portInNumbers.Add(betaTeam);
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            portInNumbers.Add(new PortInNumber("604-789-6543", "some text", "54667 -data"));
            DataContext = new AllNumbers { PortInNumbers = portInNumbers };
        }

        public class PortInNumber
        {
            #region Construction

            public PortInNumber(string id, string text, string data)
            {
                PhoneNumber = id;
                Text = text;
                Data = data;
            }

            #endregion

            #region Implementation of IPersonAge
            public string PhoneNumber { get; set; }
            public string Text { get; set; }
            public string Data { get; set; }

            #endregion
        }

        public class AllNumbers
        {
            #region Properties

            public ObservableCollection<PortInNumber> PortInNumbers { get; set; }

            #endregion
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //field.Text = "";
            //if (PlainDataGridSprintStyle.Visibility != Visibility.Collapsed)
            //{
            PlainDataGridSprintStyle.Visibility = Visibility.Collapsed;
            //Loading.Visibility=Visibility.Collapsed;
            //}
        }

        private void TextBlock_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if((bool)e.NewValue)
            //    UpdatedContent = true;
        }
    }
}
