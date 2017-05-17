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
        public DanceDirections(string name)
        {
            _name = name;
        }
    }
}
