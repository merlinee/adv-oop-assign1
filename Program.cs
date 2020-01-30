using System;

namespace assign_1
{
        enum TaskType 
        {
            Personal,
            School,
            Work
        }

        enum Priority 
        {
            Low,
            Medium,
            High
        }

    class Program
    {
        static void Main(string[] args)
        {
            MainStuff stuff = new MainStuff();
            stuff.RunCalendar();
        }
    }
}
