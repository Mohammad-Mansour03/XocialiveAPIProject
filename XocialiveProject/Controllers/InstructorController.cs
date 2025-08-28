using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Models;

namespace XocialiveProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InstructorController : ControllerBase
	{
		private readonly IInstructorService _instructorService;

		public InstructorController(IInstructorService instructorService)
		{
			_instructorService = instructorService;
		}

		[HttpGet]
		public async Task<IActionResult> Get() 
		{
			var result = await _instructorService.GetAll();

			return Ok(result.Data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id) 
		{
			var result = await _instructorService.GetById(id);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddInstructor(InstructorDto instructorDto) 
		{
			
			var result = await _instructorService.Add(instructorDto);

			if(result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpPut]
		public async Task<IActionResult> Update(InstructorDto instructorDto) 
		{

			var result = await _instructorService.Update(instructorDto);

			if(result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id) 
		{
			var result = await _instructorService.Delete(id);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}
	}
}
