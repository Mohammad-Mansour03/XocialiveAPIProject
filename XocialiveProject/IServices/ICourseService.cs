using XocialiveProject.Data.DTO;
using XocialiveProject.Models;

namespace XocialiveProject.IServices
{
	public interface ICourseService
	{
		Task<ApiResponse<List<CourseDto>>> GetCourses();
		Task<ApiResponse<CourseDto>> GetCourse(int id);

		Task<ApiResponse<CourseDto>> Add(CourseDto course);
		Task<ApiResponse<bool>> Update(CourseDto course);
		Task<ApiResponse<CourseDto>> Delete(int id);

		Task<ApiResponse<List<AllCoursesWithTotalNumberOfParticipant>>> GetAllCoursesWithTotalNumOfPartic();
		Task<ApiResponse<List<AveragePerHours>>> GetAveragePerHours();
	
	}
}
