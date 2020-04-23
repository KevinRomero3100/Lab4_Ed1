using System;
using System.Collections.Generic;
using Lab4_Ed1.Helpers;
using Lab4_Ed1.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab4_Ed1.Controllers
{
    public class WorkController : Controller
    {
        public ActionResult CreateWork()
        {
            return View();
        }
        public ActionResult ViewWork()
        {
            if (!Storage.Instance.HeepDeveloper.isEmpty() && Storage.Instance.viewNextWork.Count == 0)
            {
                var index = new IndexModel();
                var work = new Work();
                index = Storage.Instance.HeepDeveloper.Delete(index.CompareByPriority);
                work = Storage.Instance.Dictionary.GetT(index.Title, work.CompareByTitle);
                Storage.Instance.viewNextWork.Enqueue(work);
            }
            return RedirectToAction("Developer","User");
        }
        public ActionResult ViewProjecManajer()
        {
            while(!Storage.Instance.HeepDeveloper.isEmpty())
            {
                var index = new IndexModel();
                var work = new Work();
                index = Storage.Instance.HeepDeveloper.Delete(index.CompareByPriority);
                work = Storage.Instance.Dictionary.GetT(index.Title, work.CompareByTitle);
                Storage.Instance.viewNextWork.Enqueue(work);
            }
            return RedirectToAction("ProjectManager", "User");
        }
        public ActionResult Delete()
        {
            var delete = Storage.Instance.viewNextWork.Dequeue();
            if (!Storage.Instance.HeepDeveloper.isEmpty() && Storage.Instance.viewNextWork.Count == 0)
            {

                var index = new IndexModel();
                var work = new Work();
                index = Storage.Instance.HeepDeveloper.Delete(index.CompareByPriority);
                work = Storage.Instance.Dictionary.GetT(index.Title, work.CompareByTitle);
                Storage.Instance.viewNextWork.Enqueue(work);
            }
            return RedirectToAction("Developer", "User");
        }
        [HttpPost]
        public ActionResult CreateWork(FormCollection collection)
        {
            try
            {
                var key = collection["Title"];
                Work newWork = new Work()
                {
                    Title = collection["Title"],
                    TaskDescription = collection["TaskDescription"],
                    Project = collection["Project"],
                    DeliveryDate = collection["DeliveryDate"],
                    Priority = int.Parse(collection["Priority"]),
                    UserName = Storage.Instance.UserLogin.User,
                };
                IndexModel newIndex = new IndexModel()
                {
                    Priority = newWork.Priority,
                    Title = newWork.Title,
                };
                try
                {
                    if (Storage.Instance.Dictionary.IncertT(key, newWork, newWork.CompareByTitle))
                    {
                        ViewBag.mensage = "Agregado exitosamente";
                        FileModel.WriteFile(newWork);
                        Storage.Instance.HeepDeveloper.Insert(newIndex, newIndex.CompareByPriority);
                        return View();
                    }
                    else
                    {
                        ViewBag.mensage = "No se ha agregado la tarea por que el titulo ya existe";
                        return View();
                    }
                }
                catch
                {
                    throw;
                    //return View();
                }
            }
            catch (Exception)
            {
                ViewBag.mensage = "No ha selecionado la prioridad";
                return View();
            }
        }
            
    }
}
