using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class UpdatePhotoDto
    {
        public string Name { get; set; }
        public string Link { get; set; }

        [Precision(10,2)]
        public decimal OriginalSize { get; set; }

        [Precision(5, 2)]
        public decimal Cost { get; set; }
        public int NumberOfSales { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }
    }
}
