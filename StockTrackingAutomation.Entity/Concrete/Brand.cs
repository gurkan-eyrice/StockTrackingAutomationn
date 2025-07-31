using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public required string BrandName { get; set; }
        [StringLength(100)]
        public required string Description { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}