using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDZ_DanceStudios
{
    [Serializable]
    public class DanceDirections
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _kinds;

        public string Kinds
        {
            get { return _kinds; }
            set { _kinds = value; }
        }

             public DanceDirections(string name, string kinds)
        {
            _name = name;
            _kinds = kinds;
        }
    }

}
