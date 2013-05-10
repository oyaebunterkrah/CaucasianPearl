using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;

namespace CaucasianPearl.Core.Filters.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PasswordsCompare : ValidationAttribute
    {
        private const string DoNotMatchErrorMessage = "'{0}' and '{1}' do not match.";

        public string OtherProperty { get; private set; }

        public PasswordsCompare(string otherProperty)
            : base(DoNotMatchErrorMessage)
        {
            if (otherProperty == null)
                throw new ArgumentNullException("otherProperty");

            OtherProperty = otherProperty;
        }
        
        public override string FormatErrorMessage(string name)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentCulture;

            return base.FormatErrorMessage(name);
        }
    }
}