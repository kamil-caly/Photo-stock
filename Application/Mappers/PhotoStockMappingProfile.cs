using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class PhotoStockMappingProfile
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>();
            }).CreateMapper();
    }
}
