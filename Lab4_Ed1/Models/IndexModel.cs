using System;
using System.Collections.Generic;
using Lab4_Ed1.Helpers;
using System.Linq;
using System.Web;

namespace Lab4_Ed1.Models
{
    public class IndexModel
    {
        public int Priority { get; set; }
        public string Title { get; set; }

        public delegate int Compare(IndexModel value1, IndexModel value2);
        public Compare CompareByPriority = CompareTo;

        public static int CompareTo(IndexModel value1, IndexModel value2)
        {
            return value2.Priority.CompareTo(value1.Priority);
        }
        public void saveIndice()
        {
            Storage.Instance.HeepDeveloper.Insert(this, CompareByPriority);
        }
    }
}