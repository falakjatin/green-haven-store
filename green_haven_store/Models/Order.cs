using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace green_haven_store.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_Id { get; set; }

        public int? ProductIds { get; set; }

        public int? CartIds { get; set; }

        [Required(ErrorMessage = "Please enter a User.")]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a Tax.")]
        [Range(0, 999999999999, ErrorMessage = "Please enter a Tax which no less than 0")]
        [Display(Name = "Tax")]
        public required double Order_Tax { get; set; }

        [Required(ErrorMessage = "Please enter a Amount.")]
        [Range(0, 999999999999, ErrorMessage = "Please enter a Amount which no less than 0")]
        [Display(Name = "Amount")]
        public required double Order_PayableAmount { get; set; }

        public int? Order_Quantity { get; set; }

        public bool Order_IsPaymentDone { get; set; }

        public DateTime Order_Date { get; set; }
    }
}
