using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        #region Constructors
        public ProductViewModel()
        {
            CategoryList = new SelectList("");
            SubCategoryList = new SelectList("");
            SizeList = DataSourceHelper.ParseEnumName<Size>();
        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "This field is required.")]
        public int CategoryId { get; set; }

        [Display(Name = "Sub Category")]
        [Required(ErrorMessage = "This field is required.")]
        public int SubCategoryId { get; set; }

        [Display(Name = "Quantity Per Unit")]
        public Nullable<int> QuantityPerUnit { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Size")]
        public Nullable<int> SizeID { get; set; }

        [Display(Name = "Discount")]
        public Nullable<decimal> Discount { get; set; }

        [Display(Name = "Unit In Store")]
        public Nullable<int> UnitInStore { get; set; }

        [Display(Name = "Unit In Order")]
        public Nullable<int> UnitInOrder { get; set; }

        [Display(Name = "Product Available")]
        public Nullable<int> ProductAvailable { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Display(Name = "Picture 1")]
        public string PicturePath1 { get; set; }

        [Display(Name = "Picture 2")]
        public string PicturePath2 { get; set; }

        [Display(Name = "Picture 3")]
        public string PicturePath3 { get; set; }

        [Display(Name = "Picture 4")]
        public string PicturePath4 { get; set; }

        [Display(Name = "Picture Data1")]       
        public byte[] PictureData1 { get; set; }

        [Display(Name = "Picture Data 2")]
        public byte[] PictureData2 { get; set; }

        [Display(Name = "Picture Data 3")]
        public byte[] PictureData3 { get; set; }

        [Display(Name = "Picture Data 4")]
        public byte[] PictureData4 { get; set; }

        [Display(Name = "Product Image")]
        //[Required(ErrorMessage = "This field is required.")]
        public string ProductImagePath { get; set; }

        [Display(Name = "Product Image Data")]
        //[Required(ErrorMessage = "This field is required.")]
        public byte[] ProductImageData { get; set; }
        #endregion

        #region View Models
        [Display(Name = "Image Url")]
        public IFormFile ImageUrl { get; set; }

        [Display(Name = "Picture 1")]
        public IFormFile Picture1 { get; set; }

        [Display(Name = "Picture 2")]
        public IFormFile Picture2 { get; set; }

        [Display(Name = "Picture 3")]
        public IFormFile Picture3 { get; set; }

        [Display(Name = "Picture 4")]
        public IFormFile Picture4 { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Size Name")]
        public string SizeName { get; set; }
        #endregion

        #region Dropdowns
        public SelectList CategoryList { get; set; }
        public SelectList SubCategoryList { get; set; }
        public SelectList SizeList { get; set; }    
        #endregion
    }
}
