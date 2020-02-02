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
            SelectStuff select = new SelectStuff();
            ValidDay vali = new ValidDay();
            DateTime nowDate = DateTime.Now;
            Console.WriteLine("Input date in the format 'mm/dd/yyyy'.");
            string inputDate = Console.ReadLine();
            int[] dateArr = vali.ValiDate(inputDate, nowDate);
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
                    t = select.SelectItemType();
                    if(t < 3)
                        type = select.ChosenItemType(t);
                }
                if(itemObj == "1")
                {
                    Priority pLevel = Priority.Low;
                    int p = 3;
                    while(p > 2 || p < 0)
                    {
                        p = select.SelectPriority();
                        if(p < 3)
                            pLevel = select.ChosenPriority(p);
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
                        int stHour = select.MeetingHour();
                        int stMin = select.MeetingMin();
                        Console.WriteLine("Enter end time.");
                        int etHour = select.MeetingHour();
                        int etMin = select.MeetingMin();

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
    }

}
