using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominogameszko
{
    public class Board
    {

        public int[,] size;
        //0= empty, 1-6=domino value,7=placable,8=map border


        public string kep;

        public string Kep
        {
            get { return "pack://application:,,,/Dominogameszko;component/Resources/toothless.png"; }
            set { kep = value; }
        }
    }
}
