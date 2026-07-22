using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace HR_Management_Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationException(ValidationResult validationResult)
        {
            foreach (var err in validationResult.Errors)
            {
                Errors.Add(err.ErrorMessage);
            }
        }
    }
}

// --------------------------------------------------------------------------------------------
/*
public class ValidationResult
{
    public List<ValidationFailure> Errors { get; set; }  // لیست خطاها
    public bool IsValid { get; }                          // آیا اعتبارسنجی موفق بود؟
    // ... سایر اعضا
}

public class ValidationFailure
{
    public string PropertyName { get; set; }   // نام خاصیت خطادار
    public string ErrorMessage { get; set; }   // پیام خطا
    public object AttemptedValue { get; set; } // مقدار واردشده
    // ...
}
*/