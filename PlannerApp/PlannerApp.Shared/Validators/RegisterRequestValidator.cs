using FluentValidation;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage("El correo electrónico es obligatorio")
                .EmailAddress().WithMessage("Ingrese un correo electrónico válido");

            RuleFor(n => n.FirstName).NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(25).WithMessage("El nombre no puede exceder los 25 caracteres");

            RuleFor(n => n.LastName).NotEmpty().WithMessage("El apellido es obligatorio")
                .MaximumLength(25).WithMessage("El apellido no puede exceder los 25 caracteres");

            RuleFor(n => n.Password).NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

            RuleFor(n => n.ConfirmPassword).Equal(p => p.Password)
                .WithMessage("Las contraseñas deben ser iguales");
        }
    }
}
