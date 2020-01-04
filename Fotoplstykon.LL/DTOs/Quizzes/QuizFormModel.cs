using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class QuizFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public List<QuestionFormModel> Questions { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<QuizFormModel>(v =>
            {
                v.RuleFor(m => m.Name).NotEmpty().WithMessage("Nazwa jest wymagana");
                v.RuleFor(m => m.Questions).NotEmpty().WithMessage("Pytania są wymagane");
            })
            .Validate(this).Result();
        }
    }
}
