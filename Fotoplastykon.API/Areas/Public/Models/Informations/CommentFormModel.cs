using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Fotoplastykon.API.Areas.Public.Models.Informations
{
    public class CommentFormModel : IValidatableObject
    {
        public long Id { get; set; }
        public long InformationId { get; set; }
        public long? ParentId { get; set; }
        public string Content { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<CommentFormModel>(v =>
            {
                v.RuleFor(m => m.Content).NotEmpty().WithMessage("Treść jest wymagana");
            }).Validate(this).Result();
        }
    }
}
