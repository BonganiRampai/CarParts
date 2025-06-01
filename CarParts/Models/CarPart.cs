using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarParts.Models
{
    public class CarPart
    {
        // EF will instruct the database to automatically generate this value
        public int CarPartID { get; set; }

        [Required(ErrorMessage = "Please enter a Car Part code.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter a Car Part name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Car Part price.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public decimal DiscountPercent => .20M;  // A discount of 20% is hard-coded for all products

        public decimal DiscountAmount => Price * DiscountPercent;

        public decimal DiscountPrice => Price - DiscountAmount;

        public string Slug
        {
            get
            {
                if (Name == null)
                    return "";
                else
                    return Name.Replace(' ', '-');
            }
        }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryID { get; set; }  // foreign key property

        //Navigation properties Located in CarPart.cs
        public Category Category { get; set; }
    }
}
