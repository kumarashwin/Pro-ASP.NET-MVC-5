using System;
using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Infrastructure {
    public class FutureDateAttribute : RequiredAttribute {
        public override bool IsValid(object value) =>
            base.IsValid(value) && (DateTime)value > DateTime.Now;
    }
}