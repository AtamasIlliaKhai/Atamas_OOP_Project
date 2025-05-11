using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public DateTime Timestamp { get; set; }
        public string Method { get; set; }
    }
}