﻿using System;
using System.ComponentModel;
using System.Reflection;

namespace Qmos.Entities.Enums
{
    public static class EnumExtentions
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }

    public enum TypeActions : byte
    {
        Insert,
        Update,
        Delete
    }

    
}
