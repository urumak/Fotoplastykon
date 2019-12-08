using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using FluentValidation;

namespace Fotoplastykon.API.Areas.Public.Models.Chat
{
    public class MessageModel : IValidatableObject
    {
        public string MessageText { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<MessageModel>(v =>
            {
                v.RuleFor(m => m.MessageText).NotEmpty().WithMessage("Treść jest wymagana");
            }).Validate(this).Result();
        }
    }
}
