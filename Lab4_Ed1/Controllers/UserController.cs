using System;
using System.Collections.Generic;
using Lab4_Ed1.Helpers;
using Lab4_Ed1.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab4_Ed1.Controllers
{
    public class UserController : Controller
    {
        public ActionResult CreateUser()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Developer()
        {
            return View(Storage.Instance.viewNextWork);
        }
        public ActionResult ProjectManager()
        {
            return View(Storage.Instance.viewNextWork);
        }

        public ActionResult logOut()
        {
            var index = new IndexModel();
            Storage.Instance.UserLogin = null;
            Storage.Instance.viewNextWork.Clear();
            while (!Storage.Instance.HeepDeveloper.isEmpty())
            {
                Storage.Instance.HeepDeveloper.Delete(index.CompareByPriority);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var User = collection["User"];
            var search = Storage.Instance.Users.Where(x => x.User == User).ToList();

            if (search.Count != 0)
            {
                foreach (var item in search)
                {
                    if (collection["User"] == item.User && collection["Password"] == item.Password)
                    {
                        if (item.Job == "Developer")
                        {
                            Storage.Instance.UserLogin = item;
                            if (Storage.Instance.route==null)
                            {
                                Storage.Instance.route = Server.MapPath("~/Helpers/Disck.txt");
                                FileModel.CreateFile();
                            }
                            else
                            {
                                FileModel.ReadFiles();
                            }
                            return RedirectToAction("CreateWork", "Work");
                        }
                    }
                    else
                    {
                        Storage.Instance.UserLogin = item;
                        if (Storage.Instance.route == null)
                        {
                            Storage.Instance.route = Server.MapPath("~/Helpers/Disck.txt");
                            FileModel.CreateFile();
                        }
                        else
                        {
                            FileModel.ReadFiles("ProjectManager");
                        }
                        Storage.Instance.UserLogin = item;
                        return View("ProjectManager");
                    }
                }      
                ViewBag.UserDontSerched = "Usuario y contraseña no coinsiden";
                return View();
            }
            else
            {
                ViewBag.UserDontSerched = "Usuario no existente";
                return View();
            }
        }
        [HttpPost]
        public ActionResult CreateUser(FormCollection collection)
        {
            UserModel newUser = new UserModel
            {
                User = collection["User"],
                Password = collection["Password"],
                Job = collection["Job"],
             };
            Storage.Instance.Users.Add(newUser);
            return View("Login");

        }
    }
}
