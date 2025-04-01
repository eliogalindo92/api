namespace api.Interfaces;
using Models;

public interface IUsersRepository: IRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
}