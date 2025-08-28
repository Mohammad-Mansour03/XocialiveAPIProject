using Microsoft.EntityFrameworkCore;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Models;
using XocialiveProject.Repository;

namespace XocialiveProject.Services
{
	public class SectionService :ISectionService
	{
		private readonly IGenericRepository<Section> _repository;

		public SectionService(IGenericRepository<Section> _repository)
		{
			this._repository = _repository;
		}

		public async Task<ApiResponse<List<SectionDto>>> GetAllSections()
		{
			var sections =await  _repository.GetAllAsync();

			List<SectionDto> result = new List<SectionDto>();

			foreach (var section in sections) 
			{
				result.Add(new SectionDto
				{
					Id = section.Id,
					CourseId = section.CourseId,
					DateSlot = section.DateSlot!,
					InstructorId = section.InstructorId,
					ScheduleId = section.ScheduleId,
					SectionName = section.SectionName,
					TimeSlot = section.TimeSlot!
				});
			}

			return new ApiResponse<List<SectionDto>>(true , "Success" , result);
		}

		public async Task<ApiResponse<SectionDto>> GetSction(int sectionId)
		{
			var section = await _repository.GetById(sectionId);

			if (section == null)
				return new ApiResponse<SectionDto>(false , "There is no section with this id");

			return new ApiResponse<SectionDto>(true , "Success" , new SectionDto
			{
				Id = section.Id,
				CourseId = section.CourseId,
				DateSlot = section.DateSlot!,
				InstructorId = section.InstructorId,
				ScheduleId = section.ScheduleId,
				SectionName = section.SectionName,
				TimeSlot = section.TimeSlot!
			});
		}

		public async Task<ApiResponse<SectionDto>> AddSection(SectionDto sectionDto)
		{
			if (sectionDto == null)
				return new ApiResponse<SectionDto>(false , "Null Dto");

			Section section = new Section()
			{
				Id = sectionDto.Id,
				CourseId = sectionDto.CourseId,
				DateSlot = sectionDto.DateSlot!,
				InstructorId = sectionDto.InstructorId == 0 ? null : sectionDto.InstructorId,
				ScheduleId = sectionDto.ScheduleId,
				SectionName = sectionDto.SectionName,
				TimeSlot = sectionDto.TimeSlot
			};

			var result = await _repository.AddAsync(section);
			result = await _repository.SaveAsync();

			if(result)
			{
				sectionDto.Id = section.Id;
				return new ApiResponse<SectionDto>(true , "Added Successfully", sectionDto);
			}
			return new ApiResponse<SectionDto>(false , "Problem in Database" );
		}

		public async Task<ApiResponse<SectionDto>> RemoveSection(int sectionId)
		{
			var section = await _repository.GetById(sectionId);

			if (section == null)
				return new ApiResponse<SectionDto>(false , "There is no section with this id");

			var result =  _repository.Remove(section);
			result = await _repository.SaveAsync();

			if (result) 
			{
				return new ApiResponse<SectionDto>(true , "Removed Successfully", new SectionDto()
				{
					Id = section.Id,
					CourseId = section.CourseId,
					DateSlot = section.DateSlot!,
					InstructorId = section.InstructorId,
					ScheduleId = section.ScheduleId,
					SectionName = section.SectionName,
					TimeSlot = section.TimeSlot!
				});
			}

			return new ApiResponse<SectionDto>(false , "Problem in database");
		}

		public async Task<ApiResponse<bool>> UpdateSection(SectionDto sectionDto)
		{
			if (sectionDto == null)
				return new ApiResponse<bool>(false , "NULL Dto");

			Section ? section = await _repository.GetById(sectionDto.Id);

			if (section == null)
				return new ApiResponse<bool>(false, "There is no section with this id");


			section.Id = sectionDto.Id;
			section.CourseId = sectionDto.CourseId;
			section.DateSlot = sectionDto.DateSlot!;
			section.InstructorId = sectionDto.InstructorId;
			section.ScheduleId = sectionDto.ScheduleId;
			section.SectionName = sectionDto.SectionName;
			section.TimeSlot = sectionDto.TimeSlot;

			var result = await _repository.SaveAsync();
			
			if (result)
				return new ApiResponse<bool>(true , "Updated Success" , true);

			return new ApiResponse<bool>(false, "Problem in the database");
		}

		public async Task<ApiResponse<bool>> Enroll(int sectionId, int participantId)
		{
			var section = await _repository.GetById(sectionId
				, q => q.Include(p => p.Particpants)
				);

			if(section == null) 
				return new ApiResponse<bool>(false, "There is no section with this id");

			if (section.Particpants.Any(p => p.Id == participantId))
				return new ApiResponse<bool>(true , "The Participant already enroll to this section");

			var participant = await _repository.GetContext()
							.Set<Particpant>()
							.FirstOrDefaultAsync(x => x.Id == participantId);
			
			if(participant == null) 
				return new ApiResponse<bool>(false, "There is no participant with this id");

			section.Particpants.Add(participant);
			var result = await _repository.SaveAsync();

			if (result)
				return new ApiResponse<bool>(true, "The Participant added to the section" , true);
			
			return new ApiResponse<bool>(false, "There is database problems", false);
		}

