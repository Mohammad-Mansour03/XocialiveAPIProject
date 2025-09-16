using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Models;

namespace XocialiveProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OfficeController : ControllerBase
	{
		private readonly IOfficeService _officeService;

		public OfficeController(IOfficeService officeService)
		{
			_officeService = officeService;
		}

		[HttpPost]
		public async Task<IActionResult> AddOffice(OfficeDto officeDto)
		{

			var result = await _officeService.AddOffice(officeDto);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetOffices()
		{
			var result = await _officeService.GetAllOffices();

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOfficeById(int id) 
		{
			var result = await _officeService.GetOffice(id);


			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteOffice(int id) 
		{
			var result = await _officeService.RemoveOffice(id);


			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateOffice(OfficeDto officeDto) 
		{

			var result = await _officeService.UpdateOffice(officeDto);


			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpGet("Filter")]
		public async Task<IActionResult> GetOffices(string ? search , string ? orderBy , bool  desc 
			, int ? page , int? pageSize )
		{
			var result = await _officeService.FilteredOffices(search , orderBy , desc , page , pageSize);
			
			if(result.Success)
				return Ok(result.Data);

			return BadRequest(result);
			
		}
	} 
}
