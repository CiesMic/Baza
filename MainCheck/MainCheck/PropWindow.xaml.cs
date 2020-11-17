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
using System.Windows.Shapes;

namespace MainCheck
{
    /// <summary>
    /// Interaction logic for PropWindow.xaml
    /// </summary>
    public partial class PropWindow : Window
    {
        Baza _baza;
        public PropWindow()
        {
            InitializeComponent();
        }
        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            string _Name;
            string _Surname;
            long _PESEL;
            try
            {
                if (!String.IsNullOrEmpty(TxtName.Text) && !String.IsNullOrWhiteSpace(TxtName.Text) && !String.IsNullOrEmpty(TxtSurname.Text) && !String.IsNullOrWhiteSpace(TxtSurname.Text) && !String.IsNullOrEmpty(TxtPESEL.Text) && !String.IsNullOrWhiteSpace(TxtPESEL.Text))
                {

                }
            }
            catch
            {

            }
        }
    }
}
