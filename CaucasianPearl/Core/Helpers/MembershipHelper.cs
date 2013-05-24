using System.Web.Security;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Services.Logging;
using Ninject;
using WebMatrix.WebData;

namespace CaucasianPearl.Core.Helpers
{
    public class MembershipHelper
    {
        [Inject]
        private static ILogService LogService { get; set; }

        public static void AddAdmin()
        {
            // если нет в системе роли admin, создаём её
            if (!Roles.RoleExists(Consts.Roles.Admin))
                Roles.CreateRole(Consts.Roles.Admin);

            // если нет в системе пользователя admin, создаём его
            if (!WebSecurity.UserExists(Consts.AdminLoginName))
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(
                        Consts.AdminLoginName,
                        Consts.AdminPassword
                    );
                }
                catch (MembershipCreateUserException exeption)
                {
                    LogService.Error(exeption);
                }
            }

            // если у пользователя Admin нет роли Admin, присваиваем ему эту роль
            if (!Roles.IsUserInRole(Consts.AdminLoginName, Consts.Roles.Admin))
                Roles.AddUserToRole(Consts.AdminLoginName, Consts.Roles.Admin);
        }
    }
}