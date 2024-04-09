using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Delivery_Service.Domain
{ 
    [Table("Driver", Schema = "delivery")]
    public class Driver
    {
        private string _id;
        private string _name;
        private string _email;
        private string _password;
        private string _phoneNumber;
        private string _availabilityStatus; // offline, online
        private bool _isBusy;
        private DateTime _createTime;
        private DateTime _updateTime;

        [Key]
        [MaxLength(60, ErrorMessage = "This field must be less than or equals 60 character")]
        public string Id { get => _id; set => _id = value; }

        [Required]
        [MaxLength(150, ErrorMessage = "This field must be less than or equals 150 character")]
        public string Name { get => _name; set => _name = value; }

        [Required]
        [MaxLength(150, ErrorMessage = "This field must be less than or equals 150 character")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get => _email; set => _email = value; }

        [Required]
        [MaxLength(500, ErrorMessage = "This field must be less than or equals 150 character")]
        public string Password { get => _password; set => _password = value; }

        [Required]
        [MaxLength(50, ErrorMessage = "This field must be less than or equals 50 character")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }

        [Required]
        [MaxLength(10, ErrorMessage = "This field must be less than or equals 10 character")]
        public string AvailabilityStatus { get=>_availabilityStatus; set=>_availabilityStatus=value; }

        public bool IsBusy { get => _isBusy; set => _isBusy = value; }

        [Required]
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }

        public DateTime UpdateTime { get => _updateTime; set => _updateTime = value; }        
    }
}
