using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class FarmerDTO
    {
        public UserDTO? User { get; set; }
        public FarmerDetailsDTO? Farmer { get; set; }
    }

    public class UserDTO
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string AadharNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public string Password { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public DateTime InsertedDate { get; set; }
        public string InsertedBy { get; set; } = null!;
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; } = null!;
    }

    public class FarmerDetailsDTO
    {
        public string? FarmerId { get; set; } = null;
        public string FarmSize { get; set; } = null!;
        public string FarmLocation { get; set; } = null!;
        public string PinCode { get; set; } = null!;
        public string? IrrigationMethod { get; set; }
        public string? SoilType { get; set; }
        public int? FarmingExperience { get; set; }
        public string MembershipStatus { get; set; } = null!;
        public DateTime? MembershipExpiry { get; set; }
        public string LanguagePreference { get; set; } = null!;
        public DateTime InsertedDate { get; set; }
        public string InsertedBy { get; set; } = null!;
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; } = null!;
    }
}
