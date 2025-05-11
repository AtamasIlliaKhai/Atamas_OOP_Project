using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Models
{
    public class Admin : User
    {

        public string PermissionLevel { get; set; }
        public DateTime CreatedAt { get; set; }

        public Admin()
        {
            CreatedAt = DateTime.Now;
        }

        // Метод для блокування юзера
        public void BlockUser(User user) { }

        // Метод для підтвердження викладача
        public void ApproveTutor(Tutor tutor) { }
    }
}