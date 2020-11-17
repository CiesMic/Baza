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
            new Baza("Mikolaj", "Kopernik", 8520147410),
            new Baza("Kacper", "Qwerty", 23052062148),
            new Baza("Zenom", "Karynia", 03526252892),
        });
            Refresh();
        }
        private void Refresh()
        {
            WriteList.ItemsSource = "";
            WriteList.ItemsSource = _bazaDanych;
        }
        private void Serial_Click(object sender, RoutedEventArgs e)
        {
            Stream str = File.Create(Environment.CurrentDirectory + "\\Test.txt");
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<Baza>));
            xmlSer.Serialize(str, _bazaDanych);
            str.Close();
            MessageBoxResult result = MessageBox.Show("Done", "Serialize", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
