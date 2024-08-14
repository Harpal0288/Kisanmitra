using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Data;

namespace Kisanmitra.API.Repository.Implementations
{
    public class FarmerRepo : IFarmer
    {
        private readonly ApplicationDbContext _context;
        public FarmerRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(int Result, string? ErrorMessage)> CreateUserAndFarmer(TbUser user, TbFarmer farmer)
        {
            var errorMessageParam = new SqlParameter("@error_message", SqlDbType.NVarChar, 1000)
            {
                Direction = ParameterDirection.Output
            };

            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_SingleInsertUserAndFarmer @user_name, @aadhar_number, @user_email, @phone_number, @user_address, @user_password, @role_id, @user_inserted_by, @user_updated_by, @farm_size, @farm_location, @pin_code, @irrigation_method, @soil_type, @farming_experience, @membership_status, @membership_expiry, @language_preference, @farmer_inserted_by, @farmer_updated_by, @error_message OUTPUT",
                new SqlParameter("@user_name", user.UserName ?? (object)DBNull.Value),
                new SqlParameter("@aadhar_number", user.AadharNumber ?? (object)DBNull.Value),
                new SqlParameter("@user_email", user.Email ?? (object)DBNull.Value),
                new SqlParameter("@phone_number", user.PhoneNumber ?? (object)DBNull.Value),
                new SqlParameter("@user_address", user.Address ?? (object)DBNull.Value),
                new SqlParameter("@user_password", user.Password ?? (object)DBNull.Value),
                new SqlParameter("@role_id", user.RoleId ?? (object)DBNull.Value),
                new SqlParameter("@user_inserted_by", user.InsertedBy ?? (object)DBNull.Value),
                new SqlParameter("@user_updated_by", user.UpdatedBy ?? (object)DBNull.Value),
                new SqlParameter("@farm_size", farmer.FarmSize ?? (object)DBNull.Value),
                new SqlParameter("@farm_location", farmer.FarmLocation ?? (object)DBNull.Value),
                new SqlParameter("@pin_code", farmer.PinCode ?? (object)DBNull.Value),
                new SqlParameter("@irrigation_method", farmer.IrrigationMethod ?? (object)DBNull.Value),
                new SqlParameter("@soil_type", farmer.SoilType ?? (object)DBNull.Value),
                new SqlParameter("@farming_experience", farmer.FarmingExperience ?? (object)DBNull.Value),
                new SqlParameter("@membership_status", farmer.MembershipStatus ?? (object)DBNull.Value),
                new SqlParameter("@membership_expiry", farmer.MembershipExpiry ?? (object)DBNull.Value),
                new SqlParameter("@language_preference", farmer.LanguagePreference ?? (object)DBNull.Value),
                new SqlParameter("@farmer_inserted_by", farmer.InsertedBy ?? (object)DBNull.Value),
                new SqlParameter("@farmer_updated_by", farmer.UpdatedBy ?? (object)DBNull.Value),
                errorMessageParam
            );

            string? errorMessage = errorMessageParam.Value != DBNull.Value ? errorMessageParam.Value.ToString() : null;
            return (result, errorMessage);
        }

        public async Task<(TbUser? User, string? ErrorMessage)> GetUserById(string userId)
        {
            var errorMessageParam = new SqlParameter("@error", SqlDbType.NVarChar, 1000)
            {
                Direction = ParameterDirection.Output
            };

            var users = await _context.TbUsers
                .FromSqlRaw("EXEC sp_GetUserById @user_id, @error OUTPUT",
                    new SqlParameter("@user_id", userId),
                    errorMessageParam)
                .ToListAsync();

            var user = users.FirstOrDefault();
            string? errorMessage = errorMessageParam.Value != DBNull.Value ? errorMessageParam.Value.ToString() : null;
            return (user, errorMessage);
        }

