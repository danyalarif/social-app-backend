using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using social_app_backend.AppDataContext;
using social_app_backend.DTOs;
using social_app_backend.Models;
using social_app_backend.Utils;

namespace social_app_backend.Services;

public class UserServices
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<UserServices> _logger;
    public UserServices(AppDbContext dbContext, ILogger<UserServices> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<User> CreateUserAsync(CreateUserDTO _user)
    {
        //map to user
        User user = new()
        {
            Email = _user.Email,
            Password = _user.Password,
            FirstName = _user.FirstName,
            LastName = _user.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    public async Task<User> GetUserAsync(Expression<Func<User, bool>> where)
    {
        User? user = await _dbContext.Users.Where(where).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new ServiceException("User Not Found!", 404);
        }
        return user;
    }

}
