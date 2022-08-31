using Application.Dto;

namespace Application.Interfaces
{
    public interface IPhotoService
    {
        PageResult<PhotoDto> GetAll(ItemQuery query);
        PhotoDto GetById(int id);
        void UpdatePhoto(UpdatePhotoDto photo, int id);

        double calculateAverage();
    }
}
