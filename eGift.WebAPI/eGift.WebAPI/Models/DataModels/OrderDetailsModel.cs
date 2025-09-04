using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("OrderDetails", Schema = "dbo")]
    public class OrderDetailsModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("OrderId", TypeName = "int")]
        public int OrderId { get; set; }

        [Column("ProductId", TypeName = "int")]
        public int ProductId { get; set; }

        [Column("UnitPrice", TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column("Quantity", TypeName = "int")]
        public int Quantity { get; set; }

        [Column("Discount", TypeName = "decimal(18,2)")]
        public Nullable<decimal> Discount { get; set; }

        [Column("Tax", TypeName = "decimal(18,2)")]
        public Nullable<decimal> Tax { get; set; }

        [Column("NetAmount", TypeName = "decimal(18,2)")]
        public decimal NetAmount { get; set; }
        #endregion
    }
}
