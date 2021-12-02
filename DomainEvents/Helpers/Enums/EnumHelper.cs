using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Helpers.Enums
{
    public static class EnumHelper
    {
        public static int GetEnumValue(string enumValue,Type enumType)
        {
            int value = (int)Enum.Parse(@enumType, enumValue);
            return value;
        }
    }
}
