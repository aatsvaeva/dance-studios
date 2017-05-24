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

        private void RefreshListBox()
        {
            listBoxStudios.Items.Clear();
            foreach (DanceStudios st in _studios)
            {
                listBoxStudios.Items.Add(st.Name + " :  " + st.Price + "р.  " + st.Rating + ".0  " + st.Direction);
            }
        }

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

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _studios.Count; i++)
                if (_studios[i].Name == textBoxSearch.Text)
                    (listBoxStudios.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem).Background = Brushes.Plum;
                else if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    MessageBox.Show("Необходимо ввести название студии");
                    textBoxSearch.Focus();
                    return;
                }
            Logging.Log("Выполнен поиск по названию студии");
        }
    }
}
