using System.Linq.Expressions;
using MoalejilAcademy.Data.DTO;
using MoalejilAcademy.IServices;
using MoalejilAcademy.Models;
using MoalejilAcademy.Repository;

namespace MoalejilAcademy.Services
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

		private Expression<Func<Office , bool>> ? Filter(string ? search) 
		{
			Expression<Func<Office , bool>> ? filter = null;

			if (search != null)
				filter = o => o.OfficeName.ToLower() == search.ToLower() ||
				o.OfficeLocation.ToLower() == search.ToLower();

			return filter;
		}

		private Func<IQueryable<Office> , IOrderedQueryable<Office>> ? Order (string ? orderBy , bool descSort) 
		{
			Func<IQueryable<Office> , IOrderedQueryable<Office>> order = null;

			if(orderBy != null)
			{
				order = o =>
				{
					if (orderBy.ToLower() == "officename")
						return  descSort ? o.OrderByDescending(x => x.OfficeName) : o.OrderBy(x => x.OfficeName);

					if (orderBy.ToLower() == "officelocation")
						return descSort ? o.OrderByDescending(x => x.OfficeLocation) : o.OrderBy(x => x.OfficeLocation);

					return descSort ? o.OrderByDescending(x => x.Id) : o.OrderBy(x => x.Id);
				};
			}

			return order;
		}

		public async Task<ApiResponse<List<OfficeDto>>> FilteredOffices(string? search, string? orderBy, 
			bool descSort = false, int? page = null, int? pageSize = null)
		{
			var filter = Filter(search);
			var order = Order(orderBy, descSort);

			var skip = (page - 1) * pageSize;

			var offices = await _repository.GetData
				(
					filter, order, skip, pageSize
				);

			List<OfficeDto> result = new List<OfficeDto>();

			if (offices.Count > 0)
			{
				foreach (var o in offices)
				{
					result.Add(new OfficeDto
					{
						Id = o.Id,
						OfficeName = o.OfficeName,
						OfficeLocation = o.OfficeLocation,
					});
				}

				return new ApiResponse<List<OfficeDto>>(true, " ", result);
			}

			return new ApiResponse<List<OfficeDto>>(false, "There is no any office");
		}
	}
}
