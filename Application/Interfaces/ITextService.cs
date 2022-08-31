using Application.Dto;

namespace Application.Interfaces
{
    public interface ITextService
    {
        PageResult<TextDto> GetAll(ItemQuery query);
        int Create(CreateTextDto dto, int authorId);
        void WriteToCsvFile();
    }
}
