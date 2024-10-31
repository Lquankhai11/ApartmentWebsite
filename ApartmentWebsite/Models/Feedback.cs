using System;
using System.Collections.Generic;

namespace ApartmentWebsite.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int? NumberStart { get; set; }
        public int? ApartmentId { get; set; }
        public int? UserId { get; set; }

        public virtual Apartment? Apartment { get; set; }
        public virtual UserInf? User { get; set; }
    }
}
