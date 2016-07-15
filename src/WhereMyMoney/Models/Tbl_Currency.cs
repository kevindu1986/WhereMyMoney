using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhereMyMoney.Models
{
    public partial class Tbl_Currency
    {
        public Tbl_Currency()
        {
            Tbl_Trace = new HashSet<Tbl_Trace>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string CurrencyName { get; set; }

        [StringLength(4, MinimumLength =3)]
        [Required]
        public string CurrencyShortName { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Tbl_Trace> Tbl_Trace { get; set; }
    }
}
