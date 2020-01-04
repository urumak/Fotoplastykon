using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class QuestionFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public string QuestionText { get; set; }
        public bool? IsMultichoice { get; set; }

        public virtual List<AnswerFormModel> Answers { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<QuestionFormModel>(v =>
            {
                v.RuleFor(m => m.QuestionText).NotEmpty().WithMessage("Treść jest wymagana");
                v.RuleFor(m => m.Answers).NotEmpty().WithMessage("Odpowiedzi są wymagane");
                v.RuleFor(m => m.IsMultichoice).NotEmpty().WithMessage("Typ jest wymagany");
            })
            .Validate(this).Result();
        }
    }
}
