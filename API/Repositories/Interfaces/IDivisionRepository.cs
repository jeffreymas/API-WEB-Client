using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    interface IDivisionRepository
    {
        Task<IEnumerable<DivisionVM>> GetAll();
        Task<DivisionVM> GetID(int Id);
        int Create(Division division);
        int Update(int Id, Division division);
        int Delete(int Id);
    }
}
