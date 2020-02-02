using System;

namespace assign_1
{
    class UpdateStuff
    {

        public Task UpdateTask(Task editTask)
        {
            SelectStuff select = new SelectStuff();
            int exit = 0;
            int s = 999;
            while(exit != 9999)
            {
                Console.WriteLine($"1. Title: {editTask.itemTitle}");
                Console.WriteLine($"2. Details: {editTask.itemDetails}");
                Console.WriteLine($"3. Type: {editTask.itemType}");
                Console.WriteLine($"4. Priority: {editTask.taskPriority}");
                Console.WriteLine("Please enter the number of the item you would like to edit, or 6 to quit.");
                string choiceEntry = Console.ReadLine();

                if(Int32.TryParse(choiceEntry, out int c))
                    s = c;
                else
                    s = 9999;
                switch(s)
                {
                    case 1:
                        Console.WriteLine("Enter new title.");
                        editTask.itemTitle = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter new details.");
                        editTask.itemDetails = Console.ReadLine();
                        break;
                    case 3:
                        int j = 3;
                        while(j == 3)
                            j = select.SelectItemType();
                        editTask.itemType = select.ChosenItemType(j);
                            break;
                    case 4:
                        int k = 3;
                        while(k == 3)
                            k = select.SelectPriority();
                        editTask.taskPriority = select.ChosenPriority(k);
                        break;
                    case 6:
                        exit = 9999;
                        break;
                    default:
                        break;
                }
            }
            return editTask;
        }

        public Meeting UpdateMeeting(Meeting editMeeting)
        {
            SelectStuff select = new SelectStuff();
            int exit = 0;
            int s = 999;
            while(exit != 9999)
            {
                Console.WriteLine($"1. Title: {editMeeting.itemTitle}");
                Console.WriteLine($"2. Details: {editMeeting.itemDetails}");
                Console.WriteLine($"3. Type: {editMeeting.itemType}");
                Console.WriteLine($"4. Start Time: {editMeeting.startTime}");
                Console.WriteLine($"5. End Time: {editMeeting.endTime}");
                Console.WriteLine("Please enter the number of the item you would like to edit, or 6 to quit.");
                string choiceEntry = Console.ReadLine();

                if(Int32.TryParse(choiceEntry, out int c))
                    s = c;
                else
                    s = 9999;
                switch(s)
                {
                    case 1:
                        Console.WriteLine("Enter new title.");
                        editMeeting.itemTitle = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter new details.");
                        editMeeting.itemDetails = Console.ReadLine();
                        break;
                    case 3:
                        int j = 3;
                        while(j == 3)
                            j = select.SelectItemType();
                        editMeeting.itemType = select.ChosenItemType(j);
                        break;
                    case 4:
                        Console.WriteLine("Enter new start time.");
                        DateTime current = editMeeting.itemDate;
                        DateTime start = editMeeting.startTime;
                        do 
                        {
                            start = new DateTime(current.Year, current.Month, current.Day, select.MeetingHour(), select.MeetingMin(), 0);
                            if(start > editMeeting.endTime)
                                Console.WriteLine("Time is invalid.");
                        } while(start > editMeeting.endTime);
                        editMeeting.startTime = start;
                        break;
                    case 5:
                        Console.WriteLine("Enter new end time.");
                        current = editMeeting.itemDate;
                        DateTime end = editMeeting.endTime;
                        do 
                        {
                            end = new DateTime(current.Year, current.Month, current.Day, select.MeetingHour(), select.MeetingMin(), 0);
                            if(end < editMeeting.startTime)
                                Console.WriteLine("Time is invalid.");
                        } while(end < editMeeting.startTime);  
                        editMeeting.endTime = end;                  
                        break;
                    case 6:
                        exit = 9999;
                        break;
                    default:
                        break;
                }
            }
            return editMeeting;
        }

    }
}