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
    /// Логика взаимодействия для MasterClassPage.xaml
    /// </summary>
    public partial class MasterClassPage : Page
    {
        public MasterClassPage()
        {
            InitializeComponent();
            StudiosNames();
            LoadData();
            comboBoxStudios.ItemsSource = _stiudiosName;
        }

        MasterClass _newMasterClass;

       public MasterClass NewMasterClass
        {
            get { return _newMasterClass; }
        }

        List<MasterClass> _masterClasses;
        List<string> _stiudiosName = new List<string>();
        //Методы
        private void StudiosNames()
        {
            LoadData();
            using (var sr = new StreamReader("../../studio.txt"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var parts = line.Split(':');
                    var studiosName = parts[0];
                    _stiudiosName.Add(studiosName);
                }
            }
        }
        private void LoadData()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream filest = new FileStream("../../masterclass.dat", FileMode.OpenOrCreate))
                {
                    try
                    {
                        _masterClasses = (List<MasterClass>)formatter.Deserialize(filest);
                    }
                    catch
                    {
                        _masterClasses = new List<MasterClass>();
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
            dataGridMasterClass.SelectedCells.Clear();
            foreach (MasterClass mc in _masterClasses)
            {
                dataGridMasterClass.ItemsSource = _masterClasses;
            }
        }

        private void SaveData()
        {
            using (FileStream filest = new FileStream("../../masterclass.dat", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(filest, _masterClasses);
            }
        }

        //Кнопки
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream filest = new FileStream("../../masterclass.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    _masterClasses = (List<MasterClass>)formatter.Deserialize(filest);
                }
                catch
                {
                    _masterClasses = new List<MasterClass>();
                }
            }

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

            if (comboBoxStudios.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать направление");
                comboBoxStudios.Focus();
                return;
            }

            _newMasterClass = new MasterClass(textBoxName.Text, price, comboBoxStudios.Text);
            _masterClasses.Add(_newMasterClass);
            dataGridMasterClass.ItemsSource = _masterClasses;
            textBoxName.Clear();
            textBoxPrice.Clear();
            comboBoxStudios.SelectedItem = null;

            using (FileStream filest = new FileStream("../../masterclass.dat", FileMode.Open))
            {
                formatter = new BinaryFormatter();
                formatter.Serialize(filest, _masterClasses);
            }
            Logging.Log("Добавлена информация о мастер классе");
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            var index = dataGridMasterClass.SelectedIndex + 1;
            foreach (var row in dataGridMasterClass.SelectedItems)
            {
                MasterClass removedMasterClass = row as MasterClass;
                _masterClasses.Remove(removedMasterClass);
            }
            dataGridMasterClass.ItemsSource = null;
            dataGridMasterClass.ItemsSource = _masterClasses;
            RefreshDataGrid();
            SaveData();

            Logging.Log("Удалена информация о мастер классе");
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            for (int i = 0; i < _masterClasses.Count; i++)
            {
                if (_masterClasses[i].Name == textBoxSearch.Text)
                {
                    dataGridMasterClass.UpdateLayout();
                    dataGridMasterClass.ScrollIntoView(dataGridMasterClass.Items[i]);
                    (dataGridMasterClass.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Background = Brushes.LightGray;
                    (dataGridMasterClass.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow).Focus();
                }

                else if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    MessageBox.Show("Необходимо ввести название мастер класса");
                    textBoxSearch.Focus();
                    return;
                }
                Logging.Log("Выполнен поиск по названию мастер класса");
            }
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

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            textBoxSearch.Clear();
            Logging.Log("Обновлен список мастер классов");
        }

        //Обработчики событий
        private void dataGridMasterClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonRemove.IsEnabled = dataGridMasterClass.SelectedIndex != -1;
        }     
    }
}  

