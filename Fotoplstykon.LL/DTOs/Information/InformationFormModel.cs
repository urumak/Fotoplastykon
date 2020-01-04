using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Information
{
    public class InformationFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string PhotoUrl { get; set; }
        public string Content { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<InformationFormModel>(v =>
            {
                v.RuleFor(m => m.Title).NotEmpty().WithMessage("Tytuł jest wymagany");
                v.RuleFor(m => m.Introduction).NotNull().NotEmpty().WithMessage("Wprowadzenie jest wymagane");
                v.RuleFor(m => m.Content).NotNull().NotEmpty().WithMessage("Treść jest wymagana");
            })
            .Validate(this).Result();
        }
    }
}
