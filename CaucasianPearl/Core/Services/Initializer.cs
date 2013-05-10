using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Helpers;
using WebMatrix.WebData;

namespace CaucasianPearl.Core.Services
{
    public class Initializer
    {
        public static void Initialize()
        {
            WebSecurity.InitializeDatabaseConnection(
                Consts.DefaultConnectionName,
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true
            );

            MembershipHelper.AddAdmin();
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}