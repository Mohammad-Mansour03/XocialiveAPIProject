using System;
using XocialiveProject.Data.DTO;
using XocialiveProject.Enum;
using XocialiveProject.IServices;
using XocialiveProject.Models;
using XocialiveProject.Repository;

namespace XocialiveProject.Services
{
	public class ScheduleService : ISecheduleService
	{

		private readonly IGenericRepository<Schedule> _repository;

		public ScheduleService(IGenericRepository<Schedule> repository)
		{
			_repository = repository;
		}
		public async Task<ApiResponse<ScheduleDto>> GetSchedule(int scheduleId)
		{
			var schedule = await _repository.GetById(scheduleId);

			if (schedule == null)
				return new ApiResponse<ScheduleDto>(false , "There is no schedule with this id");

			return new ApiResponse<ScheduleDto>(true , "Success" , new ScheduleDto 
			{
				Id = schedule.Id,
				FRI	= schedule.FRI,
				MON = schedule.MON,
				THU = schedule.THU,
				WED = schedule.WED,
				TUE = schedule.TUE,
				SAT = schedule.SAT,
				SUN = schedule.SUN,
				ScheduleType = schedule.ScheduleType
			});
		}

		public async Task<ApiResponse<List<ScheduleDto>>> GetSchedules()
		{
			var schedules = await _repository.GetAllAsync();

			List<ScheduleDto> scheduleDto = new List<ScheduleDto>();

			foreach (var schedule in schedules) 
			{
				scheduleDto.Add(new ScheduleDto
				{
					Id = schedule.Id,
					FRI = schedule.FRI,
					MON = schedule.MON,
					THU = schedule.THU,
					WED = schedule.WED,
					TUE = schedule.TUE,
					SAT = schedule.SAT,
					SUN = schedule.SUN,
					ScheduleType = schedule.ScheduleType
				});	
			}

			return new ApiResponse<List<ScheduleDto>>(true , "Success" , scheduleDto);
		}

		public async Task<ApiResponse<ScheduleDto>> AddSchedule(ScheduleDto scheduleDto)
		{
			if (scheduleDto == null)
				return new ApiResponse<ScheduleDto>(false , "Null DTO");

			Schedule schedule = new Schedule()
			{
				FRI = scheduleDto.FRI,
				MON = scheduleDto.MON,
				THU = scheduleDto.THU,
				WED = scheduleDto.WED,
				TUE = scheduleDto.TUE,
				SAT = scheduleDto.SAT,
				SUN = scheduleDto.SUN,
				ScheduleType = scheduleDto.ScheduleType
			};

			var result = await _repository.AddAsync(schedule);
			result = await _repository.SaveAsync();


			if (result)
			{
				scheduleDto.Id = schedule.Id;
				return new ApiResponse<ScheduleDto>(true , "Added Successfully" , scheduleDto);
			}

			return new ApiResponse<ScheduleDto>(false , "There is problems in database");
		}


		public async Task<ApiResponse<ScheduleDto>> RemoveSchedule(int id)
		{
			var schedule = await _repository.GetById(id);

			if (schedule == null)
				return new ApiResponse<ScheduleDto>(false , "There is no schedule with this id");

			ScheduleDto scheduleDto = new ScheduleDto()
			{
				Id = schedule.Id,
				FRI = schedule.FRI,
				MON = schedule.MON,
				THU = schedule.THU,
				WED = schedule.WED,
				TUE = schedule.TUE,
				SAT = schedule.SAT,
				SUN = schedule.SUN,
				ScheduleType = schedule.ScheduleType
			};

			var result = _repository.Remove(schedule);
			result = await _repository.SaveAsync();

			if (result) 
				return new ApiResponse<ScheduleDto>(true , "Removed Successfully" , scheduleDto);

			return new ApiResponse<ScheduleDto>(false , "There is a problems in the database");
		}

		public async Task<ApiResponse<bool>> UpdateSchedule(ScheduleDto scheduleDto)
		{
			var schedule= await _repository.GetById(scheduleDto.Id);

			if(schedule == null)
				return new ApiResponse<bool>(false , "There is no schedule with this id" , false);

			schedule.FRI = scheduleDto.FRI;
			schedule.MON = scheduleDto.MON;
			schedule.THU = scheduleDto.THU;
			schedule.WED = scheduleDto.WED;
			schedule.TUE = scheduleDto.TUE;
			schedule.SAT = scheduleDto.SAT;
			schedule.SUN = scheduleDto.SUN;
			schedule.ScheduleType = scheduleDto.ScheduleType;


			var result = _repository.Update(schedule);
			result = await _repository.SaveAsync();

			if (result)
				return new ApiResponse<bool>(true , "Updated Successfully" , true);

			return new ApiResponse<bool>(false, "There is a problems in the database");
		}
	}
}
