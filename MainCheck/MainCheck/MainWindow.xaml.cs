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
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Data.SqlClient;

namespace MainCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Baza> _bazaDanych = new List<Baza>();
        public static string tabName = "";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void WriteFirstRow()
        {
            _bazaDanych.AddRange(new Baza[]
            {
            new Baza(_bazaDanych.Count + 1,"Mikolaj", "Kopernik", 8520147410, Environment.CurrentDirectory + "\\Pictures\\Empty.png" , "Angelika", "Pawel", 35),
            new Baza(_bazaDanych.Count + 1,"Kacper", "Qwerty", 23052062148, Environment.CurrentDirectory + "\\Pictures\\Empty.png", "Paulina", "Adam", 97),
            new Baza(_bazaDanych.Count + 1,"Zenom", "Karynia", 03526252892, Environment.CurrentDirectory + "\\Pictures\\Empty.png", "Julka", "Stanislaw", 17),
        });
            Refresh();
        }
        private void Properties_Click(object sender, RoutedEventArgs e)
        {
            PropWindow _properties = new PropWindow();
            _properties.Show();
        }
        public void Refresh()
        {
            WriteList.ItemsSource = "";
            WriteList.ItemsSource = _bazaDanych;
        }
        private void Serial_Click(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlSerializer(_bazaDanych.GetType());
            using (var writer = XmlWriter.Create(Environment.CurrentDirectory + "\\Test.txt"))
            {
                serializer.Serialize(writer, _bazaDanych);
                MessageBoxResult result = MessageBox.Show("Done", "Serialize", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
        protected void But_Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private void Deserial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var reader = new StreamReader(Environment.CurrentDirectory + "\\Test.txt"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Baza>),
                    new XmlRootAttribute("ArrayOfBaza"));
                    _bazaDanych = (List<Baza>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
                WriteFirstRow();
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WriteList.SelectedItem != null)
            {
                int Id = (WriteList.SelectedItem as Baza).Id;
                string Name = (WriteList.SelectedItem as Baza).Name;
                string Surname = (WriteList.SelectedItem as Baza).Surname;
                long PESEL = Convert.ToInt64((WriteList.SelectedItem as Baza).PESEL);
                string MotherName = (WriteList.SelectedItem as Baza).MotherName;
                string FatherName = (WriteList.SelectedItem as Baza).FatherName;
                string _imgFile = (WriteList.SelectedItem as Baza)._imgFile;
                int Age = (WriteList.SelectedItem as Baza).Age;
                int i = WriteList.SelectedIndex;
                ToChange change = new ToChange(Id, Name, Surname, PESEL, MotherName, FatherName, i, _imgFile, Age);
                change.Show();
            }
        }

        private void Sql_Read_But_Click(object sender, RoutedEventArgs e)
        {
            Base tabela = new Base(tabName, "Read");
            tabela.Show();
        }

        private void Sql_Add_But_Click(object sender, RoutedEventArgs e)
        {
            if (WriteList.SelectedItem != null)
            {
                Base tabela = new Base(tabName, "Add Item", WriteList.SelectedIndex);
                tabela.Show();
            }
        }

        private void Sql_Mod_But_Click(object sender, RoutedEventArgs e)
        {
            if (WriteList.SelectedItem != null)
            {
                Base tabela = new Base(tabName, "Update", WriteList.SelectedIndex);
                tabela.Show();
            }
        }

        private void Sql_Create_But_Click(object sender, RoutedEventArgs e)
        {
            Base tabela = new Base(tabName, "Add Items");
            tabela.Show();
        }
    }
}
