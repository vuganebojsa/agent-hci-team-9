using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgent.view
{
    /// <summary>
    /// Interaction logic for SideNavigation.xaml
    /// </summary>
    public partial class SideNavigation : UserControl
    {
        public SideNavigation()
        {

            InitializeComponent();
            btnQuestion.btnSideNav.HorizontalContentAlignment = HorizontalAlignment.Center;

        }

        public event EventHandler ButtonRezervisaniClicked;

        private void NavigationButton_ButtonClicked(object sender, EventArgs e)
        {
            ButtonRezervisaniClicked?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ButtonPregledClicked;

        private void btnPregled_ButtonClicked(object sender, EventArgs e)
        {
            ButtonPregledClicked?.Invoke(this, EventArgs.Empty);

        }
    }
}
