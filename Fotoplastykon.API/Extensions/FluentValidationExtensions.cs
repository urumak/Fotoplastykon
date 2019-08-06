﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;

namespace Fotoplastykon.API.Extensions
{
    public interface IFluentValidator<TModel> : IValidator<TModel>
    {
        IFluentValidator<TModel> Rules(Action<FluentValidator<TModel>> action, bool condition = true);
    }

    public static class Validation
    {
        #region Rules()
        public static IFluentValidator<TModel> Rules<TModel>(Action<FluentValidator<TModel>> action)
        {
            return new FluentValidator<TModel>(action);
        }
        #endregion
    }

    public class FluentValidator<TModel> : AbstractValidator<TModel>, IFluentValidator<TModel>
    {
        #region FluentValidator()
        public FluentValidator(Action<FluentValidator<TModel>> action)
        {
            action(this);
        }
        #endregion

        #region Rules()
        public IFluentValidator<TModel> Rules(Action<FluentValidator<TModel>> action, bool condition = true)
        {
            if (condition)
            {
                Include(new FluentValidator<TModel>(action));
            }

            return this;
        }
        #endregion
    }

    public static class ValidationExtensions
    {
        #region Result()
        public static IEnumerable<ValidationResult> Result(this FluentValidation.Results.ValidationResult result)
        {
            return result.Errors.Select(p => new ValidationResult(p.ErrorMessage, new[] { p.PropertyName }));
        }
        #endregion

        #region Rules()
        public static IFluentValidator<TModel> Rules<TModel>(this IValidatableObject model, Action<FluentValidator<TModel>> action)
        {
            return Validation.Rules(action);
        }
        #endregion
    }
}
