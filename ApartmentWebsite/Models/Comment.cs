using System;
using System.Collections.Generic;

namespace ApartmentWebsite.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public DateTime? DateComment { get; set; }
        public string? Content { get; set; }
        public int? ApartmentId { get; set; }
        public int? UserId { get; set; }

        public virtual Apartment? Apartment { get; set; }
        public virtual UserInf? User { get; set; }
    }
}
