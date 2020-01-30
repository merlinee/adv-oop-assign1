using System;

namespace assign_1
{
    class Task : Item
    {

        public Priority taskPriority
        { get; set; }

        public Task()
        {
            
        }
        public Task(TaskType ty, string t, string d, DateTime nd, Priority p) : base(ty, t, d, nd) 
        { 
            taskPriority = p; 
        } 


    }
}