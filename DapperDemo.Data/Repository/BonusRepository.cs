using Dapper;
using DapperDemo.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DapperDemo.Data.Repository
{
    public class BonusRepository : IBonusRepository
    {
        private IDbConnection db;

        public BonusRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public void AddTestCompanyWithEmployee(Company objComp)
        {
            var sql = "INSERT INTO Companies (Name, Address, City, PostalCode) "
                + "VALUES(@Name, @Address, @City, @PostalCode) "
                + "SELECT CAST (SCOPE_IDENTITY() AS int)";

            var id = db.Query<int>(sql, objComp).Single();

            objComp.CompanyId = id;

            //foreach (var employee in objComp.Employees)
            //{
            //    employee.CompanyId = objComp.CompanyId;

            //    var sqlEmp = "INSERT INTO Employees (Name, Title, Email, Phone, CompanyId) "
            //        + "VALUES(@Name, @Title, @Email, @Phone, @CompanyId) "
            //        + "SELECT CAST (SCOPE_IDENTITY() AS int)";

            //    db.Query<int>(sqlEmp, employee).Single();
            //}

            objComp.Employees.Select(c => { c.CompanyId = id; return c; }).ToList();

            var sqlEmp = "INSERT INTO Employees (Name, Title, Email, Phone, CompanyId) "
                    + "VALUES(@Name, @Title, @Email, @Phone, @CompanyId) "
                    + "SELECT CAST (SCOPE_IDENTITY() AS int)";

            db.Execute(sqlEmp, objComp.Employees);
        }

        public void AddTestCompanyWithEmployeeWithTransaction(Company objComp)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    var sql = "INSERT INTO Companies (Name, Address, City, PostalCode) "
                            + "VALUES(@Name, @Address, @City, @PostalCode) "
                            + "SELECT CAST (SCOPE_IDENTITY() AS int)";

                    var id = db.Query<int>(sql, objComp).Single();

                    objComp.CompanyId = id;

                    objComp.Employees.Select(c => { c.CompanyId = id; return c; }).ToList();

                    var sqlEmp = "INSERT INTO Employees (Name, Title, Email, Phone, CompanyId) "
                                + "VALUES(@Name, @Title, @Email, @Phone, @CompanyId) "
                                + "SELECT CAST (SCOPE_IDENTITY() AS int)";

                    db.Execute(sqlEmp, objComp.Employees);

                    transaction.Complete();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public List<Company> GetAllCompanyWithEmployees()
        {
            var sql = "SELECT C.*, E.* FROM Employees AS E INNER JOIN Companies AS C ON E.CompanyId = C.CompanyId";

            var companyDic = new Dictionary<int, Company>();

            var company = db.Query<Company, Employee, Company>(sql, (c, e) =>
            {
                if (!companyDic.TryGetValue(c.CompanyId, out var currentCompany))
                {
                    currentCompany = c;
                    companyDic.Add(currentCompany.CompanyId, currentCompany);
                }
                currentCompany.Employees.Add(e);
                return currentCompany;
            }, splitOn: "EmployeeId");

            return company.Distinct().ToList();
        }

        public Company GetCompanyWithEmployees(int id)
        {
            var p = new
            {
                CompanyId = id
            };

            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId "
                + "SELECT * FROM Employees WHERE CompanyId = @CompanyId";

            Company company;

            using (var lists = db.QueryMultiple(sql, p))
            {
                company = lists.Read<Company>().ToList().FirstOrDefault();
                company.Employees = lists.Read<Employee>().ToList();
            }

            return company;
        }

        public async Task<List<Employee>> GetEmployeeWithCompany(int companyId)
        {
            var sql = "SELECT E.*, C.* FROM Employees AS E INNER JOIN Companies AS C ON E.CompanyId = C.CompanyId";

            if (companyId != 0)
            {
                sql += " WHERE E.CompanyId = @CompanyId ";
            }

            var employee = await db.QueryAsync<Employee, Company, Employee>(sql, (e, c) =>
            {
                e.Company = c;
                return e;
            }, new { companyId }, splitOn: "CompanyId");

            return employee.ToList();
        }

        public void RemoveRange(int[] companyId)
        {
            db.Query("DELETE FROM Companies WHERE CompanyId IN @companyId", new { companyId });
        }

        public List<Company> FilterCompanyByName(string name)
        {
            return db.Query<Company>("SELECT * FROM Companies WHERE Name like '%' + @name + '%'", new { name }).ToList();
        }
    }
}
