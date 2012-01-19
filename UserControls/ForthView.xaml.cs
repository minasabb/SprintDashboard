using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextDashboard.Resource;

namespace TextDashboard.UserControls
{
    /// <summary>
    /// Interaction logic for ForthView.xaml
    /// </summary>
    public partial class ForthView : UserControl
    {
        public ForthView()
        {
            InitializeComponent();
            SetupDesignTimeModel();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(field.Text)) //&& PlainDataGridSprintStyle.Visibility==Visibility.Collapsed)
            {
                PlainDataGridSprintStyle.Visibility = Visibility.Visible;
                Events.IncreaseSize(this,0.5);
            }
            this.Refresh();
        }

        private void SetupDesignTimeModel()
        {
            var portInNumbers = new ObservableCollection<PortInNumber>();

            var alphaTeam = new PortInNumber("604-789-5656", "some text", "12234 -data");
            portInNumbers.Add(alphaTeam);
            var betaTeam = new PortInNumber("604-789-1234", "some text ...", "k890w -data");
            portInNumbers.Add(betaTeam);
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
            field.Text = "";
            //if (PlainDataGridSprintStyle.Visibility != Visibility.Collapsed)
            //{
                PlainDataGridSprintStyle.Visibility = Visibility.Collapsed;
                Events.DecreaseSize(this);
            this.Refresh();
            //}
        }
    }
}
