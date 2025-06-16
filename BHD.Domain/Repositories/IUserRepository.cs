using BHD.Domain.Entities;

namespace BHD.Domain.Repositories;

public interface IUserRepository
{
        Task<bool> EmailExistsAsync(string email);

        Task AddTokenAsync(UserToken token);
        Task SaveChangesAsync();
        Task<IEnumerable<User>> GetAllUserAsync();

}
