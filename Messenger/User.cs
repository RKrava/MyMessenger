using System;

namespace Messenger
{
    [Serializable]
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public User()
        {

        }
        public User(int id, string login, string name, string surname)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Login = login;
        }
        
    }
}
