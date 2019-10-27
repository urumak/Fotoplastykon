using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using FluentValidation;

namespace Fotoplastykon.API.Areas.Public.Models.Forum
{
    public class ForumThreadModel : IValidatableObject
    {
        public long Id { get; set; }
        public long? FilmId { get; set; }
        public long? PersonId { get; set; }
        public string CreatorFullName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<ForumThreadCommentModel> Comments { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<ForumThreadModel>(v =>
            {
                v.RuleFor(m => m.Content).NotEmpty().WithMessage("Treść jest wymagana");
            }).Validate(this).Result();
        }
    }
}
