using BankSystem.App.Dto;
using FluentValidation;

namespace APP.Validation
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя пользователя не может быть пустым")
                .Length(2, 100).WithMessage("Имя должно содержать от 2 до 100 символов");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не может быть пустым")
                .EmailAddress().WithMessage("Неверный формат email");
        }
    }
}