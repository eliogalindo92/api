using api.Models;

namespace api.Interfaces;

public interface IPermissionsRepository: IRepository<Permission>
{
    Task<List<Permission>> FindAllByIdAsync(List<int> permissionIds);

}