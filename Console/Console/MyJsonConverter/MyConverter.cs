using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleProject.MyJsonConverter
{
    public class MyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //return typeof(Model) ==objectType;
            return typeof(Model).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var model = new Model();
            //获取JObject对象，该对象对应着我们要反序列化的json
            var jobj = serializer.Deserialize<JObject>(reader);
            //从JObject对象中获取键位ID的值
            var id = jobj.Value<bool>("ID");
            //根据id值判断，进行赋值操作
            if (id)
            {
                model.ID = 1;
            }
            else
            {
                model.ID = 0;
            }
            //最终返回的model对象就是json反序列化所得到的Model对象
            //主要，这里的model对象不一定非得是Model类型，ReadJson()方法与WriteJson()方法是一样的，可以自由操作反序列生成的对象或者序列化生成的json
            return model;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);

            #region 自定义序列化Json对象
            ////new一个JObject对象,JObject可以像操作对象来操作json
            //var jobj = new JObject();
            ////value参数实际上是你要序列化的Model对象，所以此处直接强转
            //var model = value as Model;
            //if (model.ID != 1)
            //{
            //    //如果ID值为1，添加一个键位"ID"，值为false
            //    jobj.Add("ID1", false);
            //}
            //else
            //{
            //    jobj.Add("ID1", true);
            //}
            ////通过ToString()方法把JObject对象转换成json
            //var jsonstr = jobj.ToString();
            ////调用该方法，把json放进去，最终序列化Model对象的json就是jsonstr，由此，我们就能自定义的序列化对象了
            //writer.WriteValue(jsonstr); 
            #endregion
        }
    }
}
