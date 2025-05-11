using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public class Student : User
    {
        public int GradeLevel { get; set; }
        public List<Subject> Subjects { get; set; } = new();
        public string Level { get; set; }
        public decimal Balance { get; set; }
        public List<Lesson> Lessons { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
    }
}