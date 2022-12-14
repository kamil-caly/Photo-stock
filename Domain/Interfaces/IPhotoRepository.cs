using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPhotoRepository
    {
        IQueryable<Photo> GetAll();
        Photo GetById(int id);
        void Update(Photo photo);
    }
}
