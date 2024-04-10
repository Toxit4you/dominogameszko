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
        public Domino turn(Domino domino) {


            //12     01     00     20   
            //00     02     21     10 XD Fasz XD
            
            if (domino.sides[0,0] >0 && domino.sides[0, 1] > 0)
            {
                domino.sides[1, 0] = domino.sides[0, 1];
                domino.sides[0, 1] = 0;
                domino.vertical = false;
            }
            else if (domino.sides[0, 0] > 0 && domino.sides[1, 0] > 0)
            {
                domino.sides[0, 1] = domino.sides[0, 0];
                domino.sides[0, 0] = domino.sides[1, 0];
                domino.sides[1, 0] = 0;
                domino.vertical = true;
			}
            return domino; 

        }

        public void select(int index)
        {
           
            //Index will be equal with the index of the inhand domino
            
            selected_D.sides = inhand_Dominoes[index].sides;
            selected_D.vertical = inhand_Dominoes[index].vertical;

        }

		//                         V-- position of the selected map tile
		public Board place(int posX, int posY, Board board) 
        {
            bool goodtogo = true;
			for (int i = 0; i < board.size.GetLength(0); i++)
			{
				for (int j = 0; j < board.size.GetLength(1); j++)
				{
                    if (i == posY && j == posX)
					{
                        if (selected_D.vertical == true && board.size[i, j] != 0 || selected_D.vertical == true && board.size[i, j + 1] != 0)
                        {
                            goodtogo = false;
                        }
                        else if (selected_D.vertical == false && board.size[i, j] != 0 || selected_D.vertical == false && board.size[i + 1, j] != 0)
                        {
                            goodtogo = false;
                        }
                    }
				}
			}
			if (goodtogo)
			{
			    board.size[posY, posX] = selected_D.sides[0, 0];//place the selected part of the domino


			    //place down the  other half
			    if (selected_D.vertical == true)
			    {
					    board.size[posY, posX + 1] = selected_D.sides[0, 1];
			    }
			    else
			    {
					    board.size[posY + 1, posX] = selected_D.sides[1, 0];
		        }
				/*for (int i = 0; i < inhand_Dominoes.Count; i++)
				{
					if (inhand_Dominoes[i] == selected_D)
					{
                        inhand_Dominoes.RemoveAt(i);
					}
				}
                selected_D = null;*/
			}
            return board;
		}

	}
}
