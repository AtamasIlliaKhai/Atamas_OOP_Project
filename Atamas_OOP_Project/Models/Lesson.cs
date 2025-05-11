using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public class Lesson
    {
        public DateTime Date { get; set; }
        public Subject Subject { get; set; }
        public Tutor Tutor { get; set; }
        public Student Student { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime StartTime { get; set; }
        public int Duration { get; set; } 
        public string Status { get; set; }
        public Guid StudentId { get; set; }
        public Guid TutorId { get; set; }
    }
}