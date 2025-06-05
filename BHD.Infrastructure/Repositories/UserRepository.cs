using BHD.Domain.Entities;
using BHD.Domain.Repositories;
using BHD.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BHD.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public UserRepository(UserManager<User> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<User> CreateUserAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new Exception(string.Join("; ", result.Errors));
        return user;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task AddTokenAsync(UserToken token)
    {
        
        await _context.UserTokens.AddAsync(token);
    }

    public async Task<IEnumerable<User>> GetAllUserAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }
}
