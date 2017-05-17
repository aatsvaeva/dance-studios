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

    public partial class DanceStudiosWindow : Window
    {      
        List<DanceStudios> _studios = new List<DanceStudios>();
        List<DanceDirections> _direction = new List<DanceDirections>();

        public DanceStudiosWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void RefreshListBox()
        {
            listBoxStudios.Items.Clear();
            foreach (DanceStudios st in _studios)
            {            
                listBoxStudios.Items.Add(st.Info + "\n");               
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewStudioWindow(_direction);
            if (window.ShowDialog().Value)
            {
                _studios.Add(window.NewStudio);             
                SaveData();
                RefreshListBox();
            }
        }
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxStudios.SelectedIndex != -1)
            {
                _studios.RemoveAt(listBoxStudios.SelectedIndex);
                SaveData();
                RefreshListBox();
            }
        }

        private void listBoxStudios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            buttonRemove.IsEnabled = listBoxStudios.SelectedIndex != -1;
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
            }
            
            catch
            {
                MessageBox.Show("Ошибка чтения из файла");
            }
            RefreshListBox();           
        }
    }
}
