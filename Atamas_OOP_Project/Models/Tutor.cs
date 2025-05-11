using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atamas_OOP_Project.Interfaces;

namespace Atamas_OOP_Project.Models
{
    public class Tutor : User, ITeachable
    {
        public decimal HourlyRate { get; set; }
        public double Rating { get; set; }
        public int YearsOfExperience { get; set; } // ← перейменуй або додай
        public List<Subject> Subjects { get; set; } = new();
        public List<Schedule> Schedules { get; set; } = new(); // ← Додай як список, якщо в тестах очікується саме він
        public List<TeachingMaterial> TeachingMaterials { get; set; } = new();
    }
}