using DapperDemo.Data.Data;
using DapperDemo.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Data.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepositoryEF(ApplicationDbContext db)
        {
            _db = db;
        }




        public async Task<Company> Add(Company company)
        {
            await _db.AddAsync(company);
            await _db.SaveChangesAsync();
            return company;
        }

        public Company Find(int id)
        {
            return _db.Companies.Find(id);
        }

        public List<Company> GetAll()
        {
            return _db.Companies.ToList();
        }

        public async Task Remove(int id)
        {
            Company company = _db.Companies.FirstOrDefault(u => u.CompanyId == id);
            _db.Companies.Remove(company);
            _db.SaveChanges();
            return;
        }

        public async Task<Company> Update(Company company)
        {
            _db.Companies.Update(company);
            _db.SaveChanges();
            return company;
        }
    }
}
