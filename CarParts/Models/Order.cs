using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarParts.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Enter the name of the car part")]
        [DisplayName("Car Part Name")]
        public string CarPartName { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "Please select the category")]
        public int CategoryID { get; set; }

        //Navigation property
        public Category Category { get; set; }
    }
}
