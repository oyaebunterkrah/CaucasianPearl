using System;
using System.ComponentModel;
using System.Reflection;

namespace CaucasianPearl.Core.Filters
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private PropertyInfo _nameProperty;
        private Type _resourceType;

        public LocalizedDisplayNameAttribute(string displayNameKey)
            : base(displayNameKey)
        {
        }

        public Type NameResourceType
        {
            get { return _resourceType; }
            set
            {
                _resourceType = value;
                // инициализация nameProperty, когда тип свойства устанавливается в set
                _nameProperty = _resourceType.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public);
            }
        }

        public override string DisplayName
        {
            get
            {
                // проверяет является ли nameProperty null и возвращает исходные значения отображаемого имени
                if (_nameProperty == null)
                    return base.DisplayName;

                return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null);
            }
        }
    }
}