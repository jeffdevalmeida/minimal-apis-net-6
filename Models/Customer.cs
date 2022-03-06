using System.ComponentModel.DataAnnotations;

namespace CustomerMinimals.Models
{
    public class Customer
    {
        public Customer(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
