using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atamas_OOP_Project.Interfaces
{
    public interface IDataManager
    {
        void Save(object data);
        object Load();
    }
}