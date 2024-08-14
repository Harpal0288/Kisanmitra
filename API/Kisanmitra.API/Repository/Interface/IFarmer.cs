using Models.Entities;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IFarmer
    {
        Task<(int Result, string? ErrorMessage)> CreateUserAndFarmer(TbUser user, TbFarmer farmer);
        Task<(TbUser? User, string? ErrorMessage)> GetUserById(string userId);
        Task<(TbFarmer? Farmer, string? ErrorMessage)> GetFarmerById(string farmerId);
        Task<(int Result, string? ErrorMessage)> UpdateUser(TbUser user);
        Task<(int Result, string? ErrorMessage)> UpdateFarmer(TbFarmer farmer);
    }
}
