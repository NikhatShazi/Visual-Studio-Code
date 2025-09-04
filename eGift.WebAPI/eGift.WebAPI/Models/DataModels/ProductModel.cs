using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("Product", Schema = "dbo")]
    public class ProductModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("Name", TypeName = "varchar(200)")]
        public string Name { get; set; }

        [Column("CategoryId", TypeName = "int")]
        public int CategoryId { get; set; }

        [Column("SubCategoryId", TypeName = "int")]
        public int SubCategoryId { get; set; }

        [Column("QuantityPerUnit", TypeName = "int")]
        public Nullable<int> QuantityPerUnit { get; set; }

        [Column("UnitPrice", TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column("SizeID", TypeName = "int")]
        public Nullable<int> SizeID { get; set; }

        [Column("Discount", TypeName = "decimal(18,2)")]
        public Nullable<decimal> Discount { get; set; }

        [Column("UnitInStore", TypeName = "int")]
        public Nullable<int> UnitInStore { get; set; }

        [Column("UnitInOrder", TypeName = "int")]
        public Nullable<int> UnitInOrder { get; set; }

        [Column("ProductAvailable", TypeName = "int")]
        public Nullable<int> ProductAvailable { get; set; }

        [Column("ShortDescription", TypeName = "varchar(500)")]
        public string ShortDescription { get; set; }

        [Column("LongDescription", TypeName = "varchar(4000)")]
        public string LongDescription { get; set; }

        [Column("PicturePath1", TypeName = "varchar(500)")]
        public string PicturePath1 { get; set; }

        [Column("PicturePath2", TypeName = "varchar(500)")]
        public string PicturePath2 { get; set; }

        [Column("PicturePath3", TypeName = "varchar(500)")]
        public string PicturePath3 { get; set; }

        [Column("PicturePath4", TypeName = "varchar(500)")]
        public string PicturePath4 { get; set; }

        [Column("PictureData1", TypeName = "BLOB")]
        public byte[] PictureData1 { get; set; }

        [Column("PictureData2", TypeName = "BLOB")]
        public byte[] PictureData2 { get; set; }

        [Column("PictureData3", TypeName = "BLOB")]
        public byte[] PictureData3 { get; set; }

        [Column("PictureData4", TypeName = "BLOB")]
        public byte[] PictureData4 { get; set; }

        [Column("ProductImagePath", TypeName = "varchar(500)")]
        public string ProductImagePath { get; set; }

        [Column("ProductImageData", TypeName = "BLOB")]
        public byte[] ProductImageData { get; set; }
        #endregion

        #region View Models
        [NotMapped]
        public IFormFile ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Picture1 { get; set; }

        [NotMapped]
        public IFormFile Picture2 { get; set; }

        [NotMapped]
        public IFormFile Picture3 { get; set; }

        [NotMapped]
        public IFormFile Picture4 { get; set; }
       
        [NotMapped]
        public string CategoryName { get; set; }

        [NotMapped]
        public string SubCategoryName { get; set; }

        [NotMapped]
        public string SizeName { get; set; }
        #endregion
    }
}
