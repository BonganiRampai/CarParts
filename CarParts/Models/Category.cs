using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarParts.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please enter a category name.")]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        //Navigation property Located in Category.cs
        public List<CarPart> CarParts { get; set; }
        public List<Order> Orders { get; set; }
    }
}
