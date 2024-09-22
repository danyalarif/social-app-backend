using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public async Task<User?> GetUserAsync(Expression<Func<User, bool>> where, ServiceOptions? options = null)
    {
        User? user = await _dbContext.Users.Where(where).FirstOrDefaultAsync();
        if (user == null)
        {
            _logger.LogError("User Not Found!");
            if (options?.ThrowErrorIfNotExists == true)
            {
                throw new ServiceException("User Not Found!", 404);
            }
        }
        return user;
    }
    public async Task<List<User>> GetAllUsersAsync(Expression<Func<User, bool>>? where = null, GetOptions? options = null)
    {
        options = options ?? new GetOptions();
        (int page, int? limit) = PaginationHelper.PreparePaginationData(options.Page, options.Limit);
        IQueryable<User> query = _dbContext.Users.Where(where ?? (x => true));
        if (options.Limit.HasValue)
        {
            query = query.Skip((options.Page - 1) * options.Limit.Value).Take(options.Limit.Value);
        }
        //apply sorting
        if (options.SortBy != null)
        {
            switch (options.SortOrder.ToLower() + "_" + options.SortBy.ToLower())
            {
                case "desc_role":
                    query = query.OrderByDescending(x => x.Role);
                    break;
                case "asc_role":
                    query = query.OrderBy(x => x.Role);
                    break;

                case "desc_email":
                    query = query.OrderByDescending(x => x.Email);
                    break;
                case "asc_email":
                    query = query.OrderBy(x => x.Email);
                    break;

                case "desc_firstname":
                    query = query.OrderByDescending(x => x.FirstName);
                    break;
                case "asc_firstname":
                    query = query.OrderBy(x => x.FirstName);
                    break;

                case "desc_lastname":
                    query = query.OrderByDescending(x => x.LastName);
                    break;
                case "asc_lastname":
                    query = query.OrderBy(x => x.LastName);
                    break;

                case "desc_createdat":
                    query = query.OrderByDescending(x => x.CreatedAt);
                    break;
                case "asc_createdat":
                    query = query.OrderBy(x => x.CreatedAt);
                    break;

                case "desc_updatedat":
                    query = query.OrderByDescending(x => x.UpdatedAt);
                    break;
                case "asc_updatedat":
                    query = query.OrderBy(x => x.UpdatedAt);
                    break;

                default:
                    break;
            }
        }
        List<User> users = await query.ToListAsync();
        return users;
    }
}
