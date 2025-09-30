using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.IServices;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleController : ControllerBase
	{
		private readonly ISecheduleService _secheduleService;

		public ScheduleController(ISecheduleService secheduleService)
		{
			_secheduleService = secheduleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetSchedules() 
		{
			var result = await _secheduleService.GetSchedules();


			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}
		
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSchedule(int id) 
		{
			var result = await _secheduleService.GetSchedule(id);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddSchedule(ScheduleDto scheduleDto) 
		{

			var result = await _secheduleService.AddSchedule(scheduleDto);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteSchedule(int id) 
		{
			var result = await _secheduleService.RemoveSchedule(id);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateSchedule(ScheduleDto scheduleDto) 
		{
			
			var result = await _secheduleService.UpdateSchedule(scheduleDto);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}
	}
}
