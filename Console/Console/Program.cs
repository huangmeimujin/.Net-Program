using ConsoleProject.MyJsonConverter;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            {
                //var model = new Model();
                //model.ID = 1;
                //var json = JsonConvert.SerializeObject(model);//由于ID值为1，得到json为{"ID":ture}

                //Console.WriteLine(json);
                //model.ID = 2;
                //json = JsonConvert.SerializeObject(model);//由于ID值不为1，得到json为{"ID":false}
                //Console.WriteLine(json);
                //Console.ReadKey();
            }
            {
                var model = new Model();
                model.ID = 1;
                var json = JsonConvert.SerializeObject(model);//由于ID值为1，得到json为{"ID":ture}
                var newModel = JsonConvert.DeserializeObject<Model>(json);//序列化得到的newModel对象ID值为1
                Console.WriteLine(newModel.ID);
                Console.ReadKey();
            }
            
        }
    }
}
