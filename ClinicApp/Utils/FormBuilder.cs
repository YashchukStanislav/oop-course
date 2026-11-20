
using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T obj = new T();

        PropertyInfo[] properties = typeof(T).GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];

            if (property.SetMethod == null || !property.SetMethod.IsPublic)
                continue;

            string hint = "";

            RequiredAttribute? required =
                property.GetCustomAttribute<RequiredAttribute>();

            MaxLengthAttribute? maxLength =
                property.GetCustomAttribute<MaxLengthAttribute>();

            MinValueAttribute? minValue =
                property.GetCustomAttribute<MinValueAttribute>();

            if (required != null)
                hint += "обов'язкове";

            if (maxLength != null)
            {
                if (hint.Length > 0)
                    hint += ", ";

                hint += "макс. " + maxLength.Length;
            }

            if (minValue != null)
            {
                if (hint.Length > 0)
                    hint += ", ";

                hint += "мін. " + minValue.Min;
            }

            if (hint.Length > 0)
                hint = " (" + hint + ")";

            while (true)
            {
                Console.Write(property.Name + hint + ": ");
                string input = Console.ReadLine() ?? "";

                try
                {
                    Type targetType = Nullable.GetUnderlyingType(property.PropertyType)
                        ?? property.PropertyType;

                    object value;

                    if (targetType == typeof(string))
                    {
                        value = input;
                    }
                    else if (targetType.IsEnum)
                    {
                        value = Enum.Parse(targetType, input, true);
                    }
                    else
                    {
                        value = Convert.ChangeType(input, targetType);
                    }

                    property.SetValue(obj, value);
                    break;
                }
                catch
                {
                    Console.WriteLine("Некоректне значення, спробуйте ще раз.");
                }
            }
        }

        return obj;
    }
}
