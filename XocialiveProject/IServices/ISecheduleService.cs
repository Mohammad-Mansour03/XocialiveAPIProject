using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.Models;

namespace MoalejilAcademy.IServices
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
