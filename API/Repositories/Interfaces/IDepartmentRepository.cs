using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> Get();
        Task<IEnumerable<Department>> Get(int Id);
        int Creat (Department department);
        int Update(int Id, Department department);
        int Delete(int Id);
    }
}
