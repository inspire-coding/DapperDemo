using Dapper;
using DapperDemo.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private IDbConnection db;

        public CompanyRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }




        public async Task<Company> Add(Company company)
        {
            var sql = "INSERT INTO Companies (Name, Address, City, PostalCode) "
                + "VALUES(@Name, @Address, @City, @PostalCode) "
                + "SELECT CAST (SCOPE_IDENTITY() AS int)";

            //var id = db.Query<int>(sql, new 
            //{ 
            //    @Name = company.Name, 
            //    @Address = company.Address, 
            //    @City = company.City, 
            //    @PostalCode = company.PostalCode 
            //}).Single();

            var id = await db.QueryAsync<int>(sql, company);

            company.CompanyId = id.Single();
            return company;
        }

        public Company Find(int id)
        {
            var sql = "select * from Companies where CompanyId = @CompanyId";
            return db.Query<Company>(sql, new { @CompanyId = id }).Single();
        }

        public List<Company> GetAll()
        {
            var sql = "select * from Companies";
            return db.Query<Company>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE  FROM Companies WHERE CompanyId = @Id";

            db.Execute(sql, new { id });

            return;
        }

        public Company Update(Company company)
        {
            var sql = "UPDATE Companies SET Name = @Name, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode "
                + "WHERE CompanyId = @CompanyId";

            db.Execute(sql, company);

            return null;
        }
    }
}
