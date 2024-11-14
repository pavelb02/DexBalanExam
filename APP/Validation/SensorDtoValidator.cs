using BankSystem.App.Dto;
using FluentValidation;

namespace APP.Validation
{
    public class SensorDtoValidator : AbstractValidator<SensorDto>
    {
        public SensorDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название датчика не может быть пустым")
                .Length(2, 100).WithMessage("Название датчика должно содержать от 2 до 100 символов");

            RuleFor(x => x.Term)
                .NotEmpty().WithMessage("Температура датчика не может быть пустой");

            RuleFor(x => x.Charge)
                .GreaterThanOrEqualTo(0).WithMessage("Заряд не может быть отрицательным");

            RuleFor(x => x.Water)
                .GreaterThanOrEqualTo(0).WithMessage("Уровень влаги не может быть отрицательным");
        }
    }
}