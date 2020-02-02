using System;


namespace assign_1
{
    class SelectStuff
    {

        public int SelectItemType()
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

        public TaskType ChosenItemType(int t)
        {
            TaskType type = TaskType.Personal;

            if(t == 0)
                type = TaskType.Personal;
            else if(t == 1)
                type = TaskType.School;
            else if(t == 2)
                type = TaskType.Work;  

            return type;          
        }

        public int SelectPriority()
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

        public Priority ChosenPriority(int p)
        {
            Priority pLevel = Priority.Low;
            if(p == 0)
                pLevel = Priority.Low;
            else if(p == 1)
                pLevel = Priority.Medium;
            else if(p == 2)
                pLevel = Priority.High; 

            return pLevel;           
        }

        public int MeetingMin()
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

        public int MeetingHour()
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

    }

}