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
using System.Windows.Shapes;

namespace KDZ_DanceStudios
{
    /// <summary>
    /// Логика взаимодействия для NewStudioWindow.xaml
    /// </summary>
    public partial class NewStudioWindow : Window
    {
        public NewStudioWindow(List<DanceDirections> direction)
        {
            InitializeComponent();
            comboBoxDanceDirections.ItemsSource = direction;
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

            DanceDirections direction;
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


            direction = comboBoxDanceDirections.SelectedItem as DanceDirections;
            _newStudio = new DanceStudios(textBoxName.Text, price, rating, direction);
            //_newStudio.DanceDirections = comboBoxDanceDirections.SelectedItem as DanceDirections;

            _studios.Add(_newStudio);
            dataGridStudios.ItemsSource = _studios;

            using (FileStream filest = new FileStream("../../studio.dat", FileMode.Open))
            {
                formatter = new BinaryFormatter();
                formatter.Serialize(filest, _studios);
            }
            // DialogResult = true; закрывает окно
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

               /* _direction.Add(new DanceDirections("Современные танцы"));
                _direction.Add(new DanceDirections("Латиноамериканские танцы"));
                _direction.Add(new DanceDirections("Народные танцы"));
                _direction.Add(new DanceDirections("Бальные танцы"));
                _direction.Add(new DanceDirections("Уличные танцы"));
            }*/

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
                dataGridStudios.ItemsSource=_studios;
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
            if (index==0)
            {
                MessageBox.Show("Необходимо выбрать элемент");
            }
           else
            {
                _studios.RemoveAt(index - 1);
                dataGridStudios.ItemsSource = null;               
                dataGridStudios.ItemsSource = _studios;
                RefreshDataGrid();
                SaveData();
               
            }
        }
        
        private void dataGridStudios_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            buttonRemove.IsEnabled = dataGridStudios.SelectedIndex != -1;
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _studios.Count; i++)
                if (_studios[i].Name == textBoxSearch.Text)
                    (dataGridStudios.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.HotPink;
                else if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    MessageBox.Show("Необходимо ввести название для поиска");
                    textBoxName.Focus();
                    return;
                }

        }

        private void EditDataGrid(object sender, DataGridRowEditEndingEventArgs e)
        {
            var studio = e.Row.Item as DanceStudios;
        }
    }
}
