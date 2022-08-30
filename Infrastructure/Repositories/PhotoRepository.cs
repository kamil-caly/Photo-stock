using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly PhotoStockDbContext dbContext;

        public PhotoRepository(PhotoStockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Photo> GetAll()
        {
            var photos = dbContext
                .Photos
                .Include(p => p.Author)
                .ToList();

            return photos;
        }
    }
}
