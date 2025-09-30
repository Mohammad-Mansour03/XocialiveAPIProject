using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.IServices;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize(Roles ="Software_Engineer")]
	public class CourseController : ControllerBase
	{
		private readonly ICourseService _courseService;
		public CourseController(ICourseService _courseService)
		{
			this._courseService = _courseService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCourses() 
		{
			var result = await _courseService.GetCourses();

			return Ok(result.Data);
		}	
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCourse(int id) 
		{
			var result = await _courseService.GetCourse(id);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result.Message);
		}

		[HttpPost]
		public async Task<IActionResult> AddCourse(CourseDto courseDto) 
		{

			var result = await _courseService.Add(courseDto);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result.Message);
		}

		[HttpGet("/CourseSection/{id}")]
		public async Task<IActionResult> GetCourseSections(int id) 
		{
			var result = await _courseService.GetCourseSections(id);

			if (result.Success)
				return Ok(result);

			return BadRequest(result.Message);
		}


		[HttpDelete]
		public async Task<IActionResult> DeleteCourse(int id) 
		{
			var result = await _courseService.Delete(id);

			if (result.Success)
				return Ok(result);

			return BadRequest(result.Message);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCourse(CourseDto courseDto) 
		{

			var result = await _courseService.Update(courseDto);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpGet("TotalParticipantPerCourse")]
		public async Task<IActionResult> TotalParticPerCourse() 
		{
			var result = await _courseService.GetAllCoursesWithTotalNumOfPartic();
		
			if(result.Success)
				return Ok(result.Data);

			return BadRequest(result.Message);
		}		
		
		[HttpGet("AveragePricePerHours")]
		public async Task<IActionResult> GetAveragePricePerHours() 
		{
			var result = await _courseService.GetAveragePerHours();
		
			if(result.Success)
				return Ok(result.Data);

			return BadRequest(result.Message);
		}
	}

}