        public async Task<(TbFarmer? Farmer, string? ErrorMessage)> GetFarmerById(string farmerId)
        {
            var errorMessageParam = new SqlParameter("@error", SqlDbType.NVarChar, 1000)
            {
                Direction = ParameterDirection.Output
            };

            var farmers = await _context.TbFarmers
                .FromSqlRaw("EXEC sp_GetFarmerById @farmer_id, @error OUTPUT",
                    new SqlParameter("@farmer_id", farmerId),
                    errorMessageParam)
                .ToListAsync();

            var farmer = farmers.FirstOrDefault();
            string? errorMessage = errorMessageParam.Value != DBNull.Value ? errorMessageParam.Value.ToString() : null;
            return (farmer, errorMessage);
        }

        public async Task<(int Result, string? ErrorMessage)> UpdateUser(TbUser user)
        {
            var errorMessageParam = new SqlParameter("@error_message", SqlDbType.NVarChar, 1000)
            {
                Direction = ParameterDirection.Output
            };

            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_SingleUpdateUser @user_id, @user_name, @aadhar_number, @user_email, @phone_number, @user_address, @user_password, @role_id, @user_updated_by, @error_message OUTPUT",
                new SqlParameter("@user_id", user.UserId ?? (object)DBNull.Value),
                new SqlParameter("@user_name", user.UserName ?? (object)DBNull.Value),
                new SqlParameter("@aadhar_number", user.AadharNumber ?? (object)DBNull.Value),
                new SqlParameter("@user_email", user.Email ?? (object)DBNull.Value),
                new SqlParameter("@phone_number", user.PhoneNumber ?? (object)DBNull.Value),
                new SqlParameter("@user_address", user.Address ?? (object)DBNull.Value),
                new SqlParameter("@user_password", user.Password ?? (object)DBNull.Value),
                new SqlParameter("@role_id", user.RoleId ?? (object)DBNull.Value),
                new SqlParameter("@user_updated_by", user.UpdatedBy ?? (object)DBNull.Value),
                errorMessageParam
            );

            string? errorMessage = errorMessageParam.Value != DBNull.Value ? errorMessageParam.Value.ToString() : null;
            return (result, errorMessage);
        }

        public async Task<(int Result, string? ErrorMessage)> UpdateFarmer(TbFarmer farmer)
        {
            var errorMessageParam = new SqlParameter("@error_message", SqlDbType.NVarChar, 1000)
            {
                Direction = ParameterDirection.Output
            };

            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_SingleUpdateFarmer @farmer_id, @user_id, @farm_size, @farm_location, @pin_code, @irrigation_method, @soil_type, @farming_experience, @membership_status, @membership_expiry, @language_preference, @farmer_updated_by, @error_message OUTPUT",
                new SqlParameter("@farmer_id", farmer.FarmerId ?? (object)DBNull.Value),
                new SqlParameter("@user_id", farmer.UserId ?? (object)DBNull.Value),
                new SqlParameter("@farm_size", farmer.FarmSize ?? (object)DBNull.Value),
                new SqlParameter("@farm_location", farmer.FarmLocation ?? (object)DBNull.Value),
                new SqlParameter("@pin_code", farmer.PinCode ?? (object)DBNull.Value),
                new SqlParameter("@irrigation_method", farmer.IrrigationMethod ?? (object)DBNull.Value),
                new SqlParameter("@soil_type", farmer.SoilType ?? (object)DBNull.Value),
                new SqlParameter("@farming_experience", farmer.FarmingExperience ?? (object)DBNull.Value),
                new SqlParameter("@membership_status", farmer.MembershipStatus ?? (object)DBNull.Value),
                new SqlParameter("@membership_expiry", farmer.MembershipExpiry ?? (object)DBNull.Value),
                new SqlParameter("@language_preference", farmer.LanguagePreference ?? (object)DBNull.Value),
                new SqlParameter("@farmer_updated_by", farmer.UpdatedBy ?? (object)DBNull.Value),
                errorMessageParam
            );

            string? errorMessage = errorMessageParam.Value != DBNull.Value ? errorMessageParam.Value.ToString() : null;
            return (result, errorMessage);
        }
    }
}
