using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public decimal OriginalSize { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string NameOfAuthor { get; set; }
        public string NicknameOfAuthor { get; set; }
        public decimal Cost { get; set; }
        public int NumberOfSales { get; set; }
        public int Rating { get; set; }
    }
}
