using System;

namespace Card_Live
{
    public class GiveMoney
    {

        public int give(int day)
        {
            int start = 200;
            Random rd = new Random();
            int c = rd.Next(1,10);
            int give = 200 - day*c;
            return give;
        }
    }
}
