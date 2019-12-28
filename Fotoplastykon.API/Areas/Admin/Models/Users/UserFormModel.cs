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
                v.RuleFor(m => m.UserName).NotEmpty().MaximumLength(100);
                v.RuleFor(m => m.FirstName).MaximumLength(100);
                v.RuleFor(m => m.Surname).MaximumLength(250);
                v.RuleFor(m => m.Email).EmailAddress().NotEmpty().MaximumLength(250);
            }).Validate(this).Result();
        }
    }
}
