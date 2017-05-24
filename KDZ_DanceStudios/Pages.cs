using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDZ_DanceStudios
{
    static class Pages
    {
        private static DanceStudiosPage _danceStudiosPage = new DanceStudiosPage();
        private static NewStudioPage _newStudioPage = new NewStudioPage();
        private static Authorization _authorization = new Authorization();

        public static DanceStudiosPage DanceStudiosPage
        {
            get { return _danceStudiosPage; }
        }

        public static NewStudioPage NewStudioPage
        {
            get { return _newStudioPage; }
        }
        public static Authorization Authorization
        {
            get { return _authorization; }
        }
    }
}
