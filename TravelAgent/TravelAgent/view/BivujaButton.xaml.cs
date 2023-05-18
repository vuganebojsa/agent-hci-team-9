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
using System.ComponentModel;

namespace TravelAgent
{
    /// <summary>
    /// Interaction logic for BivujaButton.xaml
    /// </summary>
    public partial class BivujaButton : UserControl, INotifyPropertyChanged
    {
        public BivujaButton()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string buttonContent;

        public string ButtonContent
        {
            get { return buttonContent; }
            set
            {
                buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));

            }
        }
    }
}
