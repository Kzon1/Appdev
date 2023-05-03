using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AppDev.Models
{
    public class Author
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; } = null!;
        [ValidateNever]
        public ICollection<Book>? Books { get; set; }
    }
}
