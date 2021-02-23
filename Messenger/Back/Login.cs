using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Back
{
    static class Login
    {
        public static bool Loginned(String login, String pass)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.GetConnection());
            sqlCommand.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
            sqlCommand.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            return table.Rows.Count > 0;
        }
    }
}
