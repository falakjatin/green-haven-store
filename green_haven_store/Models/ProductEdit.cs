using System.ComponentModel.DataAnnotations;

namespace green_haven_store.Models
{
    public class ProductEdit
    {
        [Required(ErrorMessage = "Product is not found")]
        [Display(Name = "Product")]
        public required int P_Id { get; set; }

        [Required(ErrorMessage = "Please select a Category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required(ErrorMessage = "Please enter a Product Name.")]
        [StringLength(2000)]
        [Display(Name = "Product Name")]
        public required string P_Name { get; set; }

        [Required(ErrorMessage = "Please enter a Product Price.")]
        [StringLength(2000)]
        [Display(Name = "Product Price")]
        public required string P_Price { get; set; }

        [Required(ErrorMessage = "Please enter a Product Image Uri")]
        [Display(Name = "Picture Url")]
        public required string P_PictureUri { get; set; }

        [Required(ErrorMessage = "Please enter a Product Description.")]
        [Display(Name = "Product Description")]
        public required string P_Description { get; set; }

        [Display(Name = "Picture")]
        public byte[]? P_Picture { get; set; }

        public DateTime P_Date { get; set; }

        public bool P_IsActive { get; set; }

        public bool P_IsDelete { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public ICollection<Cart>? Carts { get; set; }
    }
}
