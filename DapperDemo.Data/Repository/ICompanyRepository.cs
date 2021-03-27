using DapperDemo.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperDemo.Data.Repository
{
    public interface ICompanyRepository
    {
        Company Find(int id);
        List<Company> GetAll();

        Task<Company> Add(Company company);
        Company Update(Company company);
        void Remove(int id);
    }
}
