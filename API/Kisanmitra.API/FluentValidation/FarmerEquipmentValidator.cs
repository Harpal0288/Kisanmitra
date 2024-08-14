using FluentValidation;
using Models.Entities;

namespace Kisanmitra.API.FluentValidation
{
    public class FarmerEquipmentValidator : AbstractValidator<TbFarmerEquipment>
    {
        public FarmerEquipmentValidator() 
        {
            RuleFor(x => x.EquipmentId)
                .NotEmpty().WithMessage("EquipmentId is required.")
                .MaximumLength(255).WithMessage("EquipmentId must not exceed 255 characters.");

            RuleFor(x => x.FarmerId)
                .NotEmpty().WithMessage("FarmerId is required.")
                .MaximumLength(255).WithMessage("FarmerId must not exceed 255 characters.");

            RuleFor(x => x.InsertedBy)
                .MaximumLength(255).WithMessage("InsertedBy must not exceed 255 characters.");

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(255).WithMessage("UpdatedBy must not exceed 255 characters.");

            RuleFor(x => x.InsertedDate)
                .NotEmpty().WithMessage("InsertedDate is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("InsertedDate cannot be in the future.");

            RuleFor(x => x.UpdatedDate)
                .NotEmpty().WithMessage("UpdatedDate is required.")
                .GreaterThanOrEqualTo(x => x.InsertedDate).WithMessage("UpdatedDate must be after or equal to InsertedDate.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("UpdatedDate cannot be in the future.");
        }
    }
}
