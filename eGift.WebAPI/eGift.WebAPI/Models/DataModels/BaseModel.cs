using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    public class BaseModel
    {
        #region Data Models
        [Column("IsDeleted", TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Column("CreatedBy", TypeName = "int")]
        public int CreatedBy { get; set; }

        [Column("CreatedDate", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Column("UpdatedBy", TypeName = "int")]
        public Nullable<int> UpdatedBy { get; set; }

        [Column("UpdatedDate", TypeName = "datetime")]
        public Nullable<DateTime> UpdatedDate { get; set; }
        #endregion
    }
}
