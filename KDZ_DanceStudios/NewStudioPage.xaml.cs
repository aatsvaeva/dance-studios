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
    /// Логика взаимодействия для NewStudioPage.xaml
    /// </summary>
    public partial class NewStudioPage : Page
    {
        public NewStudioPage()
        {
            InitializeComponent();
            LoadData();
        }

        DanceStudios _newStudio;

        public DanceStudios NewStudio
        {
            get { return _newStudio; }
        }

        List<DanceStudios> _studios;

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
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

            int rating;
            int price;
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Необходимо ввести название");
                textBoxName.Focus();
                return;
            }

            if (!int.TryParse(textBoxPrice.Text, out price))
            {
                MessageBox.Show("Некорректно задана цена");
                textBoxPrice.Focus();
                return;
            }

            if (!int.TryParse(textBoxRating.Text, out rating))
            {
                MessageBox.Show("Некорректное значение рейтинга");
                textBoxRating.Focus();
                return;
            }

            if (rating < 0 || rating > 10)
            {
                MessageBox.Show("Рейтинг должен быть от 0 до 10 включительно");
                textBoxRating.Focus();
                return;
            }

            if (comboBoxDanceDirections.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать направление");
                comboBoxDanceDirections.Focus();
                return;
            }

            _newStudio = new DanceStudios(textBoxName.Text, price, rating, comboBoxDanceDirections.Text);

            _studios.Add(_newStudio);
            dataGridStudios.ItemsSource = _studios;
            textBoxName.Clear();
            textBoxPrice.Clear();
            textBoxRating.Clear();
            comboBoxDanceDirections.SelectedItem=null;

            using (FileStream filest = new FileStream("../../studio.dat", FileMode.Open))
            {
                formatter = new BinaryFormatter();
                formatter.Serialize(filest, _studios);
            }
            Logging.Log("Добавлена новая студия");
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
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            dataGridStudios.SelectedCells.Clear();
            foreach (DanceStudios st in _studios)
            {
                dataGridStudios.ItemsSource = _studios;
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

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            var index = dataGridStudios.SelectedIndex + 1;
                foreach (var row in dataGridStudios.SelectedItems)
                {
                    DanceStudios removedStudio = row as DanceStudios;
                    _studios.Remove(removedStudio);
                }
                dataGridStudios.ItemsSource = null;
                dataGridStudios.ItemsSource = _studios;
                RefreshDataGrid();
                SaveData();
            
            Logging.Log("Удалена студия");
        }

       

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {          
            for (int i = 0; i < _studios.Count; i++)
                if (_studios[i].Name == textBoxSearch.Text)
                    (dataGridStudios.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.Plum;
                else if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    MessageBox.Show("Необходимо ввести название студии");
                    textBoxSearch.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("Студии нет в базе");
                    textBoxSearch.Focus();
                    return;
                }
            Logging.Log("Выполнен поиск по названию студии");
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
            Logging.Log("Сохранение обновленных данных");
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.DanceStudiosPage);
            Logging.Log("Переход на окно просмотра");
        }

        private void dataGridStudios_SelectionChsnged(object sender, SelectionChangedEventArgs e)
        {
            buttonRemove.IsEnabled = dataGridStudios.SelectedIndex != -1;
        }
    }
}
