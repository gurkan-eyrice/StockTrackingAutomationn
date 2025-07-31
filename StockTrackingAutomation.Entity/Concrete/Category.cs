using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public required string CategoryName { get; set; }
        [StringLength(100)]
        public required string Description { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}