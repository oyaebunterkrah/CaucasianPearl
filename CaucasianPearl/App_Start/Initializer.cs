using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Helpers;
using WebMatrix.WebData;

namespace CaucasianPearl.App_Start
{
    public class Initializer
    {
        /// <summary>
        /// Инициализирует подключение к базе данных.
        /// </summary>
        public static void Initialize()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection(
                    Consts.Connections.Default,
                    "Profile",
                    "ID",
                    "UserName", autoCreateTables: true
                );

            // добавляем админа, если его нет
            MembershipHelper.AddAdmin();

            // TODO: безопасность
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}