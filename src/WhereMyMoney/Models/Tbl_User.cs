using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WhereMyMoney.Validations;

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

        [Display(Name = "Is Administrator")]
        public bool IsAdmin { get; set; }

        public virtual ICollection<Tbl_Category> Tbl_Category { get; set; }

        public virtual ICollection<Tbl_Trace> Tbl_Trace { get; set; }

        [StringLength(12, MinimumLength = 8)]
        [NotMapped]
        [Display(Name = "New Password")]
        [CompareNotEqual("Password", ErrorMessage = "New Password must be different from Previous Password.")]
        public string NewPassword { get; set; }

        [StringLength(12, MinimumLength = 8)]
        [NotMapped]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "New Password must be the same with Confirm Password.")]
        public string RetypePassword { get; set; }
    } 
}
