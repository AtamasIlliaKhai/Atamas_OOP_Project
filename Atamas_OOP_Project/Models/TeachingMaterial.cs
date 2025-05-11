using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public class TeachingMaterial
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Subject Subject { get; set; }
        public string FilePath { get; set; }
    }
}
