﻿using System;
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

            _newStudio = new DanceStudios(textBoxName.Text, price, rating);
            _newStudio.DanceDirections = comboBoxDanceDirections.SelectedItem as DanceDirections;
            _studios.Add(_newStudio);
            using (FileStream filest = new FileStream("../../studio.dat", FileMode.Open))
            {
                formatter = new BinaryFormatter();
                formatter.Serialize(filest, _studios);
            }
            DialogResult = true;
           
        }
    }
}
