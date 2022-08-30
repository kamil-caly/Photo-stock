using Application.Dto;

namespace Application.Interfaces
{
    public interface IPhotoService
    {
        IEnumerable<PhotoDto> GetAll();
        PhotoDto GetById(int id);
        void UpdatePhoto(UpdatePhotoDto photo, int id);

        double calculateAverage(IEnumerable<PhotoDto> dtos);
    }
}
