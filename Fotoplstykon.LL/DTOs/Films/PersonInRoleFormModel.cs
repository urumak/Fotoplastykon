using Fotoplastykon.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Films
{
    public class PersonInRoleFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public long? PersonId { get; set; }
        public RoleType? Role { get; set; }
        public string CharacterName { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<PersonInRoleFormModel>(v =>
            {
                v.RuleFor(m => m.PersonId).NotEmpty().Must(x => x != 0).WithMessage("Osoba jest wymagana");
                v.RuleFor(m => m.Role).NotNull().NotEmpty().WithMessage("Rola jest wymagana");
            })
            .Rules(v => 
            {
                v.RuleFor(m => m.CharacterName).NotEmpty().WithMessage("Nazwa postaci jest wymagana");
            }, Role == RoleType.Actor)
            .Validate(this).Result();
        }
    }
}
