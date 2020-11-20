using Microsoft.Win32;
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
    /// Interaction logic for ToChange.xaml
    /// </summary>
    public partial class ToChange : Window
    {
        private int i = 0;
        private string _imgFile = "";
        public ToChange()
        {
            InitializeComponent();
        }
        public ToChange(string Name, string Surname, long PESEL, string MotherName, string FatherName, int i, string _imgFile)
        {
            InitializeComponent();
            TxtName.Text = Name;
            TxtSurname.Text = Surname;
            TxtPESEL.Text = Convert.ToString(PESEL);
            TxtMotherName.Text = MotherName;
            TxtFatherName.Text = FatherName;
            this.i = i;
            this._imgFile = _imgFile;
        }

        private void But_Chg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._bazaDanych[i].Name = TxtName.Text;
            MainWindow._bazaDanych[i].Surname = TxtSurname.Text;
            MainWindow._bazaDanych[i].PESEL = Convert.ToInt64(TxtPESEL.Text);
            MainWindow._bazaDanych[i].MotherName = TxtMotherName.Text;
            MainWindow._bazaDanych[i].FatherName = TxtFatherName.Text;
            MainWindow._bazaDanych[i].Name = TxtName.Text;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                _imgFile = ofd.FileName;
                MainWindow._bazaDanych[i]._imgFile = this._imgFile;
            }
        }
    }
}
