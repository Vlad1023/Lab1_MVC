using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_MVC.Models
{
    public class Workers
    {
        public int WorkersId;
        public string Name;
        public string Surname;
        public string Patronymic;
        public double Payment;
        public IList<Works> Works;
        public IList<WorkTypes> WorkTypes;
    }
}
