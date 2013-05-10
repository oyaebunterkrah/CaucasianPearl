using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;

namespace CaucasianPearl.Core.Filters.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PasswordLength : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentCulture;

            return base.FormatErrorMessage(name);
        }
    }
}