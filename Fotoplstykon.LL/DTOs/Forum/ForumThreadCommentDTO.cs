using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Forum
{
    public class ForumThreadCommentDTO : IValidatableObject
    {
        public long Id { get; set; }
        public long ThreadId { get; set; }
        public long? ParentId { get; set; }
        public string CreatedByName { get; set; }
        public string PhotoUrl { get; set; }
        public long CreatedById { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public List<ForumThreadCommentDTO> Replies { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<ForumThreadCommentDTO>(v =>
            {
                v.RuleFor(m => m.Content).NotEmpty().WithMessage("Treść jest wymagana");
            }).Validate(this).Result();
        }
    }
}
