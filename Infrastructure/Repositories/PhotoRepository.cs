using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly PhotoStockDbContext dbContext;

        public PhotoRepository(PhotoStockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Photo> GetAll()
        {
            var photos = dbContext
                .Photos
                .Include(p => p.Author);

            return photos;
        }

        public Photo GetById(int id)
        {
            var photo = dbContext
                .Photos
                .Include(p => p.Author)
                .FirstOrDefault(p => p.Id == id);

            return photo;
        }

        public void Update(Photo photo)
        {
            dbContext.Photos.Update(photo);
            dbContext.SaveChanges();
        }
    }
}
