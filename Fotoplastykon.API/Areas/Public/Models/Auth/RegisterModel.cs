using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.API.Extensions;

namespace Fotoplastykon.API.Areas.Public.Models.Auth
{
    public class RegisterModel : IValidatableObject
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<RegisterModel>(v =>
            {
                v.RuleFor(m => m.UserName).NotEmpty().WithMessage("Nazwa użytkownika jest wymagana");
                v.RuleFor(m => m.FirstName).NotEmpty().WithMessage("Imię jest wymagane");
                v.RuleFor(m => m.Surname).NotEmpty().WithMessage("Nazwisko jest wymagane");
                v.RuleFor(m => m.Email).EmailAddress().NotEmpty().WithMessage("Niepoprawny adres email");
                v.RuleFor(m => m.Password).NotEmpty().WithMessage("Hasło jest wymagane");
                v.RuleFor(m => m.RepeatPassword).NotEmpty().WithMessage("Powtórzenie hasła jest wymagane");
                v.RuleFor(m => m.RepeatPassword).Equal(m => m.Password).WithMessage("Powtórzenie hasła nie jest zgodne z hasłem");
            }).Validate(this).Result();
        }
    }
}
