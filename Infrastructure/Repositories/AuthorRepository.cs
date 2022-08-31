using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly PhotoStockDbContext dbContext;

        public AuthorRepository(PhotoStockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IQueryable<Author> GetAll()
        {
            var authors = dbContext
                .Authors
                .Include(a => a.Texts)
                .Include(a => a.Photos);

            return authors;
        }

        public Author GetById(int id)
        {
            var author = dbContext
                .Authors
                .Include(a => a.Texts)
                .Include(a => a.Photos)
                .FirstOrDefault(a => a.Id == id);

            return author;
        }
    }
}
