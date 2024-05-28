using AssignmentApp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelMakeController : ControllerBase
    {
        private readonly ICarModelService _carModelService;

        public ModelMakeController(ICarModelService carModelService)
        {
            _carModelService = carModelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetModels([FromQuery] int modelyear, [FromQuery] string make)
        {
            var models = await _carModelService.GetModelsForMakeIdYear(modelyear, make);
            return Ok(new { Models = models });
        }
    }
}
