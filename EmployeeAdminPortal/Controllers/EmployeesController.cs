using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Model;
using EmployeeAdminPortal.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace EmployeeAdminPortal.Controllers
{                              //xxxx =>(port no that my app is running on) 
    //its going to the localhost:xxxx/api/EmployeesController(name of the controller that is [controller])
    [Route("api/[controller]")] // this attribute is saying that this api is accessing from this particular route "api/[controller]"
    [ApiController]
    public class EmployeesController : ControllerBase
    {// we injected dbcontext file in program.cs so we can access it from anywhere inside the app even in the controller so the way to injet it inside the controller is thru constructor injection
        private readonly ApplicationDbContext dbContext;
        public EmployeesController(ApplicationDbContext DbContext)
        {
            dbContext = DbContext;
        }

        [HttpGet] // creating a get method so this is the get methods attribute
        public IActionResult GetAllEmployees()
        { //dbcontext is connecting to the db and listing all employees
          //var allEmployees = dbContext.Employees.ToList();
          //bcz we are using api restful convention so we will send it an ok response which is 200 behind the scene
          //return Ok(allEmployees);

            return Ok(dbContext.Employees.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id); //find takes the primary key so id is PK

            if(employee is null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }
         
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)  //DTO data transfer object are the
         //obj that will transfer data from app to database they are the body of http in parameters
        {
            var employeeEntity = new Employee() // here we are converting obj into entity
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            dbContext.Employees.Add(employeeEntity); // this line wont do anything at the momoent if u think that jus by adding this line will add employee to the db so its wrong
            // entity framework core wants u to save the changes urself so we will do :
            dbContext.SaveChanges(); // by calling this the changes will be transfered in db and finally this employeeEntity will be added to db

            // we are using IActionResult return type so its mandatory to return a response tht this operation was successfull or not
            return Ok(employeeEntity);

        }

        [HttpPut]
        [Route("{id:guid}")] //(id is what we are trying to update and the 2nd one is the thing that we want
// to update about the employee id so we want an obj like addemplyee that will have the info of the employee so we will create another DTO in models 
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDTO updateEmployeeDTO )
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("Employee not found");
            }
            employee.Name = updateEmployeeDTO.Name;
            employee.Email = updateEmployeeDTO.Email;
            employee.Phone = updateEmployeeDTO.Phone;
            employee.Salary = updateEmployeeDTO.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("Employee not found");
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }
    } 
}
