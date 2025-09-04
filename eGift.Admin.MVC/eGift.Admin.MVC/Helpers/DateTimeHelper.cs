using System;

namespace eGift.Admin.MVC.Helpers
{
    public static class DateTimeHelper
    {
        #region Date Format

        public static string GetDateString(DateTime inputDate)
        {
            return inputDate.ToString("yyyy-MM-dd");
        }

        public static string GetDateString(DateTime? inputDate)
        {
            return inputDate?.ToString("yyyy-MM-dd"); ;
        }

        public static string GetShortDateString(DateTime? inputDate)
        {
            return inputDate?.ToString("dd MMM yyyy"); ;
        }

        #endregion

        #region Date and Time formate

        public static string GetDateTimeString(DateTime inputDate)
        {
            return inputDate.ToString("yyyy-MM-dd hh:mm tt");
        }

        #endregion

        #region Date Format Extension Methods

        public static string GetDate(this DateTime inputDate)
        {
            return inputDate.ToString("yyyy-MM-dd");
        }

        public static string GetDate(this DateTime? inputDate)
        {
            return inputDate?.ToString("yyyy-MM-dd"); ;
        }

        public static string GetShortDate(this DateTime? inputDate)
        {
            return inputDate?.ToString("dd MMM yyyy"); ;
        }

        #endregion

        #region Date Format Extension Methods For PDF
        public static string GetPDFDate(this DateTime? inputDate)
        {
            return inputDate?.ToString("dd-MM-yyyy"); ;
        }

        public static string GetPDFDateTime(this DateTime? inputDate)
        {
            return inputDate?.ToString("dd-MM-yyyy hh:mm tt"); ;
        }

        #endregion
    }
}
