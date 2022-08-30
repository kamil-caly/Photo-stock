using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITextRepository
    {
        IEnumerable<Text> GetAll();
        int Create(int authorId, Text text);
    }
}
