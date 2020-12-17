using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Storage
{
    public class Json
    {
        /// <summary>
        /// Serialize the object to Json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JSONstring"></param>
        /// <returns></returns>
        public static T Deserialize<T>(String JSONstring) where T : new()
        {
            var json = JsonSerializer.Create().Deserialize<T>(new JsonTextReader(new StringReader(JSONstring))); ;
            if (json != null)
            {
                return json;
            }
            else
            {
                return new T();
            }
        }

        /// <summary>
        /// Deserialize the Json string to the mapping object
        /// </summary>
        /// <param name="JSONobject"></param>
        /// <returns></returns>
        public static string Serialize(object JSONobject)
        {
            return JsonConvert.SerializeObject(JSONobject);
        }
    }
}
