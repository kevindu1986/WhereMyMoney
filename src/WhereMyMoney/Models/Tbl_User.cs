using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhereMyMoney.Models
{
    public partial class Tbl_User
    {
        public Tbl_User()
        {
            Tbl_Trace = new HashSet<Tbl_Trace>();
        }

        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string UserName { get; set; }

        [StringLength(12, MinimumLength =8)]
        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Tbl_Trace> Tbl_Trace { get; set; }
    }
}
