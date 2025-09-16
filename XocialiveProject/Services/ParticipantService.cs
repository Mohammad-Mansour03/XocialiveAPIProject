using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Models;
using XocialiveProject.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace XocialiveProject.Services
{
	public class ParticipantService : IParticipantService
	{
		private readonly IGenericRepository<Particpant> _repository;

		public ParticipantService(IGenericRepository<Particpant> repository)
		{
			_repository = repository;
		}


		private Expression<Func<Particpant , bool>> ? Filter(string? search) 
		{
			Expression<Func<Particpant, bool>>? filter = null;

			if (search != null)
				filter = p => p.FName.ToLower() == search.ToLower()
				|| p.LName.ToLower() == search.ToLower();

			return filter;
		}

		private Func<IQueryable<Particpant>, IOrderedQueryable<Particpant>>? Order 
			(string ?orderBy , bool descSort)
		{
			Func<IQueryable<Particpant>, IOrderedQueryable<Particpant>>? ordering = null;

			if (orderBy != null)
			{
				ordering = q =>
				{
					if (orderBy.ToLower() == "lname")
						return descSort ? q.OrderByDescending(x => x.LName) : q.OrderBy(x => x.LName);

					if (orderBy.ToLower() == "fname")
						return descSort ? q.OrderByDescending(x => x.FName) : q.OrderBy(x => x.FName);

					return descSort ? q.OrderByDescending(x => x.Id) : q.OrderBy(x => x.Id);
				};
			}
			return ordering;
		}

		public async Task<ApiResponse<List<ParticipantDto>>> GetAllParticipant(string? search,
			string? orderBy, bool descSort = false , int? page = null, int? pageSize = null)
		{
			
			var filter = Filter(search);
			
			var ordering = Order(orderBy, descSort);

			var skip = (page - 1) * pageSize;
			
			var particpants = await _repository.GetData(filter , 
				ordering , skip , pageSize);

			List<ParticipantDto> result = new List<ParticipantDto>();

			foreach (var participant in particpants) 
			{
				result.Add(new ParticipantDto
				{
					Id = participant.Id,
					FName = participant.FName,
					LName = participant.LName
				});
			}

			return new ApiResponse<List<ParticipantDto>>(true , "Success" , result);
		}

		public async Task<ApiResponse<ParticipantDto>> GetParticipant(int id)
		{
			var participant = await _repository.GetById(id);

			if (participant == null)
				return new ApiResponse<ParticipantDto>(false , "There is no Participant have this id");

			return new ApiResponse<ParticipantDto>(true , "Success", new ParticipantDto
			{
				LName = participant.LName,
				FName = participant.FName,
				Id = participant.Id
			});

		}

		public async Task<ApiResponse<ParticipantAndCourses>> GetParticipantWithCourses(int participantId)
		{
			var participant = await _repository.GetById(participantId,
				q => q.Include(s => s.Sections).ThenInclude(c => c.Course));

			if (participant == null)
				return new ApiResponse<ParticipantAndCourses>(false, "There is no any participant " +
					"have this id");

			if (!participant.Sections.Any())
				return new ApiResponse<ParticipantAndCourses>(true, "There is no any courses enrolled to it", new ParticipantAndCourses()
				{
					ParticipantName = participant.FName + " " + participant.LName
				});

			var result = new ParticipantAndCourses
			{
				ParticipantName = participant.FName + " " + participant.LName,
				Courses = participant.Sections
				.Select
				(
					x => new CourseDto
					{
						Id = x.Course.Id,
						CourseName = x.Course.CourseName,
						HoursToComplete = x.Course.HoursToComplete,
						Price = x.Course.Price,
					}
				).ToList(),
			};

			return new ApiResponse<ParticipantAndCourses>(true , "Success" , result);
		}

		public async Task<ApiResponse<string>> GetTotalSections(int participantId) 
		{
			var participant = await _repository.GetById(participantId
				, q => q.Include(x => x.Sections).ThenInclude(c => c.Course));

			if (participant == null)
				return new ApiResponse<string>(false, "There is no any participant have this id");

			int numOfSections = 0;

			if (!participant.Sections.Any())
				return new ApiResponse<string>(true, "No any section",
					$"Number of sections was {numOfSections}");

			numOfSections = participant.Sections.DistinctBy(x => x.Course.Id).Count();

			return new ApiResponse<string>(true, " ", $"Number of Courses was {numOfSections}");
		}

		public async Task<ApiResponse<List<IndividualsDto>>> GetIndividuals()
		{
			var particpants = await _repository.GetAllAsync();

			var individuals = particpants.OfType<Individuals>()
				.Where(x => x.IsIntern).Select
				(
				x =>
					new IndividualsDto 
					{
						Id = x.Id,
						FName = x.FName,
						LName = x.LName,
						University = x.University,
						IsIntern = x.IsIntern,
						YearOfGraduation = x.YearOfGraduation
					}
				).ToList();

			if (individuals == null)
				return new ApiResponse<List<IndividualsDto>>(false, "There is no any intern");

			return new ApiResponse<List<IndividualsDto>>(true, "Success", individuals);
		}

		public async Task<ApiResponse<List<TotalSectionsPerParticipant>>> TotalSectionsPerParticipant()
		{
			var participants = await _repository.GetAllAsync
				(
					q => q.Include(x => x.Sections)
				);

			if (participants == null)
				return new ApiResponse<List<TotalSectionsPerParticipant>>(false, "The List is null");

			if (participants.Count == 0)
				return new ApiResponse<List<TotalSectionsPerParticipant>>(true, "There is no any participant");

			var result = participants.Select
				(
					x => new TotalSectionsPerParticipant
					{
						ParticipantId = x.Id,
						ParticipantName = x.FName + " " + x.LName,
						TotalSections = x.Sections.Count
					}
				).ToList();

			if (result == null)
				return new ApiResponse<List<TotalSectionsPerParticipant>>(false,
					"There is something wrong on the query");

			return new ApiResponse<List<TotalSectionsPerParticipant>>(true, "Success", result);
		}

		public async Task<ApiResponse<List<ParticipantDto>>> ParticipantWithMaxEnrollments()
		{
			var participants = await _repository.GetAllAsync(x => x.Include(s => s.Sections));

			if (participants == null)
				return new ApiResponse<List<ParticipantDto>>(false, "The Participants null");

			if (participants.Count == 0)
				return new ApiResponse<List<ParticipantDto>>(true, "The participants empty");

			int maxEnrollments = participants.Max(x => x.Sections.Count);

			var result = participants.Where(x => x.Sections.Count == maxEnrollments)
				.Select
				(
					x => new ParticipantDto
					{
						Id = x.Id,
						FName = x.FName,
						LName = x.LName,
					}
				).ToList();

			if (result == null)
				return new ApiResponse<List<ParticipantDto>>(false, "Final Result was null");

			return new ApiResponse<List<ParticipantDto>>(true , "Success" , result);
		}

		public async Task<ApiResponse<List<Partic_Indiv_Copo>>> GetParticipantIndividualOrCopor()
		{
			var participant = await _repository.GetAllAsync();

			if (participant == null)
				return new ApiResponse<List<Partic_Indiv_Copo>>(false, "The Participants was null");

			var result = participant.Select
				(
					x => new Partic_Indiv_Copo
					{
						Id = x.Id,
						FName = x.FName,
						LName = x.LName,
						//Organization = x is Coporates ? ((Coporates)x).Company : ((Individuals)x).University
						Organization = x.GetType() == typeof(Coporates) ? ((Coporates)x).Company : 
						((Individuals)x).University
					}
				).ToList();

			if (result == null)
				return new ApiResponse<List<Partic_Indiv_Copo>>(false, "The final result was null");

			return new ApiResponse<List<Partic_Indiv_Copo>>(true, "Success", result);
		}
	}
}
