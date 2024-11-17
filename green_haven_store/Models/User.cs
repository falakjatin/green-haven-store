using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace green_haven_store.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int U_Id { get; set; }

        [Required(ErrorMessage = "Please enter a User Name.")]
        [StringLength(2000)]
        [Display(Name = "User Name")]
        public required string U_Name { get; set; }

        [Required(ErrorMessage = "Please enter a User Email.")]
        [StringLength(2000)]
        [Display(Name = "User Email")]
        public required string U_Email { get; set; }

        [Required(ErrorMessage = "Please enter a User Address.")]
        [StringLength(2000)]
        [Display(Name = "User Address")]
        public required string U_Address { get; set; }

        [Required(ErrorMessage = "Please enter a User Password.")]
        [StringLength(2000)]
        [Display(Name = "User Password")]
        public required string U_Password { get; set; }

        [Required(ErrorMessage = "Please enter a Address Pincode.")]
        [StringLength(2000)]
        [Display(Name = "Address Pincode")]
        public required string U_Pincode { get; set; }

        public string? U_Type { get; set; }

        public DateTime U_Date { get; set; }

        public bool U_IsActive { get; set; }

        public bool U_IsDelete { get; set; }

        public ICollection<Cart>? Carts { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
