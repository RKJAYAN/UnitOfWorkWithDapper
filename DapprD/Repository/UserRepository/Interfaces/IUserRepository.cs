using DapprD.Models;

namespace DapprD.Repository.UserRepository.Interfaces
{
    public interface IUserRepository
    {
        bool Debit(string userId, double amount);

        bool Credit(string userId, double amount);

        User GetUserDetails(string userId);

    }
}
