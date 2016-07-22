using Microsoft.AspNetCore.Mvc;
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
        [Display(Name= "Trace Date")]
        public DateTime TraceDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(minimum:1, maximum:Int64.MaxValue, ErrorMessage = "Please select a currency.")]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Required]
        [Range(minimum: 1, maximum: Int64.MaxValue, ErrorMessage = "Please select a category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual Tbl_Category Category { get; set; }
        public virtual Tbl_Currency Currency { get; set; }
        public virtual Tbl_User User { get; set; }
    }
}
