using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для NewStudioWindow.xaml
    /// </summary>
    public partial class NewStudioWindow : Window
    {
        public NewStudioWindow(List<Metro> metro)
        {
            InitializeComponent();
            comboBoxMetro.ItemsSource = metro;
        }

        DanceStudios _newStudio;

        public DanceStudios NewStudio
        {
            get { return _newStudio; }
        }

    private void buttonAdd_Click(object sender, RoutedEventArgs e)
    {
        int rating;
        int price;
        if (string.IsNullOrWhiteSpace(textBoxName.Text))
        {
            MessageBox.Show("Необходимо ввести название");
            textBoxName.Focus();
            return;
        }
        if (comboBoxMetro.SelectedItem == null)
        {
            MessageBox.Show("Необходимо выбрать станцию метро");
            comboBoxMetro.Focus();
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

            _newStudio = new DanceStudios(textBoxName.Text, price, rating);
        _newStudio.Metro = comboBoxMetro.SelectedItem as Metro;
        DialogResult = true;
    }
}
    }
