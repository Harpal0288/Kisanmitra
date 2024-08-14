using FluentValidation;
using Models.EntityDto;

namespace Kisanmitra.API.FluentValidation
{
    public class FarmerCropUpdateValidator : AbstractValidator<FarmerCropUpdateDto>
    {
        public FarmerCropUpdateValidator()
        {
            RuleFor(x => x.FarmerId)
                .NotEmpty().WithMessage("Farmer ID is required.")
                .MaximumLength(50).WithMessage("Farmer ID cannot exceed 50 characters.");

            RuleFor(x => x.Crop)
                .NotEmpty().WithMessage("Crop is required.")
                .MaximumLength(100).WithMessage("Crop cannot exceed 100 characters.");

            RuleFor(x => x.UpdatedBy)
                .NotEmpty().WithMessage("Updated By is required.")
                .MaximumLength(50).WithMessage("Updated By cannot exceed 50 characters.");

            RuleFor(x => x.NewData)
                .NotEmpty().WithMessage("New Data is required.")
                .MaximumLength(500).WithMessage("New Data cannot exceed 500 characters.");
        }
    }
}
