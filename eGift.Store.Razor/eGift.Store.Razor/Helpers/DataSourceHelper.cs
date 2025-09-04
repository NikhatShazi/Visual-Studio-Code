using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace eGift.Store.Razor.Helpers
{
    public static class DataSourceHelper
    {
        #region Parse Enum To Select List

        public static SelectList ParseEnumName<T>() where T : struct, IConvertible
        {
            var tagData = from T e in Enum.GetValues(typeof(T))
                          select new
                          {
                              ID = e.GetValue<T>(),
                              Name = e.ToString()
                          };
            return new SelectList(tagData, "ID", "Name");
        }

        public static SelectList ParseEnumDescription<T>() where T : struct, IConvertible
        {
            var tagData = from T e in Enum.GetValues(typeof(T))
                          select new
                          {
                              ID = e.GetValue<T>(),
                              Description = e.GetDescription<T>()
                          };
            return new SelectList(tagData, "ID", "Description");
        }

        public static SelectList ParseEnumDescriptionName<T>() where T : struct, IConvertible
        {
            var tagData = from T e in Enum.GetValues(typeof(T))
                          select new
                          {
                              Name = e.ToString(),
                              Description = e.GetDescription<T>()
                          };
            return new SelectList(tagData, "Name", "Description");
        }

        #endregion
    }
}
