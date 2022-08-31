using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IMapper mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            this.authorRepository = authorRepository;
            this.mapper = mapper;
        }
        public PageResult<AuthorDto> GetAll(ItemQuery query)
        {
            IQueryable<Author> authors = authorRepository.GetAll();

            var baseQuery = authors
                .Where(a => query.searchPhrase == null ||
                    (a.FirstName.ToLower().Contains(query.searchPhrase.ToLower()) ||
                    a.LastName.ToLower().Contains(query.searchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Author, object>>>()
                {
                    {nameof(Author.FirstName), a => a.FirstName },
                    {nameof(Author.LastName), a => a.LastName },
                    {nameof(Author.DateOfBirth), a => a.DateOfBirth }
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var filtredAuthors = baseQuery
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();

            var authorsDto =  mapper.Map<List<AuthorDto>>(filtredAuthors);

            var result = new PageResult<AuthorDto>(authorsDto, baseQuery.Count(), query.pageSize, query.pageNumber);

            return result;
        }

    }
}
