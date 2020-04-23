using System;
using System.Collections.Generic;
using CustumGenerics.Structures;
using Lab4_Ed1.Models;
using System.Linq;
using System.Web;

namespace Lab4_Ed1.Helpers
{
    public class Storage
    {
        private static Storage _instance = null;

        public static Storage Instance
        {
            get
            {
                if (_instance == null) _instance = new Storage();
                return _instance;
            }
        }
        public HashTable<Work> Dictionary = new HashTable<Work>();
        public Heep<IndexModel> HeepDeveloper = new Heep<IndexModel>();
        public List<UserModel> Users = new List<UserModel>();
        public Queue<Work> viewNextWork = new Queue<Work>();
        public UserModel UserLogin = new UserModel();
        public string route = null;
    }
}