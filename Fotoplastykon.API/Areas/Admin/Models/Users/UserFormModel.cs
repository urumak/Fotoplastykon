using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.API.Extensions;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Fotoplastykon.API.Areas.Admin.Models.Users
{
    public class UserFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsAdmin { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.Rules<UserFormModel>(v =>
            {
                v.RuleFor(m => m.UserName).NotEmpty().WithMessage("Nazwa użytkownika jest wymagana");
                v.RuleFor(m => m.FirstName).NotEmpty().WithMessage("Imię jest wymagane");
                v.RuleFor(m => m.Surname).NotEmpty().WithMessage("Nazwisko jest wymagane");
                v.RuleFor(m => m.Email).NotEmpty().WithMessage("Adres email jest wymagany");
                v.RuleFor(m => m.Email).EmailAddress().WithMessage("Niepoprawny adres email");
            })
            .Rules(v => 
            {
                v.RuleFor(m => m.RepeatPassword).NotEmpty().WithMessage("Powtórzenie hasła jest wymagane");
                v.RuleFor(m => m.RepeatPassword).Equal(m => m.Password).WithMessage("Powtórzenie hasła nie jest zgodne z hasłem");
            }, !string.IsNullOrEmpty(Password))
            .Validate(this).Result();
        }
    }
}
