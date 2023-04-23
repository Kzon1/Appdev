using System.ComponentModel.DataAnnotations;

namespace AppDev.Models
{
    public class Store
    {
        public string Id { get; set; } = null!;
        public ApplicationUser StoreOwner { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
