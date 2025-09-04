using System;

namespace eGift.Admin.MVC.Models
{
    public class ErrorViewModel
    {
        #region View Models
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        #endregion
    }
}
