using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace dominogameszko
{
    public class Player
    {
        public int points;
        private List<Domino> inhand_Dominoes;
        public Domino selected_D;
        public string name;

        //I need to rewrite the turning method to make sure its working correctly, A vilag szep az eg kek
        public int[,] turn(int[,] domino) {


            //12     01     00     20   
            //00     02     21     10 XD Fasz XD
            
            if (domino[0,0] >0 && domino[0, 1] > 0)
            {
                domino[1, 0] = domino[0, 1];
                domino[0, 1] = 0;
            }
            else if (domino[0, 0] > 0 && domino[1, 0] > 0)
            {
                domino[0, 1] = domino[0, 0];
                domino[0, 0] = domino[1, 0];
                domino[1, 0] = 0;
			}
            /*else if (domino[1, 1] > 0 && domino[1, 0] > 0)
            {
                int temp = domino[1, 0];

                domino[1, 0] = domino[1, 1];
                domino[0, 0] = temp;
                domino[1, 1] = 0;
            }
            else
            {  
                int temp = domino[0, 0];

                domino[0, 0] = domino[1, 1];
                domino[0, 1] = temp;
                domino[1, 1] = 0;
            }*/

            return domino; 

        }

        public void select(int index)
        {
           
            //Index will be equal with the index of the inhand domino
            
            selected_D.sides = inhand_Dominoes[index].sides;
            selected_D.vertical = inhand_Dominoes[index].vertical;

        }

		//index in the small matrix --V                   V-- position of the selected map tile
		public void place(int smallY, int smallX, int posX, int posY) {

			Board board = new Board();

			board.size[posY, posX] = selected_D.sides[smallY, smallX];//place the selected part of the domino


			//place down the  other half
			if (selected_D.vertical == true)
			{
				if ( smallX > 0)
				{
					board.size[posY, posX - 1] = selected_D.sides[smallY, smallX - 1];
				}
				else
				{
					board.size[posY, posX + 1] = selected_D.sides[smallY, smallX + 1];
				}
			}
			else
			{
				if (smallY > 0)
				{
					board.size[posY - 1, posX] = selected_D.sides[smallY - 1, smallX];
				}
				else
				{
					board.size[posY + 1, posX] = selected_D.sides[smallY + 1, smallX];
				}
			}

		}

	}
}
