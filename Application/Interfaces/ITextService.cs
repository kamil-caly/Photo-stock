using Application.Dto;

namespace Application.Interfaces
{
    public interface ITextService
    {
        IEnumerable<TextDto> GetAll();
    }
}
