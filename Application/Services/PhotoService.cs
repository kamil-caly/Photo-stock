using Application.Dto;
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
    }
}
