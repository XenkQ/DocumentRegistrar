using Backend.Data;
using Dtos.UserDtos;
using FluentValidation;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator(AppDbContext dbContext)
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom((value, context) =>
            {
                bool emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });

        RuleFor(r => r.FirstName).NotEmpty();

        RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Equal(r => r.ConfirmPassword);

        RuleFor(r => r.RoleId).NotEmpty();
    }
}
