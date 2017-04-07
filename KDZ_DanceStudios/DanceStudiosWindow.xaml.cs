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
        List<Metro> _metro = new List<Metro>();
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
            var window = new NewStudioWindow(_metro);
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

        private void listBoxLecturers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            buttonRemove.IsEnabled = listBoxStudios.SelectedIndex != -1;
        }
        private void SaveData()
        {
            using (var sw = new StreamWriter(FileName))
            {
                foreach (var stud in _studios)
                {
                    sw.WriteLine($"{stud.Name}:{stud.Metro.Name}:{stud.Metro.Address}:{stud.Price}:{stud.Rating}");
                }
            }
        }

        private void LoadData()
        {
            try
            {
                _studios = new List<DanceStudios>();
                _metro = new List<Metro>();

                using (var sr = new StreamReader(FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var parts = line.Split(':');
                        if (parts.Length == 4)
                        {

                            int i = 0;
                            while (i < _metro.Count && _metro[i].Name != parts[2])
                                i++;
                            Metro f;
                            if (i < _metro.Count)
                                f = _metro[i];  
                            else
                            {
                                f = new Metro(parts[2], parts[3]);
                                _metro.Add(f);
                            }

                            var dancestudio = new DanceStudios(parts[0], int.Parse(parts[1]), int.Parse(parts[2]));
                            dancestudio.Metro = f;
                            _studios.Add(dancestudio);
                        }
                    }


                }
            }
            catch (FileNotFoundException)
            {
                _metro.Add(new Metro("Новокузнецкая", "Пятницкая 5"));
            }
            catch
            {
                MessageBox.Show("Ошибка чтения из файла");
            }
            RefreshListBox();
        }
    }
}
