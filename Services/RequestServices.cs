using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using social_app_backend.AppDataContext;
using social_app_backend.DTOs;
using social_app_backend.Models;
using social_app_backend.Utils;

public class RequestServices
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<RequestServices> _logger;
    public RequestServices(AppDbContext dbContext, ILogger<RequestServices> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<Request> CreateRequestAsync(CreateRequestDTO inputs, int loggedInUserId)
    {
        Request request = new()
        {
            SenderId = loggedInUserId,
            ReceiverId = inputs.ReceiverId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        //If request already exists between participants
        Request? existingRequest = await GetRequestAsync(x =>
            (x.SenderId == request.SenderId && x.ReceiverId == request.ReceiverId)
            ||
            (x.SenderId == request.ReceiverId && x.ReceiverId == request.SenderId)
        );
        if (existingRequest != null)
        {
            throw new ServiceException("Request already exists!", 409);
        }
        _dbContext.Requests.Add(request);
        await _dbContext.SaveChangesAsync();
        return request;
    }
    public async Task<Request?> GetRequestAsync(Expression<Func<Request, bool>> where, ServiceOptions? options = null)
    {
        Request? request = await _dbContext.Requests.Where(where).FirstOrDefaultAsync();
        if (request == null)
        {
            _logger.LogError("Request Not Found!");
            if (options?.ThrowErrorIfNotExists == true)
            {
                throw new ServiceException("Request Not Found!", 404);
            }
        }
        return request;
    }
    public async Task<List<Request>> GetAllRequestsAsync(Expression<Func<Request, bool>>? where = null, GetOptions? options = null)
    {
        options = options ?? new GetOptions();
        (int page, int? limit) = PaginationHelper.PreparePaginationData(options.Page, options.Limit);
        IQueryable<Request> query = _dbContext.Requests.Where(where ?? (x => true));
        if (options.Limit.HasValue)
        {
            query = query.Skip((options.Page - 1) * options.Limit.Value).Take(options.Limit.Value);
        }
        //apply sorting
        if (options.SortBy != null)
        {
            switch (options.SortOrder.ToLower() + "_" + options.SortBy.ToLower())
            {
                case "desc_status":
                    query = query.OrderByDescending(x => x.Status);
                    break;
                case "asc_status":
                    query = query.OrderBy(x => x.Status);
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
        List<Request> requests = await query.ToListAsync();
        return requests;
    }
}
