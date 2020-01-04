using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Forum
{
    public partial class ForumThreadDTO : IValidatableObject
    {
        public long Id { get; set; }
        public long? FilmId { get; set; }
        public long? PersonId { get; set; }
        public string CreatedByName { get; set; }
        public string PhotoUrl { get; set; }
        public long CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public List<ForumThreadCommentDTO> Comments { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<ForumThreadDTO>(v =>
            {
                v.RuleFor(m => m.Subject).NotEmpty().WithMessage("Temat jest wymagany");
                v.RuleFor(m => m.Content).NotEmpty().WithMessage("Treść jest wymagana");
            }).Validate(this).Result();
        }
    }
}
