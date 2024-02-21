using Application.DTO.Request;
using Application.DTO.Response;
using Application.Interfaces.IServices;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Handlers;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionFilter))]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(EmployeeResponse), 200)]
        public async Task<IActionResult> GetCompany(int id)
        {
            var result = await _employeeService.GetEmployee(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll/")]
        [ProducesResponseType(typeof(List<EmployeeResponse>), 200)]
        public async Task<IActionResult> GetEmployee()
        {
            var result = await _employeeService.GetEmployees();

            return Ok(result);
        }

        [HttpGet]
        [Route("Superiors")]
        [ProducesResponseType(typeof(List<EmployeeResponse>), 200)]
        public async Task<IActionResult> GetSuperiors(int department, int position)
        {
            var result = await _employeeService.GetSuperiors(department, position);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDepartmentByEmployee/")]
        [ProducesResponseType(typeof(DepartmentResponse), 200)]
        [Authorize]
        public async Task<IActionResult> GetDepartmentByEmployee()
        {
            // metodo para obtener el id  del token
            string idUser = JwtHelper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.Id);
            
            DepartmentResponse result = await _employeeService.GetEmployeeDepartment(Convert.ToInt32(idUser));
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/v1/Employee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequest request)
        {
            await _employeeService.CreateEmployee(request);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(MessageResponse), 400)]
        public async Task<IActionResult> DeletePosition(int id)
        {
            await _employeeService.DeleteEmployee(id);

            return Ok();
        }
        
        [HttpGet]
        [Route("ObtenerAprobador")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetApprover()
        {
            string idUser = JwtHelper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.Id);

            var value = await _employeeService.GetApprover(Convert.ToInt32(idUser));

            return Ok(value);
        }

        [HttpGet]
        [Route("NextApprover")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetNextApprover([FromQuery] int amount)
        {
            string idUser = JwtHelper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.Id);

            var value = await _employeeService.GetNextApprover(Convert.ToInt32(idUser), amount);

            return Ok(value);
        }


        [HttpPatch]
        [Route("HistoryFlagON")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AcceptHistoryFlag()
        {
            string idUser = JwtHelper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.Id);

            await _employeeService.EnableHistoryFlag(Convert.ToInt32(idUser));
            return Ok();
        }

        [HttpPatch]
        [Route("HistoryFlagOFF")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DissmisHistoryFlag()
        {
            string idUser = JwtHelper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.Id);

            await _employeeService.DisableHistoryFlag(Convert.ToInt32(idUser));
            return Ok();
        }

        [HttpPatch]
        [Route("ApprovalsFlagON")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AcceptApprovalsFlagFlag()
        {
            string idUser = JwtHelper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.Id);

            await _employeeService.EnableApprovalsFlagFlag(Convert.ToInt32(idUser));
            return Ok();
        }

        [HttpPatch]
        [Route("ApprovalsFlagOFF")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DissmisApprovalsFlag()
        {
            string idUser = JwtHelper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.Id);

            await _employeeService.DisableApprovalsFlag(Convert.ToInt32(idUser));

            return Ok();
        }
    }
}


