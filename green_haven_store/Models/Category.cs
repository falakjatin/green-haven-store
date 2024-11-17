using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace green_haven_store.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cat_Id { get; set; }

        [Required(ErrorMessage = "Please enter a Category Name.")]
        [StringLength(2000)]
        [Display(Name = "Category Name")]
        public required string Cat_Name { get; set; }

        [Required(ErrorMessage = "Please select a Category Date.")]
        [Display(Name = "Category Date")]
        public required DateTime Cat_Date { get; set; }

        public bool Cat_IsActive { get; set; }

        public bool Cat_IsDelete { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
