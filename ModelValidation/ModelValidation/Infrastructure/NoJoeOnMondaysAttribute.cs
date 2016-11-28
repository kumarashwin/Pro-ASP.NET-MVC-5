using ModelValidation.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Infrastructure {
    public class NoJoeOnMondaysAttribute : ValidationAttribute {
        public NoJoeOnMondaysAttribute() {
            ErrorMessage = "Joe cannot book appointments on Mondays";
        }
        
        public override bool IsValid(object value) {
            var app = value as Appointment;
            if(app == null || string.IsNullOrEmpty(app.ClientName) || app.Date == null) {
                // Some other validation failed
                return true;
            } else {
                return !(app.ClientName == "Joe" && app.Date.DayOfWeek == DayOfWeek.Monday);
            }
        }
    }
}