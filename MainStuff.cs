using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace assign_1
{
    class MainStuff
    {

        private Dictionary<string, Day> myCalendar; 
        string fileName = "MyCalendar.JSON";
        string dateFile = "DateList.JSON";

        private List<DateTime> dateList;
        public void RunCalendar()
        {
            while(true)
            {
                Console.WriteLine("Please select from the below options:\n");
                Console.WriteLine("Enter '1' to add a new item.");
                Console.WriteLine("Enter '2' to view full calendar.");
                Console.WriteLine("Enter '3' to view current month.");
                Console.WriteLine("Enter '4' to view current week.");
                Console.WriteLine("Enter '5' to view current day.");
                Console.WriteLine("Enter '10' to quit.");
                string choiceEntry = Console.ReadLine();
                int choice = 0;
                if(Int32.TryParse(choiceEntry, out int c))
                    choice = c;
                else
                    choice = 0;
                switch(choice)
                {
                    case 1:
                        ReadJSON();
                        AddStuff addNew = new AddStuff(myCalendar, dateList);
                        addNew.AddNewItem();
                        break;
                    case 2:
                        ReadJSON();
                        DisplayCalendar(dateList);
                        break;
                    case 3:
                        ReadJSON();
                        int month = DateTime.Now.Month;
                        var dates = dateList.Where(date => date.Month == month);
                        List<DateTime> monthDates = dates.ToList();
                        DisplayCalendar(monthDates);
                        break;
                    case 4:
                        ReadJSON();
                        List<DateTime> weekDates = new List<DateTime>{ConvertDateTime(DateTime.Now), ConvertDateTime(DateTime.Now.AddDays(1)),ConvertDateTime(DateTime.Now.AddDays(2)),
                        ConvertDateTime(DateTime.Now.AddDays(3)),ConvertDateTime(DateTime.Now.AddDays(4)), ConvertDateTime(DateTime.Now.AddDays(5)), ConvertDateTime(DateTime.Now.AddDays(6))};
                        dates = dateList.Where(date => weekDates.Contains(date));
                        List<DateTime> newWeekDates = dates.ToList();
                        DisplayCalendar(newWeekDates);
                        break;
                    case 5:
                        ReadJSON();
                        int nextDay = 1;
                        DateTime day = DateTime.Now;
                        while(nextDay != 2)
                        {
                            DisplayDay(day);
                            Console.WriteLine("Press 1 to view the next day or 2 to go to menu.");
                            string nextEntry = Console.ReadLine();
                            if(Int32.TryParse(nextEntry, out int t))
                                nextDay = t;
                            else
                            {
                                Console.WriteLine("Invalid entry. Returning to menu.");
                                nextDay = 2;
                            }
                            day = day.AddDays(1);
                        }

                        break;
                    case 10:
                        return;
                    default:
                        Console.WriteLine("Invalid entry.");
                        break;
                }

            }
        }

        DateTime ConvertDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        void ReadJSON()
        {
            string jsonString = File.ReadAllText(fileName);
            if(jsonString == "")
                myCalendar = new Dictionary<string, Day>();
            else
                myCalendar = JsonSerializer.Deserialize<Dictionary<string, Day>>(jsonString);
            string jsonDateString = File.ReadAllText(dateFile);
            if(jsonDateString == "")
                dateList = new List<DateTime>();
            else
                dateList = JsonSerializer.Deserialize<List<DateTime>>(jsonDateString);
        }

        void DisplayCalendar(List<DateTime> dates)
        {
            foreach(var date in dates)
            {
                string strDate = date.ToString();

                Day entry = myCalendar[strDate];
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Date: {entry.myDate}\n");
                foreach(var task in entry.myTasks)
                {
                    Console.WriteLine($"Task Title: {task.itemTitle}");
                    Console.WriteLine($"Task Details: {task.itemDetails}");
                    Console.WriteLine($"Task Type: {task.itemType}");
                    Console.WriteLine($"Task Priority: {task.taskPriority}");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
                foreach(var meeting in entry.myMeetings)
                {
                    Console.WriteLine($"Meeting Title: {meeting.itemTitle}");
                    Console.WriteLine($"Meeting Details: {meeting.itemDetails}");
                    Console.WriteLine($"Meeting Type: {meeting.itemType}");
                    Console.WriteLine($"Meeting Start: {meeting.startTime}");
                    Console.WriteLine($"Meeting Meeting: {meeting.endTime}");
                    Console.WriteLine();
                }
            }
        }

        void DisplayDay(DateTime day)
        {
            string strDate = ConvertDateTime(day).ToString();
            if(!myCalendar.ContainsKey(strDate))
                Console.WriteLine($"There is no entry for this date: {strDate}");
            else
            {
                Day entry = myCalendar[strDate];
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Date: {entry.myDate}\n");
                Console.WriteLine();
                foreach(var task in entry.myTasks)
                {
                    Console.WriteLine($"Task Title: {task.itemTitle}");
                    Console.WriteLine($"Task Details: {task.itemDetails}");
                    Console.WriteLine($"Task Type: {task.itemType}");
                    Console.WriteLine($"Task Priority: {task.taskPriority}");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
                foreach(var meeting in entry.myMeetings)
                {
                    Console.WriteLine($"Meeting Title: {meeting.itemTitle}");
                    Console.WriteLine($"Meeting Details: {meeting.itemDetails}");
                    Console.WriteLine($"Meeting Type: {meeting.itemType}");
                    Console.WriteLine($"Meeting Start: {meeting.startTime}");
                    Console.WriteLine($"Meeting Meeting: {meeting.endTime}");
                    Console.WriteLine();
                }
            }
        }

    }
}
