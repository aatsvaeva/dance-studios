using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace KDZ_DanceStudios
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
            
        }

        private void buttonEnterAsGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.DanceStudiosPage);
            Pages.DanceStudiosPage.buttonEdit.IsEnabled=false;
            Logging.Log("Выполнен вход гостя");
        }

            private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Необходимо ввести имя");
                textBoxName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxSurname.Text))
            { 
                MessageBox.Show("Необходимо ввести фамилию");
                textBoxSurname.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxNewLogin.Text))
            { 
                MessageBox.Show("Необходимо ввести логин");
                textBoxNewLogin.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordBoxNew.Password))
            { 
                MessageBox.Show("Необходимо ввести пароль");
                passwordBoxNew.Focus();
                return;
            }

            using (var sr = new StreamReader("../../users.txt"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var parts = line.Split(':');
                    if (textBoxNewLogin.Text ==parts[0])
                    {
                        MessageBox.Show("Логин занят");
                        textBoxNewLogin.Clear();
                        textBoxNewLogin.Focus();
                        return;
                    }
                }
            }          
            using (FileStream fs = new FileStream("../../users.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"{textBoxNewLogin.Text}:{passwordBoxNew.Password}");
                }
            }
            MessageBox.Show("Вы зарегистрированы");
            textBoxName.Clear();
            textBoxSurname.Clear();
            textBoxNewLogin.Clear();
            passwordBoxNew.Clear();

            Logging.Log("Регистрация нового пользователя");
        }

        private void buttonEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var sr = new StreamReader("../../users.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var parts = line.Split(':');
                        if (textBoxLogin.Text == parts[0] && passwordBox.Password == parts[1])
                        {
                            NavigationService.Navigate(Pages.DanceStudiosPage);
                        }                      
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка чтения из файла");
            }

            Logging.Log("Вход зарегистрированного пользователя");
        }
    }
}
