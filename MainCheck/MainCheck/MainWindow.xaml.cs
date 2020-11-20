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
    }
}
