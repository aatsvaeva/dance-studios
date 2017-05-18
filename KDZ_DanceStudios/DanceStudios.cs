using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDZ_DanceStudios
{
    [Serializable]
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
        private int _rating;

        public int Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }
        private string _direction;

        public string Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public DanceStudios (string name,int price, int rating,string direction)
        {
            _name = name;
            _price = price;
            _rating = rating;
            _direction=direction;
        }             
    }
}
