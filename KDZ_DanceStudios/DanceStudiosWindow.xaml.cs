using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        const string FileName = "studios.txt";
        List<DanceStudios> _studios = new List<DanceStudios>();
        List<DanceDirections> _direction = new List<DanceDirections>();

        public DanceStudiosWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void RefreshListBox()
        {
            listBoxStudios.ItemsSource = null;
            listBoxStudios.ItemsSource = _studios;
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
            using (var sw = new StreamWriter(FileName))
            {
                foreach (var stud in _studios)
                {
                    sw.WriteLine($"{stud.Name}:{stud.Price}:{stud.Rating}:{stud.DanceDirections.Name}:{stud.DanceDirections.Kinds}");
                }
            }
        }

        private void LoadData()
        {
            try
            {
                _studios = new List<DanceStudios>();
                _direction = new List<DanceDirections>();

                using (var sr = new StreamReader(FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var parts = line.Split(':');
                        if (parts.Length == 5)
                        {

                            int i = 0;
                            while (i < _direction.Count && _direction[i].Name != parts[2])
                                i++;
                            DanceDirections d;
                            if (i < _direction.Count)
                                d = _direction[i];  
                            else
                            {
                                d = new DanceDirections(parts[3], parts[4]);
                                _direction.Add(d);
                            }

                            var dancestudio = new DanceStudios(parts[0], int.Parse(parts[1]), int.Parse(parts[2]));
                            dancestudio.DanceDirections = d;
                            _studios.Add(dancestudio);

                        }
                    }
                    _direction.Add(new DanceDirections("Современные танцы", "Джаз-модерн, RNB dance, Джаз-фанк, Go-Go, C-Walk, House, Шаффл, Контемп, Реггетон, Vogue"));
                    _direction.Add(new DanceDirections("Латиноамериканские танцы", "Самба, Ча-ча-ча, Румба, Пасодобль, Джайв, Бачата"));
                    _direction.Add(new DanceDirections("Народные танцы", "Африканские танцы, Восточные танцы, Bollywood dance, Лезгинка, Русские народные"));
                    _direction.Add(new DanceDirections("Бальные танцы", "Медленный вальс, Венский вальс, Танго, Фокстрот, Квикстеп"));
                    _direction.Add(new DanceDirections("Уличные танцы", "Хип-Хоп, Брейк-данс, Поппинг, Локинг, Krump, Dancehall"));

                }
               
            }
            
            catch
            {
                MessageBox.Show("Ошибка чтения из файла");
            }
            RefreshListBox();           
        }
    }
}