		public async Task<ApiResponse<string>> UnEnroll(int sectionId, int participantId )
		{
			try
			{
				var section = await _repository.GetById(sectionId
					, q => q.Include(p => p.Particpants)
					);

				if (section == null)
					return new ApiResponse<string>(false, "There is no section with this id");

				var participant = await _repository.GetContext().Set<Particpant>()
					.FirstOrDefaultAsync(x => x.Id == participantId);

				if (participant == null)
					return new ApiResponse<string>(false, "There is no any participant with this id");
				

				if (!section.Particpants.Any(x => x.Id == participantId))
					return new ApiResponse<string>(true, "The Perticipant already not enrolled in this section");

				

				var result = section.Particpants.Remove(participant);
				result = await _repository.SaveAsync();

				if (result)
					return new ApiResponse<string>(true, "The Participant removed for the setion");
				
				return new ApiResponse<string>(false , "There is a problems in the database");
			}
			catch (Exception ex) 
			{
				 
				return new ApiResponse<string>(false , ex.ToString());
			}

		}

		public async Task<ApiResponse<SectionWithParticipant>> GetSectionWithParticipant(int sectionId)
		{
			try
			{
				string message = string.Empty;

				var section = await _repository.GetById(sectionId
					, q => q.Include(p => p.Particpants));

				if (section == null)
					return new ApiResponse<SectionWithParticipant>(false,
						"There is no section with this id");

				SectionWithParticipant sectionWithParticipant = new SectionWithParticipant()
				{
					SectionId = sectionId,
					SectionName = section.SectionName
				};


				if (!section.Particpants.Any())
				{
					sectionWithParticipant.Participants = new List<ParticipantDto>();

					return new ApiResponse<SectionWithParticipant>(true,
						"There is no any participant enrolled to this section" , sectionWithParticipant);
				}

				foreach (var p in section.Particpants)
				{
					sectionWithParticipant.Participants.Add
						(
							new ParticipantDto
							{
								FName = p.FName,
								LName = p.LName,
								Id = p.Id,
							}
						);
				}

				return new ApiResponse<SectionWithParticipant>(true, 
					"Section and There Participants", sectionWithParticipant);
			}
			catch (Exception ex) 
			{
				return new ApiResponse<SectionWithParticipant>(false , ex.Message);
			}

		}

		public async Task<ApiResponse<List<SectionInstructor>>> GetSectionInstructor()
		{
			var sections = await _repository.GetAllAsync(q => q.Include(i => i.Instructor));

			if (sections == null)
				return new ApiResponse<List<SectionInstructor>>(true, "There is no sections to retrieve");

			List<SectionInstructor> sectionInstructor = new List<SectionInstructor>();

			sectionInstructor =  sections.Select
				(
					x => new SectionInstructor
					{
						SectionName = x.SectionName,
						InstructorName = x.Instructor is null ? null : x.Instructor.FName + " " + x.Instructor.LName
					}

				).ToList();

			 return new ApiResponse<List<SectionInstructor>>(true, "Success" , sectionInstructor);
		}

		public async Task<ApiResponse<List<SectionWithCourseAndSchedule>>> GetSectionsWithCourseAndSchedule()
		{
			var sections = await _repository.GetAllAsync(q => q.Include(c => c.Course)
			.Include(s => s.Schedule));

			if (sections == null)
				return new ApiResponse<List<SectionWithCourseAndSchedule>>(false,
					"There is no any sections to retrieve"); 

			List<SectionWithCourseAndSchedule> sectionWithCourseAndSchedules = new List<SectionWithCourseAndSchedule>();

			sectionWithCourseAndSchedules = sections.Select
				(
					x => new SectionWithCourseAndSchedule
					{
						SectionName = x.SectionName,
						CourseName = x.Course.CourseName,
						ScheduleType = x.Schedule.ScheduleType.ToString(),
					}
				).ToList();

			return new ApiResponse<List<SectionWithCourseAndSchedule>>(true, "Success",
				sectionWithCourseAndSchedules);
		}

		public async Task<ApiResponse<List<SectionDto>>> GetSectionWithoutInstuctor() 
		{
			var sections = await _repository.GetAllAsync(q => q.Include(i => i.Instructor));

			if (sections == null)
				return new ApiResponse<List<SectionDto>>(false, "Null Sections");

			if (!sections.Any())
				return new ApiResponse<List<SectionDto>>(true, "The List was empty");

			var result =  sections.Where(x => x.Instructor == null)
				.Select
				(
					x => new SectionDto
					{
						Id = x.Id,
						SectionName = x.SectionName,
						InstructorId = x.InstructorId,
						CourseId = x.CourseId,
						DateSlot = x.DateSlot!,
						ScheduleId = x.ScheduleId,
						TimeSlot = x.TimeSlot!
					}
				).ToList();

			if (result == null)
				return new ApiResponse<List<SectionDto>>(false, "The final result null");

			return new ApiResponse<List<SectionDto>>(true , "Success" , result);
		}
	}

}
