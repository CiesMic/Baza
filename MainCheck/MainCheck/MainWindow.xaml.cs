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
using System.Windows.Threading;

namespace MainCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Baza> _bazaDanych = new List<Baza>();
        public static string tabName = "";
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            Clock.Content = DateTime.Now.ToString("HH:mm:ss");
            ClockTime();
            Sql_Count.Visibility = Visibility.Hidden;
            Sql_Add_But.Visibility = Visibility.Hidden;
            Sql_Create_But.Visibility = Visibility.Hidden;
            Sql_Update_But.Visibility = Visibility.Hidden;
            Serial.Visibility = Visibility.Hidden;
            ChangeBtn.Visibility = Visibility.Hidden;
            Properties.Visibility = Visibility.Hidden;
        }
        private void ClockTime()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Clock.Content = DateTime.Now.ToString("HH:mm:ss");
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
            if(_bazaDanych.Count > 0)
            {
                ReadDeserialize();
            }
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
                    ReadDeserialize();
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
            if (_bazaDanych.Count > 0)
            {
                Base tabela = new Base(tabName, "Add Items");
                tabela.Show();
            }
        }

        private void Sql_Count_Click(object sender, RoutedEventArgs e)
        {
            Base tabela = new Base(tabName, "CountAge");
            tabela.Show();
            Hide();
        }
        private void ReadDeserialize()
        {
            Sql_Count.Visibility = Visibility.Visible;
            Sql_Add_But.Visibility = Visibility.Visible;
            Sql_Create_But.Visibility = Visibility.Visible;
            Sql_Update_But.Visibility = Visibility.Visible;
            Serial.Visibility = Visibility.Visible;
            ChangeBtn.Visibility = Visibility.Visible;
            Properties.Visibility = Visibility.Visible;
        }

        private void Sql_Count_employees_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=DESKTOP-DELC1R0\MATRIXSERVER;Initial Catalog=Baza;User ID=sa;Password=AlgorytmDjikstry";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                string query = "Select Count(Id) AS ilosc_pracownikow, w.Nazwa_pracy FROM Base b INNER JOIN works w ON b.Id = w.Id_pracownika Group by w.Nazwa_pracy;";
                using (SqlCommand command = new SqlCommand(query, cnn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = result + reader.GetValue(0).ToString() + " - " + reader.GetValue(1) + "\n";
                        }
                    }
                }
                MessageBox.Show(result);
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Xmas_Click(object sender, RoutedEventArgs e)
        {
            XmasCard xmas = new XmasCard();
            xmas.Show();
        }

        private void Rep_Viewer_Click(object sender, RoutedEventArgs e)
        {
            Report_Viewer rep = new Report_Viewer();
            rep.Show();
        }
    }
}
