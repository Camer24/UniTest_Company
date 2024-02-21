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

    public class PositionController : ControllerBase
    {
        private IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        [Route("Position/{id}")]
        [ProducesResponseType(typeof(PositionResponse), 200)]

        public async Task<IActionResult> GetPosition(int id)
        {
            var result = await _positionService.GetPosition(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllPosition/")]
        [ProducesResponseType(typeof(List<PositionResponse>), 200)]
        [Authorize]
        public async Task<IActionResult> GetPosition()
        {
            string company = JwtHelper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.Company);

            var result = await _positionService.GetPositionsByCompany(Convert.ToInt32(company));

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert/")]
        public async Task<IActionResult> CreatePosition([FromBody] PositionRequest request)
        {
            await _positionService.CreatePosition(request);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(MessageResponse), 400)]
        public async Task<IActionResult> DeletePosition(int id)
        {
            await _positionService.DeletePosition(id);

            return Ok();
        }
    }
}
