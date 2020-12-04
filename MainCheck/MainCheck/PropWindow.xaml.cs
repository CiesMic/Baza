using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class PropWindow : Window
    {
        Baza _baza;
        public PropWindow()
        {
            InitializeComponent();
            TxtPESEL.MaxLength = 11;
        }
        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            string _Name;
            string _Surname;
            long _PESEL;
            string _imgFile;
            string _MotherName;
            string _FatherName;
            Image _image = new Image();
            bool txtIsNotClean = false;
            bool txtIsValue = false;
            try
            {
                if (!String.IsNullOrEmpty(TxtName.Text) && !String.IsNullOrWhiteSpace(TxtName.Text) && !String.IsNullOrEmpty(TxtSurname.Text) && !String.IsNullOrWhiteSpace(TxtSurname.Text) && !String.IsNullOrEmpty(TxtPESEL.Text) && !String.IsNullOrWhiteSpace(TxtPESEL.Text) && !String.IsNullOrEmpty(TxtMotherName.Text) && !String.IsNullOrWhiteSpace(TxtMotherName.Text) && !String.IsNullOrEmpty(TxtFatherName.Text) && !String.IsNullOrWhiteSpace(TxtFatherName.Text))
                {
                    txtIsNotClean = true;
                }
                if (TxtPESEL.Text.Length == 11 && TxtName.Text.Length > 3 && TxtSurname.Text.Length > 3 && TxtMotherName.Text.Length > 3 && TxtFatherName.Text.Length > 3)
                {
                    txtIsValue = true;
                }
                if (txtIsNotClean && txtIsValue)
                {
                    _Name = TxtName.Text;
                    _Surname = TxtSurname.Text;
                    _PESEL = Convert.ToInt64(TxtPESEL.Text);
                    _MotherName = TxtMotherName.Text;
                    _FatherName = TxtFatherName.Text;
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                    ofd.Multiselect = false;
                    if (ofd.ShowDialog() == true)
                    {
                        _imgFile = ofd.FileName;
                    }
                    else
                    {
                        _imgFile = @"C:\Users\zwari\source\repos\Baza\MainCheck\MainCheck\bin\Debug\Pictures\Empty.png";
                    }
                    _baza = new Baza(_Name, _Surname, _PESEL, _imgFile, _MotherName, _FatherName);
                    MainWindow._bazaDanych.Add(_baza);
                    TxtName.Text = "";
                    TxtSurname.Text = "";
                    TxtPESEL.Text = "";
                    TxtMotherName.Text = "";
                    TxtFatherName.Text = "";
                }
            }
            catch
            {

            }
        }

        private void TxtPESEL_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key >= Key.D0 && e.Key < Key.D9 ))
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
            switch(textbox)
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
