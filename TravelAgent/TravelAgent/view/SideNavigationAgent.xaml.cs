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
    /// Interaction logic for SideNavigationAgent.xaml
    /// </summary>
    public partial class SideNavigationAgent : UserControl
    {
        public SideNavigationAgent()
        {
            InitializeComponent();
            question.btnSideNav.HorizontalContentAlignment = HorizontalAlignment.Center;

        }

        public event EventHandler ButtonUpravljanjePutovanjimaClicked;

        private void btnUpravljanjePutovanjimaClicked(object sender, EventArgs e)
        {
            ButtonUpravljanjePutovanjimaClicked?.Invoke(this, EventArgs.Empty);
        }


        public event EventHandler ButtonUpravljanjeTuristickimAtrakcijama;

        private void btnUpravljanjeTuristickimAtrakcijama(object sender, EventArgs e)
        {
            ButtonUpravljanjeTuristickimAtrakcijama?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ButtonUpravljanjeSmestajemIRestoranima;

        private void btnUpravljanjeSmestajemIRestoranima(object sender, EventArgs e)
        {
            ButtonUpravljanjeSmestajemIRestoranima?.Invoke(this, EventArgs.Empty);
        }


        public event EventHandler ButtonPregledProdatihPutovanja;

        private void btnPregledProdatihPutovanja(object sender, EventArgs e)
        {
            ButtonPregledProdatihPutovanja?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ButtonHelp;

        private void btnHelpClicked(object sender, EventArgs e)
        {
            ButtonHelp?.Invoke(this, EventArgs.Empty);
        }



    }
}
