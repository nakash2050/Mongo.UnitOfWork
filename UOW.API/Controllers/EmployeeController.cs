using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UOW.Entities.Domain;
using UOW.Entities.DTO;
using UOW.Repositories.Contracts;

namespace UOW.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeController(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            this.employeeRepository = employeeRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeRepository.GetAll();
            return Ok(employees);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await employeeRepository.GetById(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Designation = employeeDTO.Designation
            };

            employeeRepository.Add(employee);
            await unitOfWork.Commit();

            var employeeInDb = await employeeRepository.GetById(employee.Id.ToString());

            return Ok(employeeInDb);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Put(string id, [FromBody] EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                Id = id,
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Designation = employeeDTO.Designation
            };

            employeeRepository.Update(employee);

            await unitOfWork.Commit();

            return Ok(await employeeRepository.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            employeeRepository.Remove(id);
            await unitOfWork.Commit();

            var employee = await employeeRepository.GetById(id);

            return Ok(employee);
        }

        [HttpGet("lastName")]
        public async Task<IActionResult> GetEmployeeLastName(string lastName)
        {
            var employee = await employeeRepository.Find(emp => emp.LastName.ToLower() == lastName);
            return Ok(employee);
        }

        [HttpPost]
        [Route("addEmployees")]
        public async Task<IActionResult> AddManyEmployees(List<EmployeeDTO> employeesDTO)
        {
            List<Employee> employees = new List<Employee>();

            foreach (var emp in employeesDTO)
            {
                employees.Add(new Employee
                {
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Designation = emp.Designation
                });
            }

            employeeRepository.AddMany(employees);
            var result = await unitOfWork.Commit();

            return Ok(result);
        }
    }
}
