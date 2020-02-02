using System;

namespace assign_1
{
    class ValidDay
    {
        public int[] ValiDate(string inDate, DateTime now)
        {
            int[] invalid = new int[]{0};
            int[] valid = new int[3];

            string[] splitDate = inDate.Split('/');
            if(splitDate.Length != 3)
                return invalid;
            
            
            string month = splitDate[0];
            string day = splitDate[1];
            string year = splitDate[2];
            int y = ValiYear(year, now);
            if(y == 0)
                return invalid;
            else 
                valid[2] = y;
            
            int m = ValiMonth(month, now);
            if(m == 0)
                return invalid;
            else
                valid[0] = m;

            int d = ValiDay(day, m, y, now);
            if(d == 0)
                return invalid;
            else
                valid[1] = d;
            
            return valid;
        }

        int ValiMonth(string month, DateTime now)
        {
            if (Int32.TryParse(month, out int m))
            {
                if(m >= now.Month && m < 13)
                    return m;
                else
                    return 0;
            } 
            else
                return 0;
        }

        int ValiYear(string year, DateTime now)
        {
            
            if (Int32.TryParse(year, out int y))
            {
                if(y >= now.Year && y < now.Year + 10)
                    return y;
                else
                    return 0;
            } 
            else
                return 0;
        }

        int ValiDay(string day, int month, int year, DateTime now)
        {
            if (Int32.TryParse(day, out int d))
            {
                if(d > 0 && d < 32)
                {
                    if(month == now.Month && d < now.Day)
                        return 0;
                    else if(month == 2)
                    {
                        if(year % 4 != 0 && d < 29)
                            return d;
                        else if(d < 30)
                            return d;
                        else 
                            return 0;
                    }
                    else if((month == 9 || month == 4 || month == 6 || month == 11) && d < 31)
                        return d;
                    else
                        return d;

                } 
                else 
                    return 0;
            } 
            else
                return 0;
        }        
    }
}