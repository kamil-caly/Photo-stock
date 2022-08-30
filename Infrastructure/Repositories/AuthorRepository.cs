using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly PhotoStockDbContext dbContext;

        public AuthorRepository(PhotoStockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Author> GetAll()
        {
            var authors = dbContext
                .Authors
                .ToList();

            return authors;
        }
    }
}
