using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace green_haven_store.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cart_Id { get; set; }

        [Required(ErrorMessage = "Please enter a Product.")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a User.")]
        [Display(Name = "User")]
        public int UserId { get; set; }

        public int? Cart_Quantity { get; set; }

        public Product? Product { get; set; }

        public User? User { get; set; }

        public bool? isOrderAdded { get; set; }

        public DateTime Cart_Date { get; set; }

        public DateTime Cart_Updated_At { get; set; }
    }
}
