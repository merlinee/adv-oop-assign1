using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace assign_1
{
    class AddStuff
    {
        private Dictionary<string, Day> myCalendar; 
        private List<DateTime> dateList;
        public AddStuff(Dictionary<string, Day> newCalendar, List<DateTime> newList)
        {
            myCalendar = newCalendar;
            dateList = newList;
        }
        string fileName = "MyCalendar.JSON";
        string dateFile = "DateList.JSON";

        void WriteJSON()
        {
            string jsonString = JsonSerializer.Serialize(myCalendar);
            File.WriteAllText(fileName, jsonString);
        }
 
        public void AddNewItem()
        {
            DateTime nowDate = DateTime.Now;
            Console.WriteLine("Input date in the format 'mm/dd/yyyy'.");
            string inputDate = Console.ReadLine();
            int[] dateArr = ValiDate(inputDate, nowDate);
            Console.WriteLine($"Length of dateArr {dateArr.Length}");
            Console.WriteLine($"First element of dateArr {dateArr[0]}");
            if(dateArr.Length != 3)
            {
                Console.WriteLine("That is not a valid date.");
                return;
            }
            else 
            {
                DateTime newDate = new DateTime(dateArr[2], dateArr[0], dateArr[1]);
                string strDate = newDate.ToString();
                Day newDay;
                if(myCalendar.ContainsKey(strDate))
                    newDay = myCalendar.GetValueOrDefault(strDate);
                else
                {
                    newDay = new Day(newDate);
                    InsertDateList(newDate);
                }

                if(!dateList.Contains(newDate))
                    InsertDateList(newDate);
                    
                Console.WriteLine("Enter '1' to add a new task, enter '2' to add a new meeting");
                string itemObj = Console.ReadLine();
                Console.WriteLine("Enter a title for the item.");
                string title = Console.ReadLine();
                Console.WriteLine("Enter details for the item.");
                string details = Console.ReadLine();

                int t = 3;
                TaskType type = TaskType.Personal;
                while(t > 2 || t < 0)
                {
                    t = SelectItemType();
                    if(t == 0)
                        type = TaskType.Personal;
                    else if(t == 1)
                        type = TaskType.School;
                    else if(t == 2)
                        type = TaskType.Work;
                    
                }
                if(itemObj == "1")
                {
                    Priority pLevel = Priority.Low;
                    int p = 3;
                    while(p > 2 || p < 0)
                    {
                        p = SelectPriority();
                        if(p == 0)
                            pLevel = Priority.Low;
                        else if(p == 1)
                            pLevel = Priority.Medium;
                        else if(p == 2)
                            pLevel = Priority.High;
                    }
                    if(pLevel == Priority.Low)
                        newDay.AddLowTask(new Task(type, title, details, newDate, pLevel));
                    else if(pLevel == Priority.High)
                        newDay.AddHighTask(new Task(type, title, details, newDate, pLevel));
                    else
                        newDay.AddMediumTask(new Task(type, title, details, newDate, pLevel));
                    myCalendar[strDate] = newDay;
                    WriteJSON();
                }
                else if(itemObj == "2")
                {
                    Boolean invalid = true;
                    while(invalid)
                    {
                        Console.WriteLine("Enter start time.");
                        int stHour = MeetingHour();
                        int stMin = MeetingMin();
                        Console.WriteLine("Enter end time.");
                        int etHour = MeetingHour();
                        int etMin = MeetingMin();

                        DateTime newStart = new DateTime(dateArr[2], dateArr[0], dateArr[1], stHour, stMin, 0);  
                        DateTime newEnd = new DateTime(dateArr[2], dateArr[0], dateArr[1], etHour, etMin, 0);  

                        if(newEnd > newStart)
                        {
                            invalid = false;
                            newDay.AddMeeting(new Meeting(type, title, details, newDate, newStart, newEnd));
                            myCalendar[strDate] = newDay;
                            WriteJSON();
                        }                            
                        else
                            Console.WriteLine("End time must be later than start time.");
                    }
                }
                else 
                    Console.WriteLine("Invalid entry.");
            }

        }

        int SelectItemType()
        {
            Console.WriteLine("Enter '0' for a personal item, '1' for a school item, or '2' for a work item.");
            string type = Console.ReadLine();
            if (Int32.TryParse(type, out int t))
            {
                if(t >= 0 && t < 3)
                    return t;
                else
                    return 3;
            } 
            else 
            {
                Console.WriteLine("Invalid entry.");
                return 3;
            }  
        }
        int SelectPriority()
        {
            Console.WriteLine("Enter '0' for a low priority task, '1' for a medium priority task, or '2' for a high priority task.");
            string prior = Console.ReadLine();
            if (Int32.TryParse(prior, out int p))
            {
                if(p >= 0 && p < 3)
                    return p;
                else
                    return 3;
            } 
            else 
            {
                Console.WriteLine("Invalid entry.");
                return 3;
            }     
        }

        int MeetingMin()
        {
            Boolean invalid = true;
            int m = 0;
            while(invalid)
            {
                Console.WriteLine("Enter minutes.");
                string min = Console.ReadLine();
                if (Int32.TryParse(min, out m))
                {
                    if(m > -1 && m < 60)
                        invalid = false;
                    else 
                        Console.WriteLine("Invalid entry.");
                } 
                else 
                    Console.WriteLine("Invalid entry.");  
            } 
            return m;
        }

        int MeetingHour()
        {
            Boolean invalid = true;
            int h = 0;
            while(invalid)
            {
                Console.WriteLine("Enter hour in 24 hour time.");
                string hour = Console.ReadLine();
                if (Int32.TryParse(hour, out h))
                {
                    if(h > -1 && h < 24)
                        invalid = false;
                    else 
                        Console.WriteLine("Invalid entry.");
                } 
                else 
                    Console.WriteLine("Invalid entry.");  
            } 
            return h;
        }

        int[] ValiDate(string inDate, DateTime now)
        {
            int[] invalid = new int[]{0};
            int[] valid = new int[3];

            string[] splitDate = inDate.Split('/');
            if(splitDate.Length != 3)
                return invalid;
            
            
            string month = splitDate[0];
            string day = splitDate[1];
            string year = splitDate[2];
            Console.WriteLine($"Month: {month} Day: {day} Year: {year}");
            Console.WriteLine($"Length of valid {valid.Length}");
            int y = ValiYear(year, now);
            if(y == 0)
                return invalid;
            else 
                valid[2] = y;
            
            Console.WriteLine($"Last elem of valid {valid[2]}");
            int m = ValiMonth(month, now);
            if(m == 0)
                return invalid;
            else
                valid[0] = m;
            
            Console.WriteLine($"First elem of valid {valid[0]}");

            int d = ValiDay(day, m, y, now);
            Console.WriteLine($"Int d is {d}");
            if(d == 0)
                return invalid;
            else
                valid[1] = d;

            Console.WriteLine($"Length of valid {valid.Length}");
            
            return valid;
        }

        int ValiMonth(string month, DateTime now)
        {
            Console.WriteLine("Outside if month");
            if (Int32.TryParse(month, out int m))
            {
                Console.WriteLine($"Inside if month int {m}");
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
                Console.WriteLine($"Inside if year int {y}");
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
                Console.WriteLine($"Inside if day int {d}");
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

        void InsertDateList(DateTime newDate)
        {
            if(dateList.Count > 0)
            {
                if(dateList[dateList.Count-1] < newDate)
                    dateList.Add(newDate);
                else 
                {
                    int i = 0;
                    Boolean cont = true;
                    while(i < dateList.Count && cont)
                    {
                        if(dateList[i] < newDate)
                            i++;
                        else
                        {
                            dateList.Insert(i,newDate);
                            cont = false;
                        }
                    }    
                }
            }
            else
                dateList.Add(newDate);

            string jsonString = JsonSerializer.Serialize(dateList);
            File.WriteAllText(dateFile, jsonString);
        }

        //0,1,4,6,7
        //Inserting 3


    }

}
