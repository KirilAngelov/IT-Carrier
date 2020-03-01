using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatelApp.Models
{
    public class User : IValidatableObject
    {
        public User()
        {

        }

        public User(int id, string username, string password, string name, string middleName, string lastName, string personalId, string telephoneNumber, string email, DateTime dateOfAppointment, bool active, DateTime fireDate)
        {
            Id = id;
            Username = username;
            Password = password;
            Name = name;
            MiddleName = middleName;
            LastName = lastName;
            PersonalId = personalId;
            TelephoneNumber = telephoneNumber;
            Email = email;
            StartDate = dateOfAppointment;
            Active = active;
            FireDate = fireDate;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Personal ID")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Entered Personal ID is not valid.")]
        public string PersonalId { get; set; }
        [Required]
        [Display(Name = "Telephone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Entered Telephone Number is not valid.")]
        public string TelephoneNumber { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        public bool Active { get; set; }

        [DataType(DataType.Date)]
        public DateTime FireDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (FireDate < StartDate)
            {
                results.Add(new ValidationResult("Fire Date must be greater than Start Date",new[] { "FireDate"}));
            }
            return results;
        }
    }
}
