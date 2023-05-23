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
    /// Interaction logic for YesNoPopup.xaml
    /// </summary>
    public partial class YesNoPopup : Window
    {
        public YesNoPopup()
        {
            InitializeComponent();
        }

        public YesNoPopup(String text)
        {

            InitializeComponent();
            OkText = text;

        }

        private string okText;

        public string OkText
        {
            get { return okText; }
            set
            {
                okText = value;
                poruka.Text = okText;
            }
        }

        public event EventHandler ButtonYesClicked;
        public event EventHandler ButtonNoClicked;

        private void btnNo_ButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = false;
            ButtonNoClicked?.Invoke(this, EventArgs.Empty);

        }

        private void btnYes_ButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = true;
            ButtonYesClicked?.Invoke(this, EventArgs.Empty);

        }
    }
}
