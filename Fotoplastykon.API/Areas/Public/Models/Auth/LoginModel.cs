﻿using FluentValidation;
using Fotoplastykon.API.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Auth
{
    public class LoginModel : IValidatableObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            return this.Rules<LoginModel>(v =>
            {
                v.RuleFor(m => m.UserName).NotEmpty().WithMessage("Nazwa użytkownika jest wymagana");
                v.RuleFor(m => m.Password).NotEmpty().WithMessage("Hasło jest wymagane");
            }).Validate(this).Result();
        }
    }
}
