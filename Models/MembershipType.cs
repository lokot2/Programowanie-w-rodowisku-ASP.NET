﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public short SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }

        public byte DiscountRate { get; set; }

        public static readonly byte Unknown = 0;

        public static readonly byte PayAsYouGo = 1;

        public virtual ICollection<Customer> Customers { get; set; }
    }
}