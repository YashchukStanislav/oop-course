
using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ValidationResult result = new ValidationResult();

        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
            object? value = property.GetValue(obj);

            RequiredAttribute? required =
                property.GetCustomAttribute<RequiredAttribute>();

            if (required != null)
            {
                if (value == null ||
                    (value is string text && string.IsNullOrWhiteSpace(text)))
                {
                    result.AddError(property.Name + ": " + required.ErrorMessage);
                }
            }

            MaxLengthAttribute? maxLength =
                property.GetCustomAttribute<MaxLengthAttribute>();

            if (maxLength != null && value is string stringValue)
            {
                if (stringValue.Length > maxLength.Length)
                {
                    result.AddError(property.Name + ": " + maxLength.ErrorMessage);
                }
            }

            MinValueAttribute? minValue =
                property.GetCustomAttribute<MinValueAttribute>();

            if (minValue != null && value != null)
            {
                double number = Convert.ToDouble(value);

                if (number < minValue.Min)
                {
                    result.AddError(property.Name + ": " + minValue.ErrorMessage);
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        Console.WriteLine(type.Name);

        PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            Console.Write("  " + properties[i].Name);

            object[] attributes = properties[i].GetCustomAttributes(false);

            for (int j = 0; j < attributes.Length; j++)
            {
                string attributeName = attributes[j].GetType().Name;

                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName.Substring(
                        0,
                        attributeName.Length - "Attribute".Length
                    );
                }

                Console.Write(" [" + attributeName + "]");
            }

            Console.WriteLine();
        }
    }
}