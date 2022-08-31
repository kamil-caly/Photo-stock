using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        void AddNewUser(User user);
        bool IsEmailTaken(string email);
        User GetExistingUser(string email);
    }
}
