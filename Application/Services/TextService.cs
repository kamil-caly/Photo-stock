using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LINQtoCSV;
using System.Linq.Expressions;

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

        public PageResult<TextDto> GetAll(ItemQuery query)
        {
            IQueryable<Text> texts = textRepository.GetAll();

            var baseQuery = texts
                .Where(t => query.searchPhrase == null ||
                    (t.Name.ToLower().Contains(query.searchPhrase.ToLower()) ||
                    t.Author.FirstName.ToLower().Contains(query.searchPhrase.ToLower()) ||
                    t.TextDesc.ToLower().Contains(query.searchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Text, object>>>()
                {
                    {nameof(Text.Name), t => t.Name },
                    {nameof(Text.Cost), t => t.Cost },
                    {nameof(Text.NumberOfSales), t => t.NumberOfSales },
                    {nameof(Text.Rating), t => t.Rating }
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var filtredTexts = baseQuery
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();

            var textDtos = mapper.Map<List<TextDto>>(filtredTexts);

            var result = new PageResult<TextDto>(textDtos, baseQuery.Count(), query.pageSize, query.pageNumber);

            return result;
        }

        public void WriteToCsvFile()
        {
            var texts = textRepository.GetAll().ToList();

            var textDtos = mapper.Map<List<TextDto>>(texts);
            
            try
            {
                var csvFileDesc = new CsvFileDescription()
                {
                    FirstLineHasColumnNames = true,
                    IgnoreUnknownColumns = true,
                    SeparatorChar = ';',
                };

                var csvContext = new CsvContext(); 
                csvContext.Write(textDtos, "textEntity.csv", csvFileDesc);
            }
            catch (Exception csvEx)
            {
                throw new Exception(csvEx.Message);
            }
        }
    }
}
