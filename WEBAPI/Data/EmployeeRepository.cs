using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Dtos;
using WEBAPI.Entity;
using WEBAPI.Interfaces;

namespace WEBAPI.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
         public EmployeeRepository(DataContext context, IMapper mapper)
         {
            _mapper = mapper;
            _context = context;
            
         }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
              var employee = _context.Employees.Find(id);
        if (employee != null) {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
        }

        public Employee GetEmployeeById(int id)
        {
             return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
             return _context.Employees.ToList();
        }

    public IEnumerable<Employee> GetEmployees(int? id = null, string name = null, string department = null) {
        var query = _context.Employees.AsQueryable();

        if (id != null) {
            query = query.Where(e => e.Id == id);
        }

        if (!string.IsNullOrEmpty(name)) {
            query = query.Where(e => e.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(department)) {
            query = query.Where(e => e.Department.Contains(department));
        }

        return query.ToList();
    }

      
        public IEnumerable<Employee> GetEmployees(string gender, string department)
        {
             var query = _context.Employees.AsQueryable();

        if (!string.IsNullOrEmpty(gender))
        {
            query = query.Where(e => e.Gender == gender);
        }

        if (!string.IsNullOrEmpty(department))
        {
            query = query.Where(e => e.Department == department);
        }

        return query.ToList();
        }

        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.Find(employee.Id);
        if (existingEmployee != null) {
            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            _context.SaveChanges();
        }
        }


           public IEnumerable<Employee> SearchEmployees(string searchText)
        {
            var employees = from e in _context.Employees select e;

            if (!string.IsNullOrEmpty(searchText))
            {
                employees = employees.Where(e =>
                    e.Name.ToLower().Contains(searchText.ToLower()) ||
                    e.Department.ToLower().Contains(searchText.ToLower()));
            }

            return employees.ToList();
        }
         

         public async Task<List<Employee>> GetEmployeeByIdOrName(string identifier)
           {
              int id;
              bool isId = int.TryParse(identifier, out id);

          var employees = await _context.Employees
              .Where(e => isId ? e.Id == id : e.Name == identifier || e.Department == identifier)
              .ToListAsync();

         return employees;
           }
    }
}