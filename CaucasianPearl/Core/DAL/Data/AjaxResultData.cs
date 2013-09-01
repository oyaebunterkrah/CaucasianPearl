using System;
using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.EntityHelpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.DAL.Data
{
    /// <summary>
    /// Объект, возращаемый, как результат выполнения ajax-запроса.
    /// </summary>
    public class AjaxResultData
    {
        public bool success { get; set; }
        public string errorMessage { get; set; }
    }

    /// <summary>
    /// Объект, возращаемый, как результат выполнения ajax-запроса.
    /// </summary>
    public class CaptchaAjaxResultData : AjaxResultData
    {
        public bool captchaIsValid { get; set; }
    }
}