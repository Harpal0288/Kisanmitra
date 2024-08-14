using Kisanmitra.API.Repository.Interface;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Entities;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Kisanmitra.API.Controllers
{
    [Route("v1/api/kisan_mitar/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(TbUser login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                Log.Warning("Invalid login request: login object or credentials are missing.");
                return BadRequest(new { success = false, message = "Invalid login request" });
            }

            var (validUser, errorMessage) = await _unitOfWork.User.AuthenticateUser(login.Email, login.Password);

            if (validUser == null)
            {
                if (errorMessage == "Invalid email or password.")
                {
                    Log.Warning("Login attempt failed for user {Email}: {ErrorMessage}", login.Email, errorMessage);
                    return Unauthorized(new { status = 401, message = errorMessage });
                }
                else
                {
                    Log.Warning("User not found with this {Email}", login.Email);
                    return NotFound(new { status = 404, message = errorMessage });
                }

            }

            IActionResult authUserResult = await AuthorizeUser(validUser);

            if (!(authUserResult is OkObjectResult okObjectResult))
            {
                Log.Error("Error while generating token for user {Email}.", login.Email);
                return Unauthorized(new { status = 500, success = false, message = "Error while generating token!" });
            }

            var authUser = okObjectResult.Value;
            string serializedUser = JsonSerializer.Serialize(authUser);

            return Ok(authUser);
        }

        private async Task<IActionResult> AuthorizeUser(TbUser validUser)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, validUser?.UserName ?? ""),
                new Claim("UserId", validUser?.UserId ?? ""),
                new Claim("Email", validUser?.Email ?? ""),
                new Claim("RoleId", validUser?.RoleId ?? "")
            };

            var token = await GenerateJwtToken(claims);
            string message;

            switch (validUser?.RoleId)
            {
                case "RA_001":
                    message = "Login successful As Admin";
                    break;
                case "RF_001":
                    message = "Login successful As Farmer";
                    break;
                case "RC_001":
                    message = "Login successful As Consultant";
                    break;
                default:
                    message = "Login successful";
                    break;
            }

            Log.Information("User {Email} authorized successfully with role {RoleId}.", validUser.Email, validUser.RoleId);
            return Ok(new { status = 200, success = true, message, accessToken = token });
        }

        private async Task<string> GenerateJwtToken(IEnumerable<Claim> claims)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = creds,
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var writtenToken = tokenHandler.WriteToken(token);

                Log.Information("JWT token generated successfully.");
                return writtenToken;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating JWT token: " + ex.Message);
                throw;
            }
        }


        //[Authorize(Policy = "AdminFarmer")]
        [HttpGet("get_user/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                var (user, errorMessage) = await _unitOfWork.User.GetUserById(userId);

                if (user == null)
                {
                    Log.Warning("User Not Found With {userId}", userId);
                    return NotFound(new { status = 404, success = false, message = $"User Not Found With {userId} user_id" });
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Log.Warning("SQL Error: {ErrorMessage}", errorMessage);
                    if (errorMessage.Contains("User does not exist"))
                    {
                        return NotFound(new { success = false, message = errorMessage });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = errorMessage });
                    }
                }

                Log.Information("User Found With {userId}", userId);
                return Ok(new { status = 200, success = true, data = user.Adapt<UserDTO>() });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Fetching Error for user with {userId} UserId: {Message}", userId, ex.Message);
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving the user data.", error = ex.Message });
            }
        }

        //[Authorize(Policy = "AdminFarmer")]
        [HttpPut("update_user")]
        public async Task<IActionResult> UpdateUser(UserDTO updatedUserDto)
        {
            if (string.IsNullOrEmpty(updatedUserDto.UserId))
            {
                return BadRequest(new { success = 403, message = "User ID is required" });
            }

            try
            {
                var userEntity = updatedUserDto.Adapt<TbUser>();

                var (result, errorMessage) = await _unitOfWork.User.UpdateUser(userEntity);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Log.Warning("SQL Error: {ErrorMessage}", errorMessage);
                    return BadRequest(new { status = 500, success = false, message = errorMessage });
                }

                await _unitOfWork.SaveAsync();

                Log.Information("User {UserId} updated successfully", updatedUserDto.UserId);
                return Ok(new { status = 200, success = true, message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Updating error for User {UserId}: {Message}", updatedUserDto.UserId, ex.Message);
                return StatusCode(500, new { status = 500, message = "An error occurred while updating the user.", error = ex.Message });
            }
        }
    }
}
