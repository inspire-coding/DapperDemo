using DapperDemo.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperDemo.Data.Repository
{
    public interface IEmployeeRepository
    {
        Employee Find(int id);
        Task<List<Employee>> GetAll();

        Employee Add(Employee employee);
        Task<Employee> AddAsync(Employee employee);
        Employee Update(Employee employee);
        Task Remove(int id);
    }
}
