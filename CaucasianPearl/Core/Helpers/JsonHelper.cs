using System;
using System.Web.Script.Serialization;
using CaucasianPearl.Core.Services.LoggingService;
using Newtonsoft.Json;

namespace CaucasianPearl.Core.Helpers
{
    public static class JsonHelper
    {
        private static ILogService LogService
        {
            get { return ServiceHelper<ILogService>.GetService(); }
        }

        public static dynamic Encode(object jsonObj)
        {
            return System.Web.Helpers.Json.Encode(jsonObj);
        }

        public static dynamic Decode(string json)
        {
            return System.Web.Helpers.Json.Decode(json);
        }

        public static string Serialize(object jsonObj)
        {
            return JsonConvert.SerializeObject(jsonObj, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        public static T Deserialize<T>(string json) where T : new()
        {
            var deserializedObj = new T();

            try
            {
                var javaScriptSerializer = new JavaScriptSerializer();
                deserializedObj = javaScriptSerializer.Deserialize<T>(json);
            }
            catch (Exception exception)
            {
                LogService.Error(exception);

                return deserializedObj;
            }
            
            return deserializedObj;
        }
    }
}