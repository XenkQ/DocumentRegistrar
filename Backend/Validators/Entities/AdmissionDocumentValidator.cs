using Backend.Data;
using Backend.Entities;
using FluentValidation;

namespace Backend.Validators.Entities;

public class AdmissionDocumentValidator : AbstractValidator<AdmissionDocument>
{
    public AdmissionDocumentValidator(AppDbContext dbContext)
    {
        RuleFor(r => r.Symbol)
            .NotEmpty()
            .Custom((value, context) =>
            {
                bool symbolInUse = dbContext.AdmissionDocuments.Any(u => u.Symbol == value);
                if (symbolInUse)
                {
                    context.AddFailure("Symbol", "That symbol is taken");
                }
            });
    }
}
