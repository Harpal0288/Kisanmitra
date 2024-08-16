using FluentValidation;
using Models.DTOs;

namespace Kisanmitra.API.FluentValidation
{
    public class ConsultantCertificationValidator : AbstractValidator<ConsultantCertificationDTO>
    {
        public ConsultantCertificationValidator()
        {
            RuleFor(x => x.ConsultantId).NotEmpty().WithMessage("Consultant ID is required");
            RuleFor(x => x.CertificationNumber).NotEmpty().WithMessage("Certification Number is required");
            RuleFor(x => x.InsertedDate).NotEmpty().WithMessage("InsertedDate is required")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Inserted Date cannot be in the future");
            RuleFor(x => x.UpdatedDate).NotEmpty().WithMessage("Updated Date is required")
                .GreaterThan(x => x.InsertedDate).WithMessage("Updated Date should be after the Issued Date");
            RuleFor(x => x.InsertedBy).NotEmpty().WithMessage("Inserted by is required");
            RuleFor(x => x.UpdatedBy).NotEmpty().WithMessage("Updated by is required");
        }
    }
}
