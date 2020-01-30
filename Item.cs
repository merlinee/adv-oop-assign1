using System;

namespace assign_1
{
    class Item
    {

        public TaskType itemType
        { get; set; }
        public string itemTitle
        { get; set; }
        public string itemDetails
        { get; set; }
        public DateTime itemDate
        { get; set; }

        public Item()
        {
            
        }
        public Item(TaskType type, string title, string details, DateTime newDate) 
        {
            itemType = type;
            itemTitle = title;
            itemDetails = details;
            itemDate = newDate;
        }
    }
}
