using Application.Dto;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        PageResult<AuthorDto> GetAll(ItemQuery query);
    }
}
