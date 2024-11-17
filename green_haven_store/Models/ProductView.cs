using System.ComponentModel.DataAnnotations;

namespace green_haven_store.Models
{
    public class ProductView
    {

        [Required(ErrorMessage = "Please select a Category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a Product Name.")]
        [StringLength(2000)]
        [Display(Name = "Product Name")]
        public required string P_Name { get; set; }

        [Required(ErrorMessage = "Please enter a Product Price.")]
        [StringLength(2000)]
        [Display(Name = "Product Price")]
        public required string P_Price { get; set; }

        [Required(ErrorMessage = "Please select a Image.")]
        [Display(Name = "Picture")]
        public required IFormFile P_Picture { get; set; }
    }
}
