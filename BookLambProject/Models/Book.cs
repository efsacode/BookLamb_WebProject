using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookLambProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }

        public string ImagePath { get; set; }

        public string Author { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        public ICollection<ShoppingCart>? ShoppingCart { get; set; }
    }
}
