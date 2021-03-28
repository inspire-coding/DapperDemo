using DapperDemo.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperDemo.Data.Repository
{
    public interface IBonusRepository
    {
        Task<List<Employee>> GetEmployeeWithCompany(int companyId);

        Company GetCompanyWithEmployees(int id);

        List<Company> GetAllCompanyWithEmployees();

        void AddTestCompanyWithEmployee(Company objComp);
        void AddTestCompanyWithEmployeeWithTransaction(Company objComp);

        void RemoveRange(int[] companyId);

        List<Company> FilterCompanyByName(string name);
    }
}
