using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4_Ed1.Models
{
    public class UserModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Job { get; set; }
        public JobSelect JobGarder { get; set; }

    }
    public enum JobSelect
    {
        Developer,
        Project_Manajer,
    }
}