using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostCard.UnitTest
{
    public class CheckFieldsValidation
    {
        /// <summary>
        /// Takes the models that it's being validated runs the specified test and returns 
        /// the error count.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<ValidationResult> Validate(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }
    }
}
