using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;

namespace XocialiveProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoporatesController : ControllerBase
	{
		private readonly ICoporateService _coporateService;

		public CoporatesController(ICoporateService coporateService)
		{
			_coporateService = coporateService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll() 
		{
			var result = await _coporateService.GetCoporatesAsync();

			return Ok(result.Data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id) 
		{
			var result = await _coporateService.GetCoporate(id);

			if(result.Success)
				return Ok(result.Data);

			return BadRequest(result.Message);
		}

		//In Primitive Type like (int , double , ...) we must write [FromBody] to tell
		//The action this parameter will take it from body as Json

		//With Complex Type like (coporateDto) the Asp.Net Core by default will deal with
		//it from the Body not as Url or Query String
		[HttpPost]
		public async Task<IActionResult> Add(CoporateDto coporateDto) 
		{
			var result = await _coporateService.AddCoporate(coporateDto);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result.Message);
		}

		[HttpDelete]
		public async Task<IActionResult> Remove(int id) 
		{
			var result = await _coporateService.Remove(id);
		
			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result.Message);
		}

		[HttpPut]
		public async Task<IActionResult> Update(CoporateDto coporateDto) 
		{
			var result = await _coporateService.Update(coporateDto);

			if (result.Success)
				return Ok(result);

			return BadRequest(result.Message);
		}
	}
}
