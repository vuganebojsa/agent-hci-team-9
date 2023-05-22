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
    /// Interaction logic for NavigationButton.xaml
    /// </summary>
    public partial class NavigationButton : UserControl
    {
        public NavigationButton()
        {
            InitializeComponent();
        }

        public event EventHandler ButtonClicked;

        private void btnSideNav_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private string buttonContent;

        public string ButtonContent
        {
            get { return buttonContent; }
            set
            {
                buttonContent = value;
                tbBtnNav.Text = buttonContent;
            }
        }
    }
}
