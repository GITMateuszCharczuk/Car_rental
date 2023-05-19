using CarRental.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _authDbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public UserRepository(AuthDbContext authDbContext, UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _authDbContext = authDbContext;
    }

    public async Task<IEnumerable<IdentityUser>> GetAll()
    {
        var users = await _authDbContext.Users.ToListAsync();
        var superAdminUser = await _authDbContext.Users
            .FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");
        if (superAdminUser != null) users.Remove(superAdminUser);

        return users;
    }

    public async Task<bool> Add(IdentityUser identityUser, string password, List<string> roles)
    {
        var identityResult = await _userManager.CreateAsync(identityUser, password);

        if (identityResult.Succeeded) identityResult = await _userManager.AddToRolesAsync(identityUser, roles);

        if (identityResult.Succeeded) return true;

        return false;
    }

    public async Task Delete(Guid userid)
    {
        var user = await _userManager.FindByIdAsync(userid.ToString());

        if (user != null) await _userManager.DeleteAsync(user);
    }
}