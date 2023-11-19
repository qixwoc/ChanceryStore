using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore
{
    public interface IEntity
    {
        void Add(Object model);
        void Edit(Object model);
        void Remove(int id);
        IEnumerable<Object> GetAll();
        IEnumerable<Object> GetByValue();

    }
}
