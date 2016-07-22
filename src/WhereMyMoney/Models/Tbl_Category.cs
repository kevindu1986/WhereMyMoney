using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhereMyMoney.Models
{
    public partial class Tbl_Category
    {
        public Tbl_Category()
        {
            Tbl_Trace = new HashSet<Tbl_Trace>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string CategoryName { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Tbl_Trace> Tbl_Trace { get; set; }
        public virtual Tbl_User User { get; set; }
    }
}
