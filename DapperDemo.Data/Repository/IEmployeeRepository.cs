using DapperDemo.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperDemo.Data.Repository
{
    public interface IEmployeeRepository
    {
        Employee Find(int id);
        List<Employee> GetAll();

        Employee Add(Employee employee);
        Task<Employee> AddAsync(Employee employee);
        Employee Update(Employee employee);
        void Remove(int id);
    }
}
