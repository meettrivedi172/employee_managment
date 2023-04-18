using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Data;
using WEBAPI.Dtos;
using WEBAPI.Entity;
using WEBAPI.Interfaces;

namespace WEBAPI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController  : ControllerBase
    {
     
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
         {
         
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }




    [HttpPost("add-employee")]
    public  ActionResult AddEmployee([FromBody] Employee employee)
    {
      

         _employeeRepository.AddEmployee(employee);
        

        return Ok();
    }

     [HttpGet]
    public IActionResult GetEmployees() {
        var employees = _employeeRepository.GetEmployees();
        return Ok(employees);
    }


      [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id) {
        _employeeRepository.DeleteEmployee(id);
        return Ok();
    }

     [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, [FromBody] Employee employee) {
        if (id != employee.Id) {
            return BadRequest();
        }

        _employeeRepository.UpdateEmployee(employee);
        return Ok();
    }

     [HttpGet("{id}")]
    public ActionResult<Employee> GetById(int id)
    {
        var employee = _employeeRepository.GetEmployeeById(id);
        
        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

   




    [HttpGet("filter")]
    public ActionResult<IEnumerable<Employee>> Getemployee(string gender, string department)
    {
        var employees = _employeeRepository.GetEmployees(gender, department);
        return Ok(employees);
    }



      
       [HttpGet("search={identifier}")]
     public async Task<IActionResult> GetEmployeeByIdOrName(string identifier)
    {
        var employees = await _employeeRepository.GetEmployeeByIdOrName(identifier);

        if (employees == null || employees.Count == 0)
        {
            return NotFound();
        }

        return Ok(employees);
    }
    }
}