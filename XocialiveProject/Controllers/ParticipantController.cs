using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XocialiveProject.IServices;

namespace XocialiveProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ParticipantController : ControllerBase
	{
		private readonly IParticipantService _participantService;

		public ParticipantController(IParticipantService participantService)
		{
			_participantService = participantService;
		}

		[HttpGet("InternIndiv")]
		public async Task<IActionResult> GetIndividuals() 
		{
			var result = await _participantService.GetIndividuals();

			if(result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll(string ? filter , string ?order , bool desc , int ?page
			, int ?pageSize)
		{
			var result = await _participantService.GetAllParticipant(filter , order , desc , page , pageSize);

			return Ok(result.Data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var result = await _participantService.GetParticipant(id);


			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpGet("{id}/Courses")]
		public async Task<IActionResult> GetParticipantWithCourses(int id)
		{
			var result = await _participantService.GetParticipantWithCourses(id);

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpGet("{id}/Num_Of_Courses")]
		public async Task<IActionResult> GetTotalCourses(int id) 
		{
			var result = await _participantService.GetTotalSections(id);

			if(result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpGet("AllParticipantWithTotalSections")]
		public async Task<IActionResult> GetAllParticipantWithTotalSections() 
		{
			var result = await _participantService.TotalSectionsPerParticipant();
		
			if(result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}
				
		[HttpGet("ParticipantWithMaxEnrollments")]
		public async Task<IActionResult> GetAllParticipantWithMaxEnrollment() 
		{
			var result = await _participantService.ParticipantWithMaxEnrollments();
		
			if(result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpGet("GetIndividualsOrCoporate")]
		public async Task<IActionResult> GetChekingParticipant()
		{
			var result = await _participantService.GetParticipantIndividualOrCopor();
		
			if(result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

	}
}
