using Application.Dto;

namespace Application.Interfaces
{
    public interface IPhotoService
    {
        IEnumerable<PhotoDto> GetAll();
        PhotoDto GetById(int id);
    }
}
