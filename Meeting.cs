using System;

namespace assign_1
{
    class Meeting : Item
    {

        public DateTime startTime
        { get; set; }
        public DateTime endTime
        { get; set; }

        public Meeting()
        {}

        public Meeting(TaskType ty, string t, string d, DateTime nd, DateTime st, DateTime et) : base(ty, t, d, nd) 
        { 
            startTime = st; 
            endTime = et;
        } 

    }
}