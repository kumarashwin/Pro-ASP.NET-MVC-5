using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HelperMethods.Models {
    [DisplayName("New Person")]
    public class Person {
        [HiddenInput(DisplayValue=false)]
        public int PersonId { get; set; }

        [Display(Name="First")]
        public string FirstName { get; set; }

        [Display(Name="Last")]
        public string LastName { get; set; }

        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Address HomeAddress { get; set; }

        [Display(Name="Approved")]
        public bool IsApproved { get; set; }

        [UIHint("Enum")]
        public Role Role { get; set; }
    }
    
    public class Address {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
    
    public enum Role {
        Admin,
        User,
        Guest
    }
}