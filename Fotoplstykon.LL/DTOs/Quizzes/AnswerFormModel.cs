using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class AnswerFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public string AnswerText { get; set; }
        public bool? IsCorrect { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<AnswerFormModel>(v =>
            {
                v.RuleFor(m => m.AnswerText).NotEmpty().WithMessage("Treść jest wymagana");
                v.RuleFor(m => m.IsCorrect).NotEmpty().WithMessage("Określenie poprawności jest wymagane");
            })
            .Validate(this).Result();
        }
    }
}
