using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDZ_DanceStudios
{
   public class DanceStudios
    {
            private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        public string Info
        {
            get
            {
                return $"{_name} - {_metro.Name} - {_metro.Address} - {_price} - {_rating} ";
            }
        }

        private Metro _metro;

        public Metro Metro
        {
            get { return _metro; }
            set { _metro = value; }
        }



        public DanceStudios (string name,int price, int rating)
        {
            _name = name;
            _price = price;
            _rating = rating;
        }

    }
}
