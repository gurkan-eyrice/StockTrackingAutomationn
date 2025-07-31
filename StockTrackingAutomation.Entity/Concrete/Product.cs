using System.ComponentModel.DataAnnotations;


namespace EntityLayer.Concrete
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(13)]
        public string BarcodeName { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public int UnitId { get; set; }
        public virtual Unit Unit {  get; set; }
        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        [Required]
        public int Amount { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
    }

}
