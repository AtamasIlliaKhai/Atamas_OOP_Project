using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Atamas_OOP_Project.Models;

namespace Atamas_OOP_Project.Interfaces
{
    public interface IAdminActions
    {
        void BlockUser(User user);
        void ApproveTutor(Tutor tutor);
    }
}