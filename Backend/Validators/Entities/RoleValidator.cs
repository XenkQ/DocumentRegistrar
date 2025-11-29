using Backend.Data;
using Backend.Entities;
using FluentValidation;

namespace Backend.Validators.Entities;

public class RoleValidator : AbstractValidator<Role>
{
    public RoleValidator(AppDbContext dbContext)
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .Custom((value, context) =>
            {
                bool symbolInUse = dbContext.Roles.Any(u => u.Name == value);
                if (symbolInUse)
                {
                    context.AddFailure("Symbol", "That symbol is taken");
                }
            });
    }
}
