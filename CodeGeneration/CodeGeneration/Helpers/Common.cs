using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CodeGeneration.Helpers
{
    public class Common
    {
    }
}


namespace System
{
    public static class Extensions
    {
        public static int ToInt(this string s)
        {
            int result = 0;
            Int32.TryParse(s, out result);
            return result;
        }

        public static void FillStringNulls<T>(this T model) where T : class
        {
            var projectProperties = model.GetType().GetProperties();
            foreach (var property in projectProperties)
            {
                if (property.GetValue(model, null) == null)
                {
                    if (property.PropertyType == typeof(String))
                    {
                        property.SetValue(model, "", null);
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        property.SetValue(model, 0, null);
                    }
                }
            }
        }


    }


    public static class JSONNetSerialization
    {
        public static String SerializeJsonNet<T>(this T toSerialize)
        {
            var jss = new JsonSerializerSettings()
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ContractResolver = new XMLIgnoreContractResolver(),
            };
            return JsonConvert.SerializeObject(toSerialize, Formatting.None, jss);
        }

        public static T DeserializeJsonNet<T>(this String json)
        {
            if (Object.ReferenceEquals(json, null))
                return default(T);

            var jss = new JsonSerializerSettings()
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ContractResolver = new XMLIgnoreContractResolver()
            };
            return JsonConvert.DeserializeObject<T>(json, jss);
        }

    }

    public class XMLIgnoreContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
        {

            var prop = base.CreateProperty(member, memberSerialization);

            if (member.GetCustomAttributes(typeof(XmlIgnoreAttribute), false).Count() > 0)
                prop.Ignored = true;

            return prop;
        }
    }


}