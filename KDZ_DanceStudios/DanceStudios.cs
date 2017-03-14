using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDZ_DanceStudios
{
    class DanceStudios
    {
            private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _metro;

        public string Metro
        {
            get { return _metro; }
            set { _metro = value; }
        }
        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private double _rating;

        public double Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        public DanceStudios(string name, string metro, int price, double rating)
        {
            _name = name;
            _metro = metro;
            _price = price;
            _rating = rating;
        }

    }
}
