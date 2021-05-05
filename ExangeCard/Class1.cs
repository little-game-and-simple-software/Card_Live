using System;
using System.Collections.Generic;

namespace Card_Live
{
    public class ExangeCard
    {
        public List<int> Foods(List<int> cards) { 
            //Cards:[A,B,C,D]
            //优先使用高等卡片兑换
            if (cards[0] > 0)
            {
                cards[0]--;
                return cards;
            }
            else if(cards[1] > 2)
            {
                cards[1] = cards[1] - 3;
                return cards;
            }
            else if(cards[2] > 4)
            {
                cards[2] = cards[2] - 5;
                return cards;
            }
            else if (cards[3] > 7)
            {
                cards[3] = cards[3] - 8;
                return cards;
            }
            else
            {
                List<int> error = new List<int>();
                return error;
            }
        }
        public List<int> Water(List<int> cards)
        {
            //Cards:[A,B,C,D]
            //优先使用高等卡片兑换
            if (cards[0] > 0)
            {
                cards[0]--;
                return cards;
            }
            else if (cards[1] > 1)
            {
                cards[1] = cards[1] - 2;
                return cards;
            }
            else if (cards[2] > 2)
            {
                cards[2] = cards[2] - 3;
                return cards;
            }
            else if (cards[3] > 4)
            {
                cards[3] = cards[3] - 5;
                return cards;
            }
            else
            {
                List<int> error = new List<int>();
                return error;
            }
        }
    }
}
