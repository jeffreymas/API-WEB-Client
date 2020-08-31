using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.ViewModels;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Data;
using API.Models;

namespace API.Repositories
{
    public class DivisionRepository : IDivisionRepository
    {
        SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);
        DynamicParameters parameter = new DynamicParameters();

        public int Create(Division division)
        {
            var sp = "SPDivisionInsert";
            parameter.Add("@name", division.Name);
            parameter.Add("@dep_id", division.department_id);
            var create = _conn.Execute(sp, parameter, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int Id)
        {
            var sp = "SPDivisionDelete";
            parameter.Add("@id", Id);
            var del = _conn.Execute(sp, parameter, commandType: CommandType.StoredProcedure);
            return del;
        }

        public async Task<IEnumerable<DivisionVM>> GetAll()
        {
            var sp = "SPDivisionGetAll";
            var getAll = await _conn.QueryAsync<DivisionVM>(sp, commandType: CommandType.StoredProcedure);
            return getAll;
        }

        public async Task<DivisionVM> GetID(int Id)
        {
            var sp = "SPDivisionGetId";
            parameter.Add("@id", Id);
            var getID = await _conn.QueryFirstOrDefaultAsync<DivisionVM>(sp, parameter, commandType: CommandType.StoredProcedure);
            return getID;
        }

        public int Update(int Id, Division division)
        {
            var sp = "SPDivisionUpdate";
            parameter.Add("@id", Id);
            parameter.Add("@name", division.Name);
            parameter.Add("@dep_id", division.department_id);
            var upd = _conn.Execute(sp, parameter, commandType: CommandType.StoredProcedure);
            return upd;
        }
    }
}