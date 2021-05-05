using System;
using System.Collections.Generic;

namespace Card_Live
{
    public class ParsingCard
    {
        public List<int> Parsing(List<int> now,int result)
        {
            //List<int>:[甲,乙,丙,丁]
            switch (result)
            {
                case 1:
                    now[0]++;
                    break;
                case 2:
                case 3:
                    now[1]++;
                    break;
                case 4:
                case 5:
                case 6:
                    now[2]++;
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    now[3]++;
                    break;
            }
            return now;
        }

    }
}
