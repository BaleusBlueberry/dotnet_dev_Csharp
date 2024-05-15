using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfClansHelper
{
    static class Converters
    {
        public static IDictionary<int, Dictionary<string, string>> ConvertJsonToDictionary(string jsonData)
        //takes in a json and convers it into a Dictionary<string, string> dynamically
        // in order to find each entery of the dictionary with a level
        {
            var jArray = JArray.Parse(jsonData);
            var dictionary = new Dictionary<int, Dictionary<string, string>>();

            foreach (var jToken in jArray)
            {
                var jObject = (JObject)jToken;
                var level = jObject["Level"].Value<int>();
                var properties = jObject.Properties()
                    .ToDictionary(p => p.Name, p => p.Value.ToString());
                dictionary[level] = properties;
            }

            return dictionary;
        }
    }
}
