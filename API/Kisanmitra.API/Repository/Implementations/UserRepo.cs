using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Data;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UserRepo : IUser
    {
        private readonly ApplicationDbContext _context;
        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(TbUser? User, string? ErrorMessage)> AuthenticateUser(string email, string password)
        {
            var emailParam = new SqlParameter("@Email", email);
            var passwordParam = new SqlParameter("@Password", password);
            var errorMessageParam = new SqlParameter("@ErrorMessage", System.Data.SqlDbType.NVarChar, 1000)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var users = await _context.TbUsers
            .FromSqlRaw("EXEC sp_LoginUser @Email, @Password, @ErrorMessage OUTPUT", emailParam, passwordParam, errorMessageParam)
            .ToListAsync();

            var errorMessage = errorMessageParam.Value as string;
            var user = users.FirstOrDefault();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return (null, errorMessage);
            }

            if (user == null || string.IsNullOrEmpty(user.UserId))
            {
                errorMessage = "User not found!";
                return (null, errorMessage);
            }

            return (user, null);

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
    }
}
