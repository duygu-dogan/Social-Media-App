using System.Linq.Expressions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaApp.Domain.Constants;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Infrastructure.Data;
using SocialMediaApp.Infrastructure.Identity;

namespace Application.FunctionalTests;
[SetUpFixture]
public class Testing
{
    private static ITestDatabase? _database;
    private static CustomWebApplicationFactory? _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static string? _userId;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        _database = await TestDatabaseFactory.CreateAsync();
        _factory = new CustomWebApplicationFactory(_database.GetConnection());
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse> (IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }
    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        await mediator.Send(request);
    }
    public static string? GetUserId()
    {
        return _userId;
    }

    public static async Task<string> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
    }

    public static async Task<string> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { Roles.Administrator });
    }

    public static async Task<string> RunAsUserAsync(string username, string password, string[] roles)
    {
        using var scope = _scopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser { UserName = username, Email = username };

        var result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            _userId = user.Id;
            return _userId;
        }

        var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        throw new Exception($"Unable to create {username}.{Environment.NewLine}{errors}");
    }

    public static async Task<string> RunAsDomainUserAsync(string userName, string password, string[] roles)
    {
        var applicationUserId = await RunAsUserAsync(userName, password, roles);

        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        var user = new User
        {
            ApplicationUserId = Guid.Parse(applicationUserId),
            FullName = userName,
            UserName = userName
        };

        context!.DomainUsers.Add(user);

        await context.SaveChangesAsync();

        return user!.Id.ToString();
    }
    public static async Task ResetState()
    {
        if (_database == null)
        {
            throw new InvalidOperationException("Database is not initialized.");
        }
        await _database!.ResetAsync();
        _userId = null;
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task<TEntity?> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, Object>> include = null!)
            where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var query = context!.Set<TEntity>().AsQueryable();
        if (include != null)
            query = query.Include(include);

        return await query.FirstOrDefaultAsync(predicate);
    }


    public static async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Add(entity);
        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        await _database!.DisposeAsync();
        await _factory!.DisposeAsync();
    }
}
