using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.API.Extensions;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.Rules<RegisterModel>(v =>
            {
                v.RuleFor(m => m.UserName).NotEmpty().MaximumLength(100);
                v.RuleFor(m => m.FirstName).MaximumLength(100);
                v.RuleFor(m => m.Surname).MaximumLength(250);
                v.RuleFor(m => m.Email).EmailAddress().NotEmpty().MaximumLength(250);
                v.RuleFor(m => m.Password).NotEmpty().MaximumLength(250);
                v.RuleFor(m => m.RepeatPassword).Equal(m => m.Password).NotEmpty().MaximumLength(250);

            }).Validate(this).Result();
        }
    }
}
