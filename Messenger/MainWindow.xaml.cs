using System.Windows;
using System.Windows.Controls;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly MessengerController messengerController = new MessengerController();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (messengerController.Login(LoginBox.Text, PasswordBox.Password))
            {
                MessageBox.Show("You have entered!");
                Open(ContactsScreen);
            }
            else
            {
                MessageBox.Show("There is no user with such a username and password.");
            }
        }
        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            Open(AddContactScreen);
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            messengerController.AddContact(ContactLoginBox.Text);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Open(ContactsScreen);
        }
        private void Open(Border screen)
        {
            LoginScreen.Visibility = Visibility.Hidden;
            ContactsScreen.Visibility = Visibility.Hidden;
            AddContactScreen.Visibility = Visibility.Hidden;
            ChatScreen.Visibility = Visibility.Hidden;

            if (screen == ContactsScreen)
            {
                ContactsList.ItemsSource = messengerController.ContactsList.Contacts;
            }
            screen.Visibility = Visibility.Visible;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void ContactsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactsList.SelectedIndex >= 0)
            {
                messengerController.ChangeContact(ContactsList.SelectedIndex);
                ContactsList.SelectedIndex = -1;
                ChatName.Text = messengerController.CurrentContact.Name;
                messengerController.LoadChat();
                MessagesList.ItemsSource = messengerController.Chat;
                Open(ChatScreen);
            }
        }
        private void ChatBackButton_Click(object sender, RoutedEventArgs e)
        {
            Open(ContactsScreen);
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            messengerController.SendMessage(MessagerBox.Text);
            messengerController.LoadChat();
            MessagesList.ItemsSource = messengerController.Chat;
            MessagerBox.Text = "";
        }

    }
}
