using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TextRepository : ITextRepository
    {
        private readonly PhotoStockDbContext dbContext;

        public TextRepository(PhotoStockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Text> GetAll()
        {
            var texts = dbContext
                .Texts
                .Include(t => t.Author)
                .ToList();

            return texts;
        }
    }
}
