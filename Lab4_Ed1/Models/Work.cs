using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4_Ed1.Models
{
    public class Work
    {
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public string Project { get; set; }
        public int Priority { get; set; }
        public string DeliveryDate { get; set; }
        public string UserName { get; set; }
   
        public Work()
        {

        }

        public delegate int Compare(string key, Work student1);
        public Compare CompareByTitle = CompareTo;

        public static int CompareTo(string key, Work work)
        {
            return key.CompareTo(work.Title.ToString());
        }
    }
   
}