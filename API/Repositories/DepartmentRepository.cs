using API.Context;
using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Web.UI.WebControls;
using System.Data;

namespace API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);
        DynamicParameters param = new DynamicParameters();
        public int Creat(Department department)
        {
            var SP = "SPDepartmentInsert";
            param.Add("@Name", department.Name);
            var create = con.Execute(SP, param, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int Id)
        {
            var SP = "SPDepartmentDelete";
            param.Add("@id", Id);
            var delete = con.Execute(SP, param, commandType: CommandType.StoredProcedure);
            return delete;
        }

        public async Task<IEnumerable<Department>> Get()
        {
            var SP = "SPDepartmentGetAll";
            var show = await con.QueryAsync<Department>(SP, commandType: CommandType.StoredProcedure);
            return show;
        }

        public async Task<Department> Get(int Id)
        {
            var SP = "SPDepartmentGetID";
            param.Add("@id", Id);
            var getID = await con.QueryFirstOrDefaultAsync<Department>(SP, param, commandType : CommandType.StoredProcedure);
            return getID;
        }

        public int Update(int Id, Department department)
        {
            var SP = "SPDepartmentUpdate";
            param.Add("@id", Id);
            param.Add("@name", department.Name);
            var update = con.Execute(SP, param, commandType: CommandType.StoredProcedure);
            return update;
        }

        Task<IEnumerable<Department>> IDepartmentRepository.Get(int Id)
        {
            throw new NotImplementedException();
        }
    }
}