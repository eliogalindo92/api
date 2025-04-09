using api.Models;

namespace api.Interfaces;

public interface IRolesRepository: IRepository<Role>
{
    Task<Role?> FindByDenominationAsync(string denomination);
    Task<List<Role>> FindAllByIdAsync(List<int> roleIds);
    Task<Role?> FindByIdWithPermissionsAsync(int id);
}