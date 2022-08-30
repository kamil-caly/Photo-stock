using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author GetById(int id);
    }
}
