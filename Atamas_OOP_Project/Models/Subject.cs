using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public class Subject
    {
        private static int _lastId = 0;

        public int SubjectId { get; private set; }

        public Subject()
        {
            SubjectId = ++_lastId;
        }

        public string Name { get; set; }
    }

}