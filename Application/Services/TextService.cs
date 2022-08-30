using Application.Dto;
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

        public TextService(ITextRepository textRepository, IMapper mapper)
        {
            this.textRepository = textRepository;
            this.mapper = mapper;
        }

        public IEnumerable<TextDto> GetAll()
        {
            var texts = textRepository.GetAll();

            return mapper.Map<IEnumerable<TextDto>>(texts);
        }
    }
}
