using XocialiveProject.Data.DTO;

namespace XocialiveProject.IServices
{
	public interface ISectionService
	{
		Task<ApiResponse<List<SectionDto>>> GetAllSections();
		Task<ApiResponse<List<SectionDto>>> GetAllCourseSections(int courseId);
		Task<ApiResponse<SectionDto>> GetSction(int  sectionId);
		Task<ApiResponse<SectionDto>> AddSection(SectionDto sectionDto);
		Task<ApiResponse<SectionDto>> RemoveSection(int sectionId);
		Task<ApiResponse<bool>> UpdateSection(SectionDto sectionDto);
		Task<ApiResponse<bool>> Enroll(int sectionId , int participantId);
		Task<ApiResponse<string>> UnEnroll(int sectionId , int participantId);
		Task<ApiResponse<SectionWithParticipant>> GetSectionWithParticipant(int sectionId);
		Task<ApiResponse<List<SectionInstructor>>> GetSectionInstructor();
		Task<ApiResponse<List<SectionWithCourseAndSchedule>>> GetSectionsWithCourseAndSchedule();
		Task<ApiResponse<List<SectionDto>>> GetSectionWithoutInstuctor();
	}
}
