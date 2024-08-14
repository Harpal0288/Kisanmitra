using Mapster;
using Models.DTOs;
using Models.Entities;

namespace Kisanmitra.API.MappingConfig
{
    public class FarmerMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TbUser, UserDTO>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.AadharNumber, src => src.AadharNumber)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.RoleId, src => src.RoleId)
                .Map(dest => dest.InsertedDate, src => src.InsertedDate)
                .Map(dest => dest.InsertedBy, src => src.InsertedBy)
                .Map(dest => dest.UpdatedDate, src => src.UpdatedDate)
                .Map(dest => dest.UpdatedBy, src => src.UpdatedBy);

            config.NewConfig<TbFarmer, FarmerDetailsDTO>()
                .Map(dest => dest.FarmSize, src => src.FarmSize)
                .Map(dest => dest.FarmLocation, src => src.FarmLocation)
                .Map(dest => dest.PinCode, src => src.PinCode)
                .Map(dest => dest.IrrigationMethod, src => src.IrrigationMethod)
                .Map(dest => dest.SoilType, src => src.SoilType)
                .Map(dest => dest.FarmingExperience, src => src.FarmingExperience)
                .Map(dest => dest.MembershipStatus, src => src.MembershipStatus)
                .Map(dest => dest.MembershipExpiry, src => src.MembershipExpiry)
                .Map(dest => dest.LanguagePreference, src => src.LanguagePreference)
                .Map(dest => dest.InsertedDate, src => src.InsertedDate)
                .Map(dest => dest.InsertedBy, src => src.InsertedBy)
                .Map(dest => dest.UpdatedDate, src => src.UpdatedDate)
                .Map(dest => dest.UpdatedBy, src => src.UpdatedBy);
        }
    }
}
