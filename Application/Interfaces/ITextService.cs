using Application.Dto;

namespace Application.Interfaces
{
    public interface ITextService
    {
        IEnumerable<TextDto> GetAll();
        int Create(CreateTextDto dto, int authorId);
    }
}
