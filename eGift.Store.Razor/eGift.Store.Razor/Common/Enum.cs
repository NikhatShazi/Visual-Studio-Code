using System.ComponentModel;

namespace eGift.Store.Razor.Common
{
    #region Project Specific
    public enum Status
    {
        [Description("New")]
        New = 1,

        [Description("Completed")]
        Completed = 2,

        [Description("Dispatched")]
        Dispatched = 3,

        [Description("Shipped")]
        Shipped = 4,

        [Description("Delievered")]
        Delievered = 5,

        [Description("Cancelled")]
        Cancelled = 6,
    }

    public enum RoleType
    {
        [Description("Super Admin")]
        SuperAdmin = 1,

        [Description("Admin")]
        Admin = 2,

        [Description("Employee")]
        Employee = 3,

        [Description("Customer")]
        Customer = 4,
    }

    public enum Gender
    {
        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2,

        [Description("Other")]
        Other = 3,
    }

    public enum Size
    {
        [Description("Extra Small")]
        XS = 1,

        [Description("Small")]
        S = 2,

        [Description("Medium")]
        M = 3,

        [Description("Large")]
        L = 4,

        [Description("Extra Large")]
        XL = 5,

        [Description("2X Large")]
        XXL = 6,

        [Description("3X Large")]
        XXXL = 7,

        [Description("4X Large")]
        XXXXL = 8,

        [Description("5X Large")]
        XXXXXL = 9,

        [Description("One Size")]
        OneSize = 10
    }
    #endregion
}
