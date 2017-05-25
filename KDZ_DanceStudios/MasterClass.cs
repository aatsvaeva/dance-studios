using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDZ_DanceStudios
{
    [Serializable]
    public class MasterClass
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

        private string _studioName;

        public string StudioName
        {
            get { return _studioName; }
            set { _studioName = value; }
        }


        public MasterClass(string name, int price, string studioName)
        {
            _name = name;
            _price = price;
            _studioName = studioName;
        }
    }
}
