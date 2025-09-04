using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{

    [Table("Order", Schema = "dbo")]
    public class OrderModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("CustomerId", TypeName = "int")]
        public int CustomerId { get; set; }

        [Column("TotalAmount", TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column("TotalDiscount", TypeName = "decimal(18,2)")]
        public Nullable<decimal> TotalDiscount { get; set; }

        [Column("TotalTax", TypeName = "decimal(18,2)")]
        public Nullable<decimal> TotalTax { get; set; }

        [Column("OrderNumber", TypeName = "varchar(200)")]
        public string OrderNumber { get; set; }

        [Column("Notes", TypeName = "varchar(500)")]
        public string Notes { get; set; }

        [Column("DispatchDate", TypeName = "datetime")]
        public Nullable<DateTime> DispatchDate { get; set; }

        [Column("ShippedDate", TypeName = "datetime")]
        public Nullable<DateTime> ShippedDate { get; set; }

        [Column("DeliveryDate", TypeName = "datetime")]
        public Nullable<DateTime> DeliveryDate { get; set; }

        [Column("CancelDate", TypeName = "datetime")]
        public Nullable<DateTime> CancelDate { get; set; }

        [Column("StatusId", TypeName = "int")]
        public int StatusId { get; set; }
        #endregion

        #region View Models
        [NotMapped]
        public string StatusName { get; set; }
        #endregion
    }
}
