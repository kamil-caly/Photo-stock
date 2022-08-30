using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class TextService : ITextService
    {
        private readonly ITextRepository textRepository;
        private readonly IMapper mapper;
        private readonly IAuthorRepository authorRepository;

        public TextService(ITextRepository textRepository, IMapper mapper,
            IAuthorRepository authorRepository)
        {
            this.textRepository = textRepository;
            this.mapper = mapper;
            this.authorRepository = authorRepository;
        }

        public int Create(CreateTextDto dto, int authorId)
        {
            var author = authorRepository.GetById(authorId);

            if (author is null)
            {
                throw new NotFoundException("author not found");
            }

            var textEntity = mapper.Map<Text>(dto);

            return textRepository.Create(authorId, textEntity);
        }

        public IEnumerable<TextDto> GetAll()
        {
            var texts = textRepository.GetAll();

            return mapper.Map<IEnumerable<TextDto>>(texts);
        }
    }
}
