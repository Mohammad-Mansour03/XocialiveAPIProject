using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;

namespace XocialiveProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SectionController : ControllerBase
	{
		private readonly ISectionService _sectionService;

		public SectionController(ISectionService sectionService)
		{
			_sectionService = sectionService;
		}

		[HttpGet("SectionsCourse/{courseId}")]
		public async Task<IActionResult> GetSectionsCourse(int courseId) 
		{
			var result = await _sectionService.GetAllCourseSections(courseId);
		
			if (result.Success)
				return Ok(result);

			return BadRequest(result.Message);

		}


		[HttpGet]
		public async Task<IActionResult> GetAllSections()
		{
			var result = await _sectionService.GetAllSections();

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSection(int id)
		{
			var result = await _sectionService.GetSction(id);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddSection(SectionDto sectionDto)
		{
			var result = await _sectionService.AddSection(sectionDto);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateSection(SectionDto sectionDto)
		{
			var result = await _sectionService.UpdateSection(sectionDto);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteSection(int id)
		{
			var result = await _sectionService.RemoveSection(id);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpPost("Enroll")]
		public async Task<IActionResult> Enroll([FromBody] EnrollmentDto enrollmentDto)
		{
			var result = await _sectionService.Enroll(enrollmentDto.SectionId,
				enrollmentDto.ParticipantId);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpDelete("{sectionId}/{participantId}")]
		public async Task<IActionResult> UnEnroll(int sectionId, int participantId)
		{
			var result = await _sectionService.UnEnroll(sectionId, participantId);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpGet("{sectionId}/Participants")]
		public async Task<IActionResult> GetSectionWithParticipant(int sectionId) 
		{
			var result = await _sectionService.GetSectionWithParticipant(sectionId);

			if (result.Success)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpGet("GetInstructors")]
		public async Task<IActionResult> GetInstructorEachSection() 
		{
			var result = await _sectionService.GetSectionInstructor();

			if (result.Success)
				return Ok(result.Data);

			return BadRequest(result);
		}

		[HttpGet("WithScheduleType")]
		public async Task<IActionResult> GetSectionWithScheduleType() 
		{
			var result = await _sectionService.GetSectionsWithCourseAndSchedule();

			if(result.Success)
				return Ok(result.Data);
			
			return BadRequest(result);
		}	
		
		[HttpGet("SectionsWithoutInstructors")]
		public async Task<IActionResult> GetSectionWithoutInstructor() 
		{
			var result = await _sectionService.GetSectionWithoutInstuctor();

			if(result.Success)
				return Ok(result.Data);
			
			return BadRequest(result);
		}
	}
}
