using System;
using System.Collections.Generic;

namespace ApartmentWebsite.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            Bills = new HashSet<Bill>();
            Comments = new HashSet<Comment>();
            Feedbacks = new HashSet<Feedback>();
            Users = new HashSet<UserInf>();
        }

        public int ApartmentId { get; set; }
        public string? ApartmentName { get; set; }
        public decimal? Price { get; set; }
        public int? Size { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }
        public bool? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual ICollection<UserInf> Users { get; set; }
    }
}
