using Models.Entities;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IUser
    {
        Task<(TbUser? User, string? ErrorMessage)> AuthenticateUser(string email, string password);
        Task<(TbUser? User, string? ErrorMessage)> GetUserById(string userId);
        Task<(int Result, string? ErrorMessage)> UpdateUser(TbUser user);
    }
}
