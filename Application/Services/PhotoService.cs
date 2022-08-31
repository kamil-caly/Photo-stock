using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq.Expressions;

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

        public double calculateAverage()
        {
            var photos = photoRepository.GetAll();

            var result = photos.Select(p => p.Rating).Average();

            return Math.Round(result, 2);
        }

        public PageResult<PhotoDto> GetAll(ItemQuery query)
        {
            IQueryable<Photo> photos = photoRepository.GetAll();

            var baseQuery = photos
                .Where(p => query.searchPhrase == null ||
                    (p.Name.ToLower().Contains(query.searchPhrase.ToLower()) ||
                    p.Author.FirstName.ToLower().Contains(query.searchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Photo, object>>>()
                {
                    {nameof(Photo.Name), p => p.Name },
                    {nameof(Photo.Cost), p => p.Cost },
                    {nameof(Photo.Rating), p => p.Rating },
                    {nameof(Photo.NumberOfSales), p => p.NumberOfSales }
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var filtredPhotos = baseQuery
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();

            var photoDtos = mapper.Map<List<PhotoDto>>(filtredPhotos);

            var result = new PageResult<PhotoDto>(photoDtos, baseQuery.Count(), query.pageSize, query.pageNumber);

            return result;
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
