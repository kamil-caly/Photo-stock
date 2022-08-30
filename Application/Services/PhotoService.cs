using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository photoRepository;
        private readonly IMapper mapper;

        public PhotoService(IPhotoRepository photoRepository, IMapper mapper)
        {
            this.photoRepository = photoRepository;
            this.mapper = mapper;
        }

        public IEnumerable<PhotoDto> GetAll()
        {
            var photos = photoRepository.GetAll();

            return mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public PhotoDto GetById(int id)
        {
            var photo = photoRepository.GetById(id);

            if (photo is null)
            {
                throw new NotFoundException("photo not found");
            }

            return mapper.Map<PhotoDto>(photo);
        }

        public void UpdatePhoto(UpdatePhotoDto photo, int id)
        {
            var existingPhoto = photoRepository.GetById(id);

            if (photo is null)
            {
                throw new NotFoundException("photo not found");
            }

            var updatedPhoto = mapper.Map(photo, existingPhoto);
            photoRepository.Update(updatedPhoto);
        }
    }
}
