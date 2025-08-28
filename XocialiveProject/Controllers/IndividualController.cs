using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;

namespace XocialiveProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IndividualController : ControllerBase
	{
		private readonly IIndividualService _individualService;
		public IndividualController(IIndividualService individualService)
		{
			_individualService = individualService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll() 
		{
			var result = await _individualService.GetIndividualsAsync();

			return Ok(result.Data);
		}	
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id) 
		{
			var result = await _individualService.GetIndividualAsync(id);

			if (result.Success)
				return Ok(result);

			return BadRequest(result.Message);
		}


		[HttpPost]
		public async Task<IActionResult> Add(IndividualsDto individualsDto) 
		{
			var result = await _individualService.Add(individualsDto);

			if (result.Success)
				return Ok(result);

			return BadRequest(result.Message);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) 
		{
			var result = await _individualService.Delete(id);
		
			if (result.Success)
				return Ok(result);

			return BadRequest(result.Message);
		}

		[HttpPut]
		public async Task<IActionResult> Update(IndividualsDto individualsDto) 
		{
			var result = await _individualService.Update(individualsDto);

			if (result.Success)
				return Ok(result);

			return BadRequest(result.Message);
		}
	}
}
