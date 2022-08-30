using Application.Dto;

namespace Application.Interfaces
{
    public interface IPhotoService
    {
        public IEnumerable<PhotoDto> GetAll();
    }
}
