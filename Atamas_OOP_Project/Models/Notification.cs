using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public class Notification
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public Guid UserId { get; set; }
    }
}