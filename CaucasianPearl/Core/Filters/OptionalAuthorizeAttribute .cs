using System.Web;
using System.Web.Mvc;

namespace CaucasianPearl.Core.Filters
{
    public class OptionalAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool _authorize;

        public OptionalAuthorizeAttribute()
        {
            _authorize = true;
        }

        public OptionalAuthorizeAttribute(bool authorize)
        {
            _authorize = authorize;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!_authorize)
                return true;

            return base.AuthorizeCore(httpContext);
        }
    }
}