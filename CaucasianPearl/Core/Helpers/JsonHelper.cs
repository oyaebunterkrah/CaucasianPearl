﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using CaucasianPearl.Controllers;
using CaucasianPearl.Core.Services.Logging;
using Ninject;

namespace CaucasianPearl.Core.Helpers
{
    public static class JsonHelper
    {
        [Inject]
        private static ILogService LogService { get; set; }

        public static T Deserialize<T>(string json) where T: new()
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