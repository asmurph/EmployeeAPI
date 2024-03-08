using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeDbContext _employeeDbContext { get; }
        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeDbContext.Employees.ToListAsync(); ;
        }
        public async Task<Employee> GetById(int id)
        {
            return await _employeeDbContext.Employees.FindAsync(id);
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = _employeeDbContext.Employees.Add(employee);
            await _employeeDbContext.SaveChangesAsync();

            return result.Entity;
        }
        public async Task<Employee> UpdateEmployee(int id, Employee employee)
        {
            _employeeDbContext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _employeeDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var findEmployee = await GetById(id);
                if (findEmployee == null)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return employee;
        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            var employee = await GetById(id);
            if (employee == null)
            {
                return null;
            }
            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();

            return employee;
        }

        //public Task<IEnumerable<Employee>> GetAllEmployees()
        //{
        //    throw new NotImplementedException();
        //}
    }
}