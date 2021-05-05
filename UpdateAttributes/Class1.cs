using System;
using System.Collections.Generic;

namespace Card_Live
{
    public class UpdateAttributes
    {
        public int blood_now_inwater;
        public int blood_now_inhungry;
        public List<int> Update(int blood,int water,int hungry,int day) {
            Random rd = new Random();
            int temp = rd.Next(1, 3);
            water = water + (day - rd.Next(0,day))  * temp;
            hungry = hungry + (day - rd.Next(0, day))  * temp;
            if (water >= 20)
            {
                double blood_temp = (double)blood;
                blood_temp = (double)Math.Floor(Convert.ToDecimal(water) / 5);
                blood_now_inwater = blood - Convert.ToInt32(blood_temp);
                blood = blood_now_inwater; //根据水量减血
            }
            if (hungry >= 20)
            {
                double hungry_temp = (double)hungry;
                hungry_temp = (double)Math.Floor(Convert.ToDecimal(hungry) / 6);
                blood_now_inhungry = blood - Convert.ToInt32(hungry_temp);
                blood = blood_now_inhungry; //根据饥饿减血
            }
            //准备返回
            List<int> result = new List<int>();
            result.Add(blood);
            result.Add(water);
            result.Add(hungry);
            //返回
            return result;

        }
    }
}
 