using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class DateRangeModel
    {
        public int TransactionType { get; set; }
        public DateTime? DataStartDate { get; set; }
        public DateTime? DataEndDate { get; set; }
    }
}
