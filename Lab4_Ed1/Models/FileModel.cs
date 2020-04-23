using System;
using System.Collections.Generic;
using Lab4_Ed1.Helpers;
using Lab4_Ed1.Models;
using System.IO;
using System.Linq;
using System.Web;

namespace Lab4_Ed1.Models
{
    public class FileModel
    {
        public static void CreateFile()
        {
            var path = Storage.Instance.route;
            try
            {
                using (FileStream disck = File.Create(path))
                {
                }
                var line = "Priority, Title, TaskDescription, work.Project, DeliveryDate, UserName"; ;
                using (StreamWriter discWriter = File.AppendText(path))
                {
                    discWriter.WriteLine(line);
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void ReadFiles()
        {
            string route = Storage.Instance.route;
            string line = "";

            using (FileStream fileStream = new FileStream(route, FileMode.Open))
            {
                using (StreamReader file = new StreamReader(fileStream))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        var values = line.Split(',');
                        if (values[values.Length-1]==Storage.Instance.UserLogin.User)
                        {
                            var indice = new IndexModel
                            {
                                Priority = int.Parse(values[0]),
                                Title = values[1].TrimStart('"').TrimEnd('"')
                            };
                            indice.saveIndice();
                        }

                    }
                }
            }
        }
        public static void ReadFiles(string user)
        {
            string route = Storage.Instance.route;
            string line = "";

            using (FileStream fileStream = new FileStream(route, FileMode.Open))
            {
                using (StreamReader file = new StreamReader(fileStream))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        var values = line.Split(',');
                        var indice = new IndexModel
                        {
                            Priority = int.Parse(values[0]),
                            Title = values[1].TrimStart('"').TrimEnd('"')
                        };
                        indice.saveIndice();
                    }
                }
            }
        }
        public static void WriteFile(Work work)
        {
            try
            {
                var line = "";
                line = work.Priority + "," + work.Title + "," + work.TaskDescription + "," + work.Project + "," + work.DeliveryDate + "," + Storage.Instance.UserLogin.User;
                using (StreamWriter discWriter = File.AppendText(Storage.Instance.route))
                {
                    discWriter.WriteLine(line);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}