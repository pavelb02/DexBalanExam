using BankSystem.App.Dto;
using FluentValidation;

namespace APP.Validation
{
    public class BuildingDtoValidator : AbstractValidator<BuildingDto>
    {
        public BuildingDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название здания не может быть пустым")
                .Length(2, 100).WithMessage("Название здания должно содержать от 2 до 100 символов");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Адрес не может быть пустым")
                .Length(10, 200).WithMessage("Адрес должен содержать от 10 до 200 символов");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание не может быть пустым")
                .Length(10, 500).WithMessage("Описание должно содержать от 10 до 500 символов");
        }
    }
}