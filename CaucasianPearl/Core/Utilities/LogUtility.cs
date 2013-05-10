using System;
using System.Web;

namespace CaucasianPearl.Core.Utilities
{
    public class LogUtility
    {

        public static string BuildExceptionMessage(Exception exception)
        {
            var httpContext = HttpContext.Current;

            if (httpContext == null)
                throw new NullReferenceException("httpContext");

            var logException = exception;

            if (exception.InnerException != null)
                logException = exception.InnerException;

            var errorMessage = string.Format("{0}{1}{2}",
                                             Environment.NewLine,
                                             "Error in Path: ",
                                             httpContext.Request.Path);

            // get the QueryString along with the Virtual Path
            errorMessage += string.Format("{0}{1}{2}",
                                          Environment.NewLine,
                                          "Raw Url: ",
                                          httpContext.Request.RawUrl);

            // get the error message
            errorMessage += string.Format("{0}{1}{2}",
                                          Environment.NewLine,
                                          "Message: ",
                                          logException.Message);

            // source of the message
            errorMessage += string.Format("{0}{1}{2}",
                                          Environment.NewLine,
                                          "Source: ",
                                          logException.Source);

            // stack Trace of the error
            errorMessage += string.Format("{0}{1}{2}",
                                          Environment.NewLine,
                                          "Stack Trace: ",
                                          logException.StackTrace);
            // method where the error occurred
            errorMessage += string.Format("{0}{1}{2}",
                                          Environment.NewLine,
                                          "TargetSite: ",
                                          logException.TargetSite);

            return errorMessage;
        }
    }
}