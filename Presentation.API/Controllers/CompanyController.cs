using Application.DTO.Request;
using Application.DTO.Response;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Handlers;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionFilter))]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CompanyResponse), 200)]
        [ProducesResponseType(typeof(MessageResponse), 404)]
        //[Authorize]
        public async Task<IActionResult> GetCompany(int id)
        {
            var result = await  _companyService.GetCompany(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(List<CompanyResponse>), 200)]
        public async Task<IActionResult> GetCompanys()
        {
            var result = await _companyService.GetCompanys();

            return Ok(result);
        }
        
        [HttpPost]
        [Route("Insert/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageResponse), 400)]
        public async Task<IActionResult> CreateCompany(CompanyRequest request)
        {
            await _companyService.CreateCompany(request);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
