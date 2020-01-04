using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Fotoplastykon.BLL.Extensions;

namespace Fotoplastykon.BLL.DTOs.Information
{
    public class CommentDTO : IValidatableObject
    {
        public long Id { get; set; }
        public string CreatorFullName { get; set; }
        public long CreatedById { get; set; }
        public string PhotoUrl { get; set; }
        public string Content { get; set; }
        public long InformationId { get; set; }
        public long? ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public List<CommentDTO> Replies { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<CommentDTO>(v =>
            {
                v.RuleFor(m => m.Content).NotEmpty().WithMessage("Treść jest wymagana");
            }).Validate(this).Result();
        }
    }
}
