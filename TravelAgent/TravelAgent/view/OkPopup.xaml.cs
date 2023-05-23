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
    /// Interaction logic for OkPopup.xaml
    /// </summary>
    public partial class OkPopup : Window
    {
        public OkPopup()
        {
            InitializeComponent();
        }
        public OkPopup(String text)
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

        private void BivujaButton_ButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
