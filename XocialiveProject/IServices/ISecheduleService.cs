using XocialiveProject.Data.DTO;
using XocialiveProject.Models;

namespace XocialiveProject.IServices
{
	public interface ISecheduleService
	{
		Task<ApiResponse<List<ScheduleDto>>> GetSchedules();
		Task<ApiResponse<ScheduleDto>> GetSchedule(int scheduleId);

		Task<ApiResponse<ScheduleDto>> AddSchedule(ScheduleDto schedule);
		Task<ApiResponse<ScheduleDto>> RemoveSchedule(int id);
		Task<ApiResponse<bool>> UpdateSchedule(ScheduleDto schedule);

	}
}
