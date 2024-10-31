using System;
using System.Collections.Generic;

namespace ApartmentWebsite.Models
{
    public partial class UserInf
    {
        public UserInf()
        {
            Bills = new HashSet<Bill>();
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            Apartments = new HashSet<Apartment>();
        }

        public int UserId { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public bool? Gender { get; set; }
        public int? RoleId { get; set; }
        public bool? Status { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}
