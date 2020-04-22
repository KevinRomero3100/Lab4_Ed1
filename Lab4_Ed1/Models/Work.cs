using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4_Ed1.Models
{
    public class Work
    {
        public int MyProperty { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public string Project { get; set; }
        public int Priority { get; set; }
        public DateTime DeliveryDate { get; set; }

        public Work()
        {

        }
    }
}