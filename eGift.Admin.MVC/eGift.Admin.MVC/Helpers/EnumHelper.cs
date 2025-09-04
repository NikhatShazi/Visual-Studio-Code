using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System;

namespace eGift.Admin.MVC.Helpers
{
    public static class EnumHelper
    {
        #region Get Enum Description Static Method

        public static string GetEnumDescription(Enum @enum)
        {
            if (@enum == null)
                return null;

            string description = @enum.ToString();

            try
            {
                FieldInfo fi = @enum.GetType().GetField(@enum.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    description = attributes[0].Description;
            }
            catch
            {
            }

            return description;
        }

        #endregion

        #region Get Enum Description Extension Method

        public static string GetDescription<T>(this T @enum) where T : struct, IConvertible
        {
            string description = @enum.ToString();

            try
            {
                FieldInfo fi = @enum.GetType().GetField(@enum.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    description = attributes[0].Description;
            }
            catch
            {
            }

            return description;
        }

        #endregion

        #region Get Enum Value Extension Method

        public static object GetValue<T>(this T @enum) where T : struct, IConvertible
        {
            return @enum.ToInt32(CultureInfo.InvariantCulture.NumberFormat);
        }

        #endregion
    }
}
