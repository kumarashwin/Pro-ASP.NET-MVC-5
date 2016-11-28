using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Infrastructure {
    public class MustBeTrueAttribute : ValidationAttribute {
        public override bool IsValid(object value) => value is bool && (bool)value;
    }
}