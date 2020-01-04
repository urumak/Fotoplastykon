using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.FilmPeople
{
    public class FilmPersonFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhotoUrl { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<FilmPersonFormModel>(v =>
            {
                v.RuleFor(m => m.FirstName).NotEmpty().WithMessage("Imię jest wymagane");
                v.RuleFor(m => m.Surname).NotEmpty().WithMessage("Nazwisko jest wymagane");
            }).Validate(this).Result();
        }
    }
}
