using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaucasianPearl.Core.Helpers
{
    public static class DependencyResolverHelper<T>
    {
        public static T GetService()
        {
            return (T) DependencyResolver.Current.GetService(typeof (T));
        }
    }
}
