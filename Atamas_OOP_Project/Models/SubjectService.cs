using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public class SubjectService
    {
        private List<Subject> _subjects = new List<Subject>();

        public Subject CreateSubject(string name)
        {
            var subject = new Subject { Name = name };
            _subjects.Add(subject);
            return subject;
        }

        public List<Subject> GetAllSubjects()
        {
            return _subjects.ToList();
        }

        public Subject GetSubjectById(int id)
        {
            return _subjects.FirstOrDefault(s => s.SubjectId == id);
        }
    }
}
