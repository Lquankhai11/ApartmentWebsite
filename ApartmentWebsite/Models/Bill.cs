using System;
using System.Collections.Generic;

namespace ApartmentWebsite.Models
{
    public partial class Bill
    {
        public int BillId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DatePayment { get; set; }
        public int? Time { get; set; }
        public int? ApartmentId { get; set; }
        public int? UserId { get; set; }

        public virtual Apartment? Apartment { get; set; }
        public virtual UserInf? User { get; set; }
    }
}
