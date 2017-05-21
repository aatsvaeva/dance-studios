using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class DanceStudiosWindow : Window
    {      
        List<DanceStudios> _studios = new List<DanceStudios>();
        List<DanceDirections> _direction = new List<DanceDirections>();

        public DanceStudiosWindow()
        {
            InitializeComponent();
            LoadData();
            Logging.Log("Программа запущена");
        }

        private void RefreshListBox()
        {
            listBoxStudios.Items.Clear();          
                foreach (DanceStudios st in _studios)
                {
                listBoxStudios.Items.Add(st.Name + " :  " + st.Price + "р.  " + st.Rating + ".0  " + st.Direction);
                }
            listBoxStudios.Items.SortDescriptions.Clear();
            listBoxStudios.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Rating", System.ComponentModel.ListSortDirection.Ascending));
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewStudioWindow(_direction);
            window.Show();
            this.Close();
            Logging.Log("Открыто окно редактирования");
            
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
                    _direction.Add(new DanceDirections("Современные танцы"));
                    _direction.Add(new DanceDirections("Латиноамериканские танцы"));
                    _direction.Add(new DanceDirections("Народные танцы"));
                    _direction.Add(new DanceDirections("Бальные танцы"));
                    _direction.Add(new DanceDirections("Уличные танцы"));
                    _direction.Add(new DanceDirections("Восточные танцы"));
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

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            Logging.Log("Обновлен список студий");

        }
    }
}
