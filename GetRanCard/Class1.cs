using System;

namespace Card_Live
{
    public class GetRanCard
    {
        public int Get()
        {
            //1:甲 2:乙 3:丙 4:丁 
            Random rd = new Random();
            int temp = rd.Next(1,11);
            return temp;

        }
    }
}
