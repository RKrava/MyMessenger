using Messenger.Back;
using MySql.Data.MySqlClient;
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

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "Type name") NameBox.Text = "";
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
           if(NameBox.Text == "") NameBox.Text = "Type name";
        }

        private void SurnameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBox.Text == "Type surname") SurnameBox.Text = "";
        }

        private void SurnameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SurnameBox.Text == "") SurnameBox.Text = "Type surname";
        }
        private void LoginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "Type login") LoginBox.Text = "";
        }

        private void LoginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "") LoginBox.Text = "Type login";
        }
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "Type password") PasswordBox.Text = "";
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "") PasswordBox.Text = "Type password";
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)", db.GetConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginBox.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = PasswordBox.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = NameBox.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = SurnameBox.Text;
            db.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Error!");
            }

            db.closeConnection();
        }
    }
}
