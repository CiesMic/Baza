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
using System.Data.SqlClient;

namespace MainCheck
{
    /// <summary>
    /// Interaction logic for Base.xaml
    /// </summary>
    public partial class Base : Window
    {
        private string choose = "";
        private int i = 0;
        public Base(string table, string choose)
        {
            InitializeComponent();
            DoSomething.Content = choose;
            this.choose = choose;
            TableName.Text = table;
        }
        public Base(string table, string choose, int i)
        {
            InitializeComponent();
            DoSomething.Content = choose;
            TableName.Text = table;
            this.choose = choose;
            this.i = i;
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
        private void BaseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length >= 2 || textBox.Text.Length == 0)
            {
                Warning.Content = "";
            }
            else if (textBox.Text.Length < 2)
            {
                Warning.Content = " must have at least 3 letters";
            }
        }

        private void DoSomething_Click(object sender, RoutedEventArgs e)
        {
            if (TableName.Text.Length >= 2)
            {
                switch (choose)
                {
                    case "Read":
                        Read();
                        break;
                    case "Add Items":
                        AddItems();
                        break;
                    case "Add Item":
                        Add();
                        break;
                    case "Update":
                        Update();
                        break;
                }
            }
        }
        private void Read()
        {
            string tableName = TableName.Text;
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=DESKTOP-DELC1R0\MATRIXSERVER;Initial Catalog=Baza;User ID=sa;Password=AlgorytmDjikstry";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = "SELECT * FROM " + tableName;
                using (SqlCommand command = new SqlCommand(query, cnn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Baza _baza = new Baza
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                PESEL = Convert.ToInt64(reader["PESEL"]),
                                _imgFile = (string)reader["imgFile"],
                                MotherName = (string)reader["MotherName"],
                                FatherName = (string)reader["FatherName"],
                                Age = Convert.ToInt32(reader["Age"])
                            };
                            MainWindow._bazaDanych.Add(_baza);
                        }
                    }
                }
                cnn.Close();
                SentMessage(tableName);
                MainWindow.tabName = tableName;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void AddItems()
        {
            string tableName = TableName.Text;
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=DESKTOP-DELC1R0\MATRIXSERVER;Initial Catalog=Baza;User ID=sa;Password=AlgorytmDjikstry";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                string query = "Create Table " + tableName;
                query += "(Id int not null,";
                query += "Name varchar(255) not null,";
                query += "Surname varchar (255) not null,";
                query += "PESEL char(11) not null,";
                query += "MotherName varchar(255) not null,";
                query += "FatherName varchar(255) not null,";
                query += "imgFile varchar(255) null,";
                query += "Age int not null);";
                SqlCommand myCommand = new SqlCommand(query, cnn);
                myCommand.ExecuteNonQuery();
                cnn.Close();
                cnn.Open();
                for (int x = 0; x < MainWindow._bazaDanych.Count; x++)
                {
                    query = "";
                    query = "INSERT INTO " + tableName;
                    query += " VALUES (@Id, @Name, @Surname, @PESEL, @MotherName, @FatherName, @imgFile, @Age)";

                    myCommand = new SqlCommand(query, cnn);
                    myCommand.Parameters.AddWithValue("@Id", MainWindow._bazaDanych[x].Id);
                    myCommand.Parameters.AddWithValue("@Name", MainWindow._bazaDanych[x].Name);
                    myCommand.Parameters.AddWithValue("@Surname", MainWindow._bazaDanych[x].Surname);
                    myCommand.Parameters.AddWithValue("@PESEL", MainWindow._bazaDanych[x].PESEL);
                    myCommand.Parameters.AddWithValue("@MotherName", MainWindow._bazaDanych[x].MotherName);
                    myCommand.Parameters.AddWithValue("@FatherName", MainWindow._bazaDanych[x].FatherName);
                    myCommand.Parameters.AddWithValue("@imgFile", MainWindow._bazaDanych[x]._imgFile);
                    myCommand.Parameters.AddWithValue("@Age", MainWindow._bazaDanych[x].Age);

                    myCommand.ExecuteNonQuery();
                }
                cnn.Close();
                SentMessage(tableName);
                MainWindow.tabName = tableName;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Add()
        {
            string tableName = TableName.Text;
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=DESKTOP-DELC1R0\MATRIXSERVER;Initial Catalog=Baza;User ID=sa;Password=AlgorytmDjikstry";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                int Id = MainWindow._bazaDanych[i].Id;
                string Name = MainWindow._bazaDanych[i].Name;
                string Surname = MainWindow._bazaDanych[i].Surname;
                long PESEL = MainWindow._bazaDanych[i].PESEL;
                string MotherName = MainWindow._bazaDanych[i].MotherName;
                string FatherName = MainWindow._bazaDanych[i].FatherName;
                string _imgFile = MainWindow._bazaDanych[i]._imgFile;
                int Age = MainWindow._bazaDanych[i].Age;
                string query = "INSERT INTO " +tableName;
                query += " VALUES (@Id, @Name, @Surname, @PESEL, @MotherName, @FatherName, @imgFile, @Age)";

                SqlCommand myCommand = new SqlCommand(query, cnn);

                myCommand.Parameters.AddWithValue("@Id", Id);
                myCommand.Parameters.AddWithValue("@Name", Name);
                myCommand.Parameters.AddWithValue("@Surname", Surname);
                myCommand.Parameters.AddWithValue("@PESEL", PESEL);
                myCommand.Parameters.AddWithValue("@MotherName", MotherName);
                myCommand.Parameters.AddWithValue("@FatherName", FatherName);
                myCommand.Parameters.AddWithValue("@imgFile", _imgFile);
                myCommand.Parameters.AddWithValue("@Age", Age);

                myCommand.ExecuteNonQuery();

                cnn.Close();
                SentMessage(tableName);
                MainWindow.tabName = tableName;
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Update()
        {
            string tablename = TableName.Text;
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=DESKTOP-DELC1R0\MATRIXSERVER;Initial Catalog=Baza;User ID=sa;Password=AlgorytmDjikstry";
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                int Id = MainWindow._bazaDanych[i].Id;
                string Name = MainWindow._bazaDanych[i].Name;
                string Surname = MainWindow._bazaDanych[i].Surname;
                long PESEL = MainWindow._bazaDanych[i].PESEL;
                string MotherName = MainWindow._bazaDanych[i].MotherName;
                string FatherName = MainWindow._bazaDanych[i].FatherName;
                string _imgFile = MainWindow._bazaDanych[i]._imgFile;
                int Age = MainWindow._bazaDanych[i].Age;

                string query = "UPDATE Base SET Name = @Name, Surname = @Surname, PESEL = @PESEL, MotherName = @MotherName, FatherName = @FatherName, imgFile = @imgFile, Age = @Age WHERE Id = @Id";
                SqlCommand myCommand = new SqlCommand(query, cnn);

                myCommand.Parameters.AddWithValue("@Id", Id);
                myCommand.Parameters.AddWithValue("@Name", Name);
                myCommand.Parameters.AddWithValue("@Surname", Surname);
                myCommand.Parameters.AddWithValue("@PESEL", PESEL);
                myCommand.Parameters.AddWithValue("@MotherName", MotherName);
                myCommand.Parameters.AddWithValue("@FatherName", FatherName);
                myCommand.Parameters.AddWithValue("@imgFile", _imgFile);
                myCommand.Parameters.AddWithValue("@Age", Age);

                myCommand.ExecuteNonQuery();
                cnn.Close();
                SentMessage(tablename);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SentMessage(string command)
        {
            MessageBox.Show(command + ": Done", "Done");
        }
    }
}
