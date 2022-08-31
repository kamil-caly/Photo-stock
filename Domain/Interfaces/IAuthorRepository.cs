using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthorRepository
    {
        IQueryable<Author> GetAll();
        Author GetById(int id);
    }
}
