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
        public MainWindow()
        {
            InitializeComponent();
            Connect();
        }
        private void WriteFirstRow()
        {
            _bazaDanych.AddRange(new Baza[]
            {
            new Baza("Mikolaj", "Kopernik", 8520147410, Environment.CurrentDirectory + "\\Pictures\\Empty.png" , "Angelika", "Pawel"),
            new Baza("Kacper", "Qwerty", 23052062148, Environment.CurrentDirectory + "\\Pictures\\Empty.png", "Paulina", "Adam"),
            new Baza("Zenom", "Karynia", 03526252892, Environment.CurrentDirectory + "\\Pictures\\Empty.png", "Julka", "Stanislaw"),
        });
            Refresh();
        }
        private void Properties_Click(object sender, RoutedEventArgs e)
        {
            PropWindow _properties = new PropWindow();
            _properties.Show();
        }
        private void Refresh()
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
                string Name = (WriteList.SelectedItem as Baza).Name;
                string Surname = (WriteList.SelectedItem as Baza).Surname;
                long PESEL = Convert.ToInt64((WriteList.SelectedItem as Baza).PESEL);
                string MotherName = (WriteList.SelectedItem as Baza).MotherName;
                string FatherName = (WriteList.SelectedItem as Baza).FatherName;
                string _imgFile = (WriteList.SelectedItem as Baza)._imgFile;
                int i = WriteList.SelectedIndex;
                ToChange change = new ToChange(Name, Surname, PESEL, MotherName, FatherName, i, _imgFile);
                change.Show();
            }
        }
        private void Connect()
        {
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=DESKTOP-DELC1R0\MATRIXSERVER;Initial Catalog=Baza;User ID=sa;Password=AlgorytmDjikstry";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            MessageBox.Show("Connection Open  !");
            cnn.Close();
        }
    }
}
