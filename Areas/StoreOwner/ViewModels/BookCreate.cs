using System.ComponentModel.DataAnnotations;
using AppDev.Models;

namespace AppDev.Areas.StoreOwner.ViewModels
{
    public class BookCreate
    {
        [StringLength(255)]
        public string Title { get; set; } = null!;

        public int CategoryId { get; set; }
        public int[] Authors {get; set;}


        [Range(0, double.MaxValue, ErrorMessage = "Invalid Input")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string StoreId { get; set; } = null!;

        public IFormFile UploadImage { get; set; } = null!;
    }
}
