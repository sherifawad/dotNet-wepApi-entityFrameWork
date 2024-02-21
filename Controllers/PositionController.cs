using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Helpers;
using dotNet_wepApi_entityFrameWork.Model.Dtos.Position;
using dotNet_wepApi_entityFrameWork.Services.PositionService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_wepApi_entityFrameWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController(IPositionService positionService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<PositionDTO>>>> Get(
            [FromQuery] QueryObject query
        )
        {
            return Ok(await positionService.GetAllPositions(query));
        }

        [HttpGet("{code:int}")]
        public async Task<ActionResult<ServiceResponse<PositionDTO>>> GetSingle(
            [FromRoute] int code
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await positionService.GetPositionByCode(code);
            if (result.Data is null)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }
    }
}
