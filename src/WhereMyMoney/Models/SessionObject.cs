using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhereMyMoney.Models
{
    public class SessionObject
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
