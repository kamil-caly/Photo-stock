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
                // Map for Author
                cfg.CreateMap<Author, AuthorDto>();

                // Map for Photo
                cfg.CreateMap<Photo, PhotoDto>()
                .ForMember(p => p.NameOfAuthor, c => c.MapFrom(s => s.Author.FirstName))
                .ForMember(p => p.NicknameOfAuthor, c => c.MapFrom(s => s.Author.NickName));

                // Map for Text
                cfg.CreateMap<Text, TextDto>()
                .ForMember(t => t.NameOfAuthor, c => c.MapFrom(s => s.Author.FirstName))
                .ForMember(t => t.NicknameOfAuthor, c => c.MapFrom(s => s.Author.NickName));

            }).CreateMapper();
    }
}
