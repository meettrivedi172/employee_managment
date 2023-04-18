using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.Dtos;
using WEBAPI.Entity;

namespace WEBAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        
        void AddEmployee(Employee employee);
         IEnumerable<Employee> GetEmployees();

          void DeleteEmployee(int id);

          void UpdateEmployee(Employee employee);
        IEnumerable<Employee> GetEmployees(int? id = null, string name = null, string department = null);

         IEnumerable<Employee> GetEmployees(string gender, string department);


         Employee GetEmployeeById(int id);
        Task<List<Employee>> GetEmployeeByIdOrName(string identifier);

        IEnumerable<Employee> SearchEmployees(string searchText);
        // void DeleteEmployee(Employee employee);
        // void UpdateEmployee(AppUser employee);


        //  Task<AppUser> GetEmployeeByIdAsync(int id);


        //   Task<bool> SaveAllAsync();
    }
}