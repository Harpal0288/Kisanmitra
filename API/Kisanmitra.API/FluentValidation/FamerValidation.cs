using FluentValidation;
using Models.DTOs;

namespace Kisanmitra.API.FluentValidation
{
    public class FamerValidation : AbstractValidator<FarmerDTO>
    {
        public FamerValidation()
        {
            RuleFor(x => x.User).NotNull().WithMessage("User details are required")
                .SetValidator(new UserValidator());
            RuleFor(x => x.Farmer).NotNull().WithMessage("Farmer details are required")
                .SetValidator(new FarmerDetailsValidator());
        }
    }

    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.AadharNumber).NotEmpty().WithMessage("Aadhar number is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role ID is required");
            RuleFor(x => x.InsertedBy).NotEmpty().WithMessage("Inserted by is required");
            RuleFor(x => x.UpdatedBy).NotEmpty().WithMessage("Updated by is required");
        }
    }

    public class FarmerDetailsValidator : AbstractValidator<FarmerDetailsDTO>
    {
        public FarmerDetailsValidator()
        {
            RuleFor(x => x.FarmSize).NotEmpty().WithMessage("Farm size is required");
            RuleFor(x => x.FarmLocation).NotEmpty().WithMessage("Farm location is required");
            RuleFor(x => x.PinCode).NotEmpty().WithMessage("Pin code is required");
            RuleFor(x => x.MembershipStatus).NotEmpty().WithMessage("Membership status is required");
            RuleFor(x => x.LanguagePreference).NotEmpty().WithMessage("Language preference is required");
            RuleFor(x => x.InsertedBy).NotEmpty().WithMessage("Inserted by is required");
            RuleFor(x => x.UpdatedBy).NotEmpty().WithMessage("Updated by is required");
        }
    }
}
