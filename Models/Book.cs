using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using AppDev.Models;

namespace AppDev.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Title { get; set; } = null!;

        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; } = null!;
        public ICollection<Author> Authors {get; set;}

        [Range(0,10000000, ErrorMessage = "Invalid input")]
        public double Price { get; set; }

        public string StoreId { get; set; } = null!;

        [ValidateNever]
        public Store Store { get; set; } = null!;

        public int? ImageId { get; set; }

        public Image? Image { get; set; } = null!;
    }
}
