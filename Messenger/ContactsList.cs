using System;
using System.Collections.ObjectModel;

namespace Messenger
{
    [Serializable]
    public class ContactsList
    {
        public ObservableCollection<User> Contacts = new ObservableCollection<User>();

        public ContactsList()
        {

        }
    }
}
