using AccessControl.Domain.Entities.Employees;
using AccessControl.Domain.Entities.Employees.ValueObjects;
using FluentValidation;

namespace AccessControl.Application.UseCases.Employees.Commands.UpdateEmployee
{
    public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("El Id del empleado es requerido.");

            RuleFor(command => command.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Los nombres son requeridos.")
                .Length(Employee.NameMinLength, Employee.NameMaxLength)
                .WithMessage($"Los nombres deben tener entre {Employee.NameMinLength} y {Employee.NameMaxLength} caracteres.");

            RuleFor(command => command.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Los apellidos son requeridos.")
                .Length(Employee.NameMinLength, Employee.NameMaxLength)
                .WithMessage($"Los apellidos deben tener entre {Employee.NameMinLength} y {Employee.NameMaxLength} caracteres.");

            RuleFor(command => command.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .MaximumLength(Email.MaxLength).WithMessage($"El correo electrónico debe tener máximo {Email.MaxLength} caracteres.")
                .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.");

            RuleFor(command => command.Position)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El cargo es requerido.")
                .Length(Employee.PositionMinLength, Employee.PositionMaxLength)
                .WithMessage($"El cargo debe tener entre {Employee.PositionMinLength} y {Employee.PositionMaxLength} caracteres.");

            RuleFor(command => command.AreaId)
                .NotEmpty().WithMessage("El área es requerida.");
        }
    }
}
