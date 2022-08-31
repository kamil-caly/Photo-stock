using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PhotoStockDbContext dbContext;

        public UserRepository(PhotoStockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddNewUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public User GetExistingUser(string email)
        {
            return dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool IsEmailTaken(string email)
        {
            return dbContext.Users.Any(u => u.Email == email);
        }
    }
}
