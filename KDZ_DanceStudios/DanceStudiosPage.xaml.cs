using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Логика взаимодействия для DanceStudiosPage.xaml
    /// </summary>
    public partial class DanceStudiosPage : Page
    {
        List<DanceStudios> _studios = new List<DanceStudios>();

        public DanceStudiosPage()
        {
            InitializeComponent();
            LoadData();
        }

        //Методы
        private void RefreshListBox()
        {
            listBoxStudios.Items.Clear();
            foreach (DanceStudios st in _studios)
            {
                listBoxStudios.Items.Add(st.Name + " :  " + st.Price + "р.  " + st.Rating + ".0  " + st.Direction);
            }
        }

        private void SaveData()
        {
            using (FileStream filest = new FileStream("../../studio.dat", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(filest, _studios);
            }
        }

        private void LoadData()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream filest = new FileStream("../../studio.dat", FileMode.OpenOrCreate))
                {
                    try
                    {
                        _studios = (List<DanceStudios>)formatter.Deserialize(filest);
                    }
                    catch
                    {
                        _studios = new List<DanceStudios>();
                    }
                }             
            }

            catch
            {
                MessageBox.Show("Ошибка чтения из файла");
            }
            RefreshListBox();
        }

        //Кнопки
        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.NewStudioPage);
            Logging.Log("Открыто окно редактирования");
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            textBoxSearch.Clear();
            Logging.Log("Обновлен список студий");

        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            for (int i = 0; i < _studios.Count; i++)
                if (_studios[i].Name == textBoxSearch.Text)
                {
                    listBoxStudios.UpdateLayout();
                    listBoxStudios.ScrollIntoView(listBoxStudios.Items[i]);
                    (listBoxStudios.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem).Background = Brushes.LightGray;
                    (listBoxStudios.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem).Focus();
                }
                else if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    MessageBox.Show("Необходимо ввести название студии");
                    textBoxSearch.Focus();
                    return;
                }
            Logging.Log("Выполнен поиск по названию студии");
        }

        private void buttonBackToAuthorization_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.Authorization);
            Logging.Log("Переход на окно регистрации");
        }

        private void buttonToMasterClass_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.MasterClassPage);
            Logging.Log("Переход на окно с информацией о мастер классах");
        }


        //Обработчики событий
        private void buttonEdit_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonEdit.FontSize = 14;
        }

        private void buttonEdit_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonEdit.FontSize = 12;
        }

        private void buttonRefresh_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonRefresh.FontSize = 14;
        }

        private void buttonRefresh_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonRefresh.FontSize = 12;
        }

        private void buttonBackToAuthorization_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonBackToAuthorization.FontSize = 14;
        }

        private void buttonBackToAuthorization_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonBackToAuthorization.FontSize = 12;
        }

        private void buttonToMasterClass_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonToMasterClass.FontSize = 14;
        }

        private void buttonToMasterClass_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonToMasterClass.FontSize = 12;
        }
    }
}
