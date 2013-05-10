using System;
using System.Web;

namespace CaucasianPearl.Core.HttpModules
{
    public class ServerHeaderModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose() { }

        private static void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            if (HttpContext.Current == null)
                throw new NullReferenceException("HttpContext.Current");

            HttpContext.Current.Response.Headers.Set("Server", "Apache 2000 Server");
        }
    }
}