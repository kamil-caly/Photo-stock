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

        public int Create(int authorId, Text text)
        {
            text.AuthorId = authorId;

            dbContext.Texts.Add(text);
            dbContext.SaveChanges();

            return text.Id;
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
