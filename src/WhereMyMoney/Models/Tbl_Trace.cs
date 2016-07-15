using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhereMyMoney.Models
{
    public partial class Tbl_Trace
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 999999999.00)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TraceDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual Tbl_Currency Currency { get; set; }
        public virtual Tbl_User User { get; set; }
    }
}
