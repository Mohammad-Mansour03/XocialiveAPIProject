using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Models;
using XocialiveProject.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XocialiveProject.Services
{
	public class CoporateService :ICoporateService
	{
		private readonly IGenericRepository<Coporates> _repository;

		public CoporateService(IGenericRepository<Coporates> repository)
		{
			_repository = repository;
		}

		public async Task<ApiResponse<CoporateDto>> GetCoporate(int id)
		{
			var coporate = await _repository.GetById(id);

			if (coporate == null)
				return new ApiResponse<CoporateDto>(false , "The Id was wrong");

			return new ApiResponse<CoporateDto>(true, "The User Return Successfully",
				new CoporateDto
				{
					Id = coporate.Id,
					FName = coporate.FName,
					LName = coporate.LName,
					Company = coporate.Company,
					JobTitle = coporate.JobTitle
				});
		}

		public async Task<ApiResponse<List<CoporateDto>>> GetCoporatesAsync()
		{
			var coporates = await _repository.GetAllAsync();

			List<CoporateDto> coporateDtos = new List<CoporateDto>();

			foreach (var coporate in coporates) 
			{
				coporateDtos.Add
					(
						new CoporateDto
						{

							Id = coporate.Id,
							FName = coporate.FName,
							LName = coporate.LName,
							Company = coporate.Company,
							JobTitle = coporate.JobTitle
						}
					);
			}

			return new ApiResponse<List<CoporateDto>>(true , "The List Return success" , coporateDtos);
		}

		public async Task<ApiResponse<CoporateDto>> AddCoporate(CoporateDto cooporatesDto)
		{
			if (cooporatesDto == null)
				return new ApiResponse<CoporateDto>(false , "The Dto Was null");

			Coporates coporates = new Coporates()
			{

				Id = cooporatesDto.Id,
				FName = cooporatesDto.FName,
				LName = cooporatesDto.LName,
				Company = cooporatesDto.Company,
				JobTitle = cooporatesDto.JobTitle
			};

			var result = await _repository.AddAsync(coporates);
			await _repository.SaveAsync();

			if (result) 
			{
				cooporatesDto.Id = coporates.Id;
				return new ApiResponse<CoporateDto>(true , "The Coporate Added" , cooporatesDto);
			}

			return new ApiResponse<CoporateDto>(false , "There is a problem while added to the database");
		}

		public async Task<ApiResponse<CoporateDto>> Remove(int id)
		{
			var coporate = await _repository.GetById(id);

			if (coporate == null)
				return new ApiResponse<CoporateDto>(false , "There is no Coporate have this id");

			
			var result = _repository.Remove(coporate);
			await _repository.SaveAsync();

			if (result) 
			{
				return new ApiResponse<CoporateDto>(true , $"The Coporate with id {id} was removed" 
					, new CoporateDto()
				{
					Id = coporate.Id,
					FName = coporate.FName,
					LName = coporate.LName,
					Company = coporate.Company,
					JobTitle = coporate.JobTitle
				});
			}
			return new ApiResponse<CoporateDto>(false , "There is an internal problem during removed " +
				"it from the database");
		}

		public async Task<ApiResponse<bool>> Update(CoporateDto cooporatesDto)
		{
			if (cooporatesDto == null)
				return new ApiResponse<bool>(false , "The Dto was null");

			var corporate = await _repository.GetById(cooporatesDto.Id);

			if(corporate == null)
				return new ApiResponse<bool>(false, "There is no Coporate with this id");

			corporate.Id = cooporatesDto.Id;
			corporate.LName = cooporatesDto.LName;
			corporate.FName = cooporatesDto.FName;
			corporate.Company = cooporatesDto.Company;
			corporate.JobTitle = cooporatesDto.JobTitle;
		

		//	var result = _repository.Update(corporate);
			await _repository.SaveAsync();

			return new ApiResponse<bool>(true , "The Coporate Updated Successfully", true);
		}
	}
}
