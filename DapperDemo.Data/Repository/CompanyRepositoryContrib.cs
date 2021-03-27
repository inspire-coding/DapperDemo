using Dapper.Contrib.Extensions;
using DapperDemo.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Data.Repository
{
    public class CompanyRepositoryContrib : ICompanyRepository
    {
        private IDbConnection db;

        public CompanyRepositoryContrib(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }




        public async Task<Company> Add(Company company)
        {
            var id = await db.InsertAsync(company);
            company.CompanyId = (int)id;

            return company;
        }

        public Company Find(int id)
        {
            return db.Get<Company>(id);
        }

        public List<Company> GetAll()
        {
            return db.GetAll<Company>().ToList();
        }

        public void Remove(int id)
        {
            db.Delete(new Company { CompanyId = id });
        }

        public async Task<Company> Update(Company company)
        {
            var id = await db.UpdateAsync(company);

            return company;
        }
    }
}
