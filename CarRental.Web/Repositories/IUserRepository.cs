using Microsoft.AspNetCore.Identity;

namespace CarRental.Web.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<IdentityUser>> GetAll();

    Task<bool> Add(IdentityUser identityUser, string password, List<string> roles);

    Task Delete(Guid userid);
}