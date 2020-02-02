using System;
using System.Collections.Generic; 

namespace assign_1
{
    class Day
    {

        public DateTime myDate
        { get; set; }
        public List<Task> myTasks
        { get; set; }
        public List<Meeting> myMeetings
        { get; set; }

        public Day()
        {

        }
        public Day (DateTime newDate) 
        {
            myDate = newDate;
            myTasks = new List<Task>();
            myMeetings = new List<Meeting>();
        }

        public void AddTask(Task newTask)
        {
            if(newTask.taskPriority == Priority.Low)
                myTasks.Add(newTask);
            else if(newTask.taskPriority == Priority.High)
                myTasks.Insert(0, newTask);
            else
                AddMediumTask(newTask);
        }
        public void AddLowTask(Task newTask)
        {
            myTasks.Add(newTask);
        }
        public void RemoveTask(int i)
        {
            myTasks.RemoveAt(i);
        }

        public void AddHighTask(Task newTask)
        {
            myTasks.Insert(0, newTask);
        }

        public void AddMediumTask(Task newTask)
        {
            if(myTasks.Count > 0)
            {
                int idx = myTasks.FindLastIndex(FindHighTask);
                if(idx > -1)
                {
                    myTasks.Insert(idx+1, newTask);
                }
                else 
                    myTasks.Insert(0,newTask);
            }
            else
                myTasks.Add(newTask);
        }

        private static bool FindHighTask(Task tk)
        {
            if (tk.taskPriority == Priority.High)
                return true;
            else
                return false;
        }

        public void RemoveMeeting(int i)
        {
            myMeetings.RemoveAt(i);
        }

        public void AddMeeting(Meeting newMeeting)
        {
            if(myMeetings.Count > 0)
            {
                if(myMeetings[0].startTime >= newMeeting.startTime)
                    myMeetings.Insert(0,newMeeting);
                else if(myMeetings[myMeetings.Count-1].startTime <= newMeeting.startTime)
                    myMeetings.Add(newMeeting);
                else
                {
                    int i = 1;
                    Boolean cont = true;
                    while(i < myMeetings.Count-1 && cont)
                    {
                        if(myMeetings[i].startTime >= newMeeting.startTime)
                        {
                            myMeetings.Insert(i,newMeeting);
                            cont = false;
                        } 
                        else if(myMeetings.Count-2 == i)
                        {
                            myMeetings.Insert(i+1,newMeeting);
                            cont = false;
                        } 
                        else
                            i++;
                    }
                } 
            }
            else
                myMeetings.Add(newMeeting);
        }

        //new one is 12
        //existing is 5,6,9,11,13
        //eliminated 5 and 13
        //6,9,11
        //
        //but if new one is 8
        //


    }
}
