using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Models;
using XocialiveProject.Repository;

namespace XocialiveProject.Services
{
	public class IndividualService : IIndividualService
	{
		private readonly IGenericRepository<Individuals> _repository;
		public IndividualService(IGenericRepository<Individuals> repository)
		{
			_repository = repository;
		}
		public async Task<ApiResponse<IndividualsDto>> GetIndividualAsync(int id)
		{
			var individual = await _repository.GetById(id);

			if (individual == null)
				return new ApiResponse<IndividualsDto>(false , "There is no Individual with this id");

			return new ApiResponse<IndividualsDto>(true , "Success" , new IndividualsDto
			{
				FName = individual.FName,
				LName = individual.LName,
				Id = individual.Id,
				IsIntern = individual.IsIntern,
				University = individual.University,
				YearOfGraduation = individual.YearOfGraduation
			});
		}

		public async Task<ApiResponse<List<IndividualsDto>>> GetIndividualsAsync()
		{
			var individuals = await _repository.GetAllAsync();

			List<IndividualsDto> individualsDtos = new List<IndividualsDto>();

			foreach (var individual in individuals) 
			{
				individualsDtos.Add
					(
						new IndividualsDto
						{
							FName = individual.FName,
							LName = individual.LName,
							Id = individual.Id,
							IsIntern = individual.IsIntern,
							University = individual.University,
							YearOfGraduation = individual.YearOfGraduation
						}
					);
			}

			return new ApiResponse<List<IndividualsDto>> (true , "Succes" , individualsDtos);
		}

		public async Task<ApiResponse<IndividualsDto>> Add(IndividualsDto individualsDto)
		{
			if (individualsDto == null)
				return new ApiResponse<IndividualsDto>(false , "The Dto Was null");

			Individuals individual = new Individuals
			{
				FName = individualsDto.FName,
				LName = individualsDto.LName,
				IsIntern = individualsDto.IsIntern,
				University = individualsDto.University,
				YearOfGraduation = individualsDto.YearOfGraduation
			};

			var result = await _repository.AddAsync(individual);
			result = await _repository.SaveAsync();

			if (result) 
			{
				individualsDto.Id = individual.Id;
				return new ApiResponse<IndividualsDto>(true , "Added Successfully" , individualsDto);
			}

			return new ApiResponse<IndividualsDto>(false, "There is problems in database");
		}

		public async Task<ApiResponse<IndividualsDto>> Delete(int id)
		{
			var individual = await _repository.GetById(id);

			if (individual == null)
				return new ApiResponse<IndividualsDto>(false , "There is no Individual have this id");
			
			var result = _repository.Remove(individual);
			result = await _repository.SaveAsync();

			if (result) 
			{
				return new ApiResponse<IndividualsDto>(true , "Deleted Successfully" , new IndividualsDto
				{
					FName = individual.FName,
					LName = individual.LName,
					Id = individual.Id,
					IsIntern = individual.IsIntern,
					University = individual.University,
					YearOfGraduation = individual.YearOfGraduation
				});
			}

			return new ApiResponse<IndividualsDto>(false , "There is a problems in database");
		}

		public async Task<ApiResponse<bool>> Update(IndividualsDto individualsDto)
		{
			if (individualsDto == null)
				return new ApiResponse<bool>(false, "The Dto Was null");
			

			var individual = await _repository.GetById(individualsDto.Id);

			if (individual == null)
				return new ApiResponse<bool>(false, "There is no Individual have this id");


			individual.FName = individualsDto.FName;
			individual.LName = individualsDto.LName;
			individual.Id = individualsDto.Id;
			individual.IsIntern = individualsDto.IsIntern;
			individual.University = individualsDto.University;
			individual.YearOfGraduation = individualsDto.YearOfGraduation;

			var result = await _repository.SaveAsync();

			if (result)
				return new ApiResponse<bool>(true, "Updated Success", true);

			return new ApiResponse<bool>(false, "There is a problems in database");
		}
	}
}
