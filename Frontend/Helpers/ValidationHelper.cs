using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Helpers;

public static class ValidationHelper
{
    public static bool ValidateObject<T>(T target, Action<IEnumerable<ValidationResult>> onError)
        where T : class
    {
        List<ValidationResult> errors = new();

        Validator.TryValidateObject(target, new ValidationContext(target), errors, true);

        if (errors.Count > 0)
        {
            onError?.Invoke(errors);
            return false;
        }

        return true;
    }
}
