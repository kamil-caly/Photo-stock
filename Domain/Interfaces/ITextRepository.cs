using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITextRepository
    {
        IQueryable<Text> GetAll();
        int Create(int authorId, Text text);
    }
}
