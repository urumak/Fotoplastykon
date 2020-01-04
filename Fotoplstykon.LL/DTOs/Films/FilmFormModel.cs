using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Films
{
    public class FilmFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int? YearOfProduction { get; set; }
        public string PhotoUrl { get; set; }
        public List<PersonInRoleFormModel> People { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<FilmFormModel>(v =>
            {
                v.RuleFor(m => m.Title).NotEmpty().WithMessage("Tytuł jest wymagany");
                v.RuleFor(m => m.YearOfProduction).NotEmpty().Must(x => x != 0).WithMessage("Rok produkcji jest wymagany");
            }).Validate(this).Result();
        }
    }
}
