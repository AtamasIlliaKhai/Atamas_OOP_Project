using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Atamas_OOP_Project.Models
{
    [JsonObject(IsReference = false)]
    public class Tutor : User
    {
        [JsonProperty("hourlyRate")]
        public decimal HourlyRate { get; set; } = 300;

        [JsonProperty("rating")]
        public double Rating { get; set; } = 0;

        [JsonProperty("yearsOfExperience")]
        public int YearsOfExperience { get; set; } = 1;

        [JsonProperty("subjects")]
        public List<Subject> Subjects { get; set; } = new();

        [JsonIgnore]
        public List<Schedule> Schedules { get; set; } = new();

        [JsonIgnore]
        public List<TeachingMaterial> TeachingMaterials { get; set; } = new();

        [JsonIgnore]
        public string FullDisplay => $"{FirstName} {LastName} — {Email}";

        public Tutor() : base() { }

        public void AddTeachingSubject(Subject subject)
        {
            if (!Subjects.Any(s => s.SubjectId == subject.SubjectId))
            {
                Subjects.Add(subject);
            }
        }

        public void RemoveTeachingSubject(int subjectId)
        {
            var subject = Subjects.FirstOrDefault(s => s.SubjectId == subjectId);
            if (subject != null)
            {
                Subjects.Remove(subject);
            }
        }

        public bool CanTeachSubject(int subjectId)
        {
            return Subjects.Any(s => s.SubjectId == subjectId);
        }
    }
}