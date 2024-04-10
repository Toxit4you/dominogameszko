using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominogameszko
{
    public class Board
    {

        public int[,] values = new int[50, 50];
        //0= empty, 1-6=domino value,7=placable,8=ASZ,9=BAS,10=BSZ,11=BFS,12=FSZ,13=JFS,14=JSZ,15=JAS
    }
}
