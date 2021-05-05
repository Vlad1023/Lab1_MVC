using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_MVC.Models
{
    public class Works
    {
        public int WorksId { get; set; }
        public int WorkersId { get; set; }
        public Workers Workers { get; set; }

        public int WorkTypesId { get; set; }
        public WorkTypes WorkTypes { get; set; }

        public DateTime StartDate;
        public DateTime EndDate;
    }
}
