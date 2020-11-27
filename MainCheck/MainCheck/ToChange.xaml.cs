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
            TxtPESEL.MaxLength = 11;
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
        private void TxtPESEL_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key >= Key.D0 && e.Key < Key.D9))
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void BaseName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
            }
            else
            {
                e.Handled = true;
            }
        }
        private void TxtPESEL_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPESEL.Text.Length == 11 || TxtPESEL.Text.Length == 0)
            {
                WarningPesel.Content = "";
            }
            else if (TxtPESEL.Text.Length < 11)
            {
                WarningPesel.Content = "PESEL must have 11 numbers";
            }
        }
        private void BaseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            object label = labels(textBox.Name);
            string war = "";
            if (textBox.Text.Length > 3 || textBox.Text.Length == 0)
            {
                war = "";
                Warnings(textBox.Name, war);
            }
            else if (textBox.Text.Length < 3)
            {
                war = label + " must have at least 3 letters";
                Warnings(textBox.Name, war);
            }
        }
        private void Warnings(object textbox, string war)
        {
            switch (textbox)
            {
                case "TxtName":
                    WarningName.Content = war;
                    break;
                case "TxtSurname":
                    WarningSurname.Content = war;
                    break;
                case "TxtMotherName":
                    WarningMotherName.Content = war;
                    break;
                case "TxtFatherName":
                    WarningFatherName.Content = war;
                    break;
            }
        }
        private object labels(object textbox)
        {
            switch (textbox)
            {
                case "TxtName":
                    return LName.Content;
                case "TxtSurname":
                    return LSurname.Content;
                case "TxtMotherName":
                    return LMotherName.Content;
                case "TxtFatherName":
                    return LFatherName.Content;
            }
            return null;
        }
    }
}
