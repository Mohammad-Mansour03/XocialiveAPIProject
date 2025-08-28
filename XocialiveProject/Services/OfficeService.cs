using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Models;
using XocialiveProject.Repository;

namespace XocialiveProject.Services
{
	public class OfficeService : IOfficeService
	{
		private readonly IGenericRepository<Office> _repository;

		public OfficeService(IGenericRepository<Office> repository)
		{
			_repository = repository;
		}

		public async Task<ApiResponse<List<OfficeDto>>> GetAllOffices()
		{
			var offices = await _repository.GetAllAsync();

			List<OfficeDto> officeDtos = new List<OfficeDto>();

			foreach (var office in offices) 
			{
				officeDtos.Add(new OfficeDto
				{
					Id = office.Id,
					OfficeLocation = office.OfficeLocation,
					OfficeName = office.OfficeName
				});
			}

			return new ApiResponse<List<OfficeDto>>(true , "Success" , officeDtos);
		}

		public async Task<ApiResponse<OfficeDto>> GetOffice(int id)
		{
			var office =await  _repository.GetById(id);

			if (office == null)
				return new ApiResponse<OfficeDto>(false , "There is no Office have this id");

			return new ApiResponse<OfficeDto>(true , "Success",new OfficeDto
			{
				Id = office.Id,
				OfficeLocation = office.OfficeLocation,
				OfficeName = office.OfficeName
			});
		}

		public async Task<ApiResponse<OfficeDto>> AddOffice(OfficeDto? officeDto)
		{
			if(officeDto == null) 
				return new ApiResponse<OfficeDto>(false , "Null DTO");

			Office office = new Office
			{
				OfficeLocation = officeDto.OfficeLocation,
				OfficeName = officeDto.OfficeName
			};

			bool result = await _repository.AddAsync(office);
			result = await _repository.SaveAsync();

			if (result)
			{
				officeDto.Id = office.Id;
				return new ApiResponse<OfficeDto>(true , "Added Successfully" , officeDto);
			}

			return new ApiResponse<OfficeDto>(false, "There is a problems in the database");
		}

		public async Task<ApiResponse<OfficeDto>> RemoveOffice(int id)
		{
			var office = await _repository.GetById(id);
			
			if(office == null)
				return new ApiResponse<OfficeDto>(false , "There is no office with this id");

			var result = _repository.Remove(office);
			
			result = await _repository.SaveAsync();

			if (result)
			{
				OfficeDto officeDto = new OfficeDto
				{
					Id = office.Id,
					OfficeLocation = office.OfficeLocation,
					OfficeName = office.OfficeName
				};

				return new ApiResponse<OfficeDto>(true , "Removed Successfully" , officeDto);
			}

			return new ApiResponse<OfficeDto>(false, "There is a problems in the database");
		}

		public async Task<ApiResponse<bool>> UpdateOffice(OfficeDto officeDto)
		{
			var office = await _repository.GetById(officeDto.Id);

			if (office == null)
				return new ApiResponse<bool>(false , "There is no office with this id");

			office.OfficeName = officeDto.OfficeName;
			office.OfficeLocation = officeDto.OfficeLocation;

			var result = _repository.Update(office);
			result = await _repository.SaveAsync();

			if (result)
				return new ApiResponse<bool>(true, "Updated Successfully" , true);

			return new ApiResponse<bool>(false, "There is a problem in the database");
		}
	}
}
