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
            int i = 1;
            foreach(var date in dates)
            {
                string strDate = date.ToString();

                Day entry = myCalendar[strDate];
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"{i}. Date: {entry.myDate}\n");
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
                    Console.WriteLine($"Meeting End: {meeting.endTime}");
                    Console.WriteLine();
                }
                i++;
            }

            Console.WriteLine("Enter 9999 to return to menu.");
            Console.WriteLine("Enter the number of the day in order to edit or delete an item on that day.");
            string choiceEntry = Console.ReadLine();
            if(Int32.TryParse(choiceEntry, out int c))
            {
                if(c == 9999 || c > dates.Count || c < 1)
                    return;
                else
                    DeleteUpdateStuff(dates[c-1]);
            }
            else
                return;            
        }

        void DeleteUpdateStuff(DateTime date)
        {
            int choice = 9999;
            string choiceEntry = "";
            while(choice > 2 || choice < 1)
            {
            Console.WriteLine("Would you like to delete the entire day? Type 1 if yes or 2 to move to individual items.");
            choiceEntry = Console.ReadLine();

            if(Int32.TryParse(choiceEntry, out int c))
                choice = c;
            else
                choice = 9999;
            }

            if(choice == 1)
            {
                while(choice > 4 || choice < 3)
                {
                    Console.WriteLine("Are you sure you want to delete the entire day? You will not be able to retrieve it or any items on that day.");
                    Console.WriteLine("Press 3 to return to menu, or 4 to confirm.");
                    choiceEntry = Console.ReadLine();
                    if(Int32.TryParse(choiceEntry, out int c))
                    {
                        if(c != 4)
                            return;
                        else
                        {
                            choice = c;
                            myCalendar.Remove(date.ToString());
                            dateList.Remove(date);
                            DeleteWriteJSON();
                            Console.WriteLine("Day deleted.");
                        }
                            
                    }
                    else
                        choice = 9999;
                }
            } 
            else 
            {
                DisplayDay(date);
                Day editDay = myCalendar[date.ToString()];
                UpdateStuff update = new UpdateStuff();
                int s = 9999;
                choice = 9999;
                while(choice > 5 || choice < 1)
                {
                    Console.WriteLine("Enter 1 to edit a task, 2 to edit a meeting, 3 to delete a task, or 4 to delete a meeting.");
                    Console.WriteLine("Otherwise enter 5 to return to the main menu.");
                    choiceEntry = Console.ReadLine();
            
                    if(Int32.TryParse(choiceEntry, out int c))
                        choice = c;
                    else
                        choice = 9999;
                }
                
                if(choice == 1 || choice == 3)
                {
                    s = SelectItemNumber(editDay.myTasks.Count);
                    Task editTask = editDay.myTasks[s-1];
                    editDay.RemoveTask(s-1);
                    if(choice == 1)
                        editDay.AddTask(update.UpdateTask(editTask));
                }
                else if(choice == 2 || choice == 4)
                {
                    s = SelectItemNumber(editDay.myMeetings.Count);
                    Meeting editMeeting = editDay.myMeetings[s-1];
                    editDay.RemoveMeeting(s-1);
                    if(choice == 2)
                        editDay.AddMeeting(update.UpdateMeeting(editMeeting));
                }
                else 
                    return;

                myCalendar[date.ToString()] = editDay;
                DeleteWriteJSON();
            } 
        }

        int SelectItemNumber(int count)
        {
            int s = 9999;
            while(s > count+1 || s < 1)
            {
                Console.WriteLine("Enter the number of the item you would like to edit or delete.");
                string choiceEntry = Console.ReadLine();
            
                if(Int32.TryParse(choiceEntry, out int c))
                    s = c;
                else
                    s = 9999;
            }
            return s;
        }

        void DeleteWriteJSON()
        {
            string jsonString = JsonSerializer.Serialize(myCalendar);
            File.WriteAllText(fileName, jsonString);
            jsonString = JsonSerializer.Serialize(dateList);
            File.WriteAllText(dateFile, jsonString);
        }

        void DisplayDay(DateTime day)
        {
            string strDate = ConvertDateTime(day).ToString();
            if(!myCalendar.ContainsKey(strDate))
                Console.WriteLine($"There is no entry for this date: {strDate}");
            else
            {
                int i = 1;
                int j = 1;
                int k = 1;
                Day entry = myCalendar[strDate];
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Date: {entry.myDate}\n");
                Console.WriteLine();
                foreach(var task in entry.myTasks)
                {
                    Console.WriteLine($"{j}. Task Title: {task.itemTitle}");
                    Console.WriteLine($"Task Details: {task.itemDetails}");
                    Console.WriteLine($"Task Type: {task.itemType}");
                    Console.WriteLine($"Task Priority: {task.taskPriority}");
                    Console.WriteLine();
                    j++;
                }
                Console.WriteLine();
                Console.WriteLine();
                foreach(var meeting in entry.myMeetings)
                {
                    Console.WriteLine($"{k}. Meeting Title: {meeting.itemTitle}");
                    Console.WriteLine($"Meeting Details: {meeting.itemDetails}");
                    Console.WriteLine($"Meeting Type: {meeting.itemType}");
                    Console.WriteLine($"Meeting Start: {meeting.startTime}");
                    Console.WriteLine($"Meeting End: {meeting.endTime}");
                    Console.WriteLine();
                    k++;
                }
                i++;
            }
        }

    }
}
