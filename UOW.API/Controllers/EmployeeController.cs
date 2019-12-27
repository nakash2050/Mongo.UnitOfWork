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

            var employeeInDb = await employeeRepository.Get(employee.Id.ToString());

            return Ok(employeeInDb);
        }
    }
}
