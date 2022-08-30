using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public decimal OriginalSize { get; set; }
        public DateTime DateOfCreation { get; set; }
        public virtual Author Author { get; set; }
        public int AuthorId { get; set; }
        public decimal Cost { get; set; }
        public int NumberOfSales { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }
    }
}
