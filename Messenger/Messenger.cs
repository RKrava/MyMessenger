using Messenger.Back;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Messenger
{
    [Serializable]
    public class MessengerController
    {
        public static string MyLogin;
        public User CurrentContact { get; set; }
        public ContactsList ContactsList = new ContactsList();
        public List<Message> Chat { get; set; }
        public MessengerController()
        {
            ContactsList.Contacts = new ObservableCollection<User>();

        }
        public void LoadChat()
        {
            Chat = new List<Message>();

            var db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM `messages` WHERE `sender_login` = @sL AND `receiver_login` = @rL OR `sender_login` = @rL AND `receiver_login` = @sL", db.GetConnection());
            sqlCommand.Parameters.Add("@sL", MySqlDbType.VarChar).Value = MyLogin;
            sqlCommand.Parameters.Add("@rL", MySqlDbType.VarChar).Value = CurrentContact.Login;

            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Message msg = new Message(Convert.ToInt32(table.Rows[i]["id"]), table.Rows[i]["text"].ToString(), table.Rows[i]["sender_login"].ToString(), table.Rows[i]["receiver_login"].ToString(), Convert.ToDateTime(table.Rows[i]["date"]));
                bool flag = false;
                foreach(var item in Chat)
                {
                    if(item.Text == msg.Text && item.date == msg.date)
                    {
                        flag = true;
                        break;
                    }
                }
                if(!flag)   Chat.Add(msg);
            }
        }
        public void ChangeContact(int index)
        {
            CurrentContact = ContactsList.Contacts[index];
            Chat = new List<Message>();
        }
        public void SendMessage(string text)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `messages` (`text`, `sender_login`, `receiver_login`, `date`) VALUES (@text, @sender_login, @receiver_login, @date)", db.GetConnection());
            command.Parameters.Add("@text", MySqlDbType.VarChar).Value = text;
            command.Parameters.Add("@sender_login", MySqlDbType.VarChar).Value = MyLogin;
            command.Parameters.Add("@receiver_login", MySqlDbType.VarChar).Value = CurrentContact.Login;
            command.Parameters.Add("@date", MySqlDbType.DateTime).Value = DateTime.Now;
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Send!");
            }
            else
            {
                MessageBox.Show("Error!");
            }

            db.closeConnection();
        }

        public bool Login(string loginUser, string passUser)
        {
            if (Back.Login.Loginned(loginUser, passUser))
            {
                MyLogin = loginUser;
                ContactsList = Desiaralize();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddContact(string contactlogin)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.GetConnection());
            sqlCommand.Parameters.Add("@uL", MySqlDbType.VarChar).Value = contactlogin;

            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                int id = Convert.ToInt32(table.Rows[0]["id"]);
                string name = table.Rows[0]["name"].ToString();
                string surname = table.Rows[0]["surname"].ToString();
                if (contactlogin == MyLogin)
                {
                    MessageBox.Show("You can`t add yourself!");
                }
                else
                {
                    var flag = false;
                    foreach (var item in ContactsList.Contacts)
                    {
                        if (item.Login == contactlogin)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        ContactsList.Contacts.Add(new User(id, contactlogin, name, surname));
                        MessageBox.Show("Success!");
                        Serialize();
                    }
                    else
                    {
                        MessageBox.Show("You already added this user!");
                    }
                }
            }
            else
            {
                MessageBox.Show("No user with this login!");
            }
        }
        public void Serialize()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ContactsList));

            using (FileStream fs = new FileStream("Contacts.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, ContactsList);
            }
        }
        public ContactsList Desiaralize()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ContactsList));
            using (FileStream fs = new FileStream("Contacts.xml", FileMode.OpenOrCreate))
            {
                    ContactsList contactList = (ContactsList)xml.Deserialize(fs);
                    return contactList;
            }
        }
    }
}
