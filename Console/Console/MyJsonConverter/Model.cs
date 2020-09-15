using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleProject.MyJsonConverter
{
    [JsonConverter(typeof(MyConverter))]
    public class Model
    {
        public int ID { get; set; }
    }
}
