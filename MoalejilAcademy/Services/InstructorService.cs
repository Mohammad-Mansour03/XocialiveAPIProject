using Microsoft.EntityFrameworkCore;
using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.IServices;
using MoalejilAcademy.Models;
using MoalejilAcademy.Repository;

namespace MoalejilAcademy.Services
{
	public class InstructorService : IInstructorService
	{
		private readonly IGenericRepository<Instructor> _repository;

		public InstructorService(IGenericRepository<Instructor> repository)
		{
			_repository = repository;
		}

		public async Task<ApiResponse<List<InstructorDto>>> GetAll()
		{
			var instructors = await _repository.GetAllAsync(x => x.Include(o => o.Office));

			List<InstructorDto> instructorDtos = new List<InstructorDto>();

			foreach (var instructor in instructors) 
			{
				instructorDtos.Add
					(
						new InstructorDto
						{
							Id = instructor.Id,
							FName = instructor.FName,
							LName = instructor.LName,
							OfficeId = instructor.OfficeId,
							OfficeName = instructor.Office?.OfficeName ??"There is no Office"
						}
					);
			}

			return new ApiResponse<List<InstructorDto>>(true , "Success" , instructorDtos);
		}

		public async Task<ApiResponse<InstructorDto>> GetById(int id)
		{
			var instructor = await _repository.GetById(id);

			if(instructor == null)
			    return new ApiResponse<InstructorDto>(false, "There is no Instructor have this id");
			

			return new ApiResponse<InstructorDto>(true , "Succes" , new InstructorDto 
			{
				Id=instructor.Id,
				FName=instructor.FName,
				LName= instructor.LName,
				OfficeId=instructor.OfficeId
			});
		}

		public async Task<ApiResponse<InstructorDto>> Add(InstructorDto instructorDto)
		{
			if (instructorDto == null)
				return new ApiResponse<InstructorDto>(false , "Null Dto");

			Instructor instructor = new Instructor
			{
				FName = instructorDto.FName,
				LName = instructorDto.LName,
				OfficeId = instructorDto.OfficeId == 0 ? null : instructorDto.OfficeId
			};

			var result = await _repository.AddAsync(instructor);
			
			result = await _repository.SaveAsync();
			

			instructorDto.Id = instructor.Id;
			
			if(result)
				return new ApiResponse<InstructorDto>(true , "Added Successfully" , instructorDto); 

			return new ApiResponse<InstructorDto>(false , "There is problems on database");

		}

		public async Task<ApiResponse<InstructorDto>> Delete(int id)
		{
			var instructor = await _repository.GetById(id);
			
			if(instructor == null)
				return new ApiResponse<InstructorDto>(false , "There is no instructor have this id");

			var result  = _repository.Remove(instructor);
			result = await _repository.SaveAsync();

			InstructorDto instructorDto = new InstructorDto
			{
				Id = instructor.Id,
				FName = instructor.FName,
				LName = instructor.LName,
				OfficeId = instructor.OfficeId
			};
			if(result)
				return new ApiResponse<InstructorDto>(true, "Deleted Successfully", instructorDto);

			return new ApiResponse<InstructorDto>(false, "There is problems on database");
		}

		public async Task<ApiResponse<bool>> Update(InstructorDto instructorDto)
		{
			if (instructorDto == null)
				return new ApiResponse<bool>(false , "Null Dto");

			var instructor = await _repository.GetById(instructorDto.Id);

			if (instructor == null)
				return new ApiResponse<bool>(false, "There is no instructor have this id");

			instructor.FName = instructorDto.FName;
			instructor.LName = instructorDto.LName;
			instructor.OfficeId = instructorDto.OfficeId == 0 ? null : instructorDto.OfficeId;

			var result = _repository.Update(instructor);
			result = await _repository.SaveAsync();

			if (result)
				return new ApiResponse<bool>(true, "Updated successfully", true);

			return new ApiResponse<bool>(false, "There is problem on database", false);
		}
	}
}
