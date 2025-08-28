using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using XocialiveProject.Data.DTO;
using XocialiveProject.IServices;
using XocialiveProject.Models;
using XocialiveProject.Repository;

namespace XocialiveProject.Services
{
	public class CourseService :ICourseService
	{
		private readonly IGenericRepository<Course> _repository;

		public CourseService(IGenericRepository<Course> _repository)
		{
			this._repository = _repository;
		}

		public async Task<ApiResponse<CourseDto>> GetCourse(int id)
		{
			var course = await _repository.GetById(id);

			if (course == null)
				return new ApiResponse<CourseDto>(false,"There is no course with this id");

			return new ApiResponse<CourseDto>(true ," " ,new CourseDto
			{
				Id = course.Id,
				Price = course.Price,
				HoursToComplete = course.HoursToComplete,
				CourseName = course.CourseName
			});
			
		}

		public async Task<ApiResponse<List<CourseDto>>> GetCourses()
		{
			var courses = await _repository.GetAllAsync();

			List<CourseDto> courseDtos = new List<CourseDto>();

			foreach (var course in courses) 
			{
				courseDtos.Add(
					new CourseDto
					{
						Id= course.Id,
						CourseName= course.CourseName,
						Price= course.Price,
						HoursToComplete= course.HoursToComplete
					}
					);
			}
			return new ApiResponse<List<CourseDto>>(true , " " , courseDtos);
		}

		public async Task<ApiResponse<CourseDto>> Add(CourseDto courseDto)
		{
			if (courseDto == null)
			{
				return new ApiResponse<CourseDto>(false , "The Dto Null");
			}

			Course course = new Course() 
			{
				//Id = courseDto.Id,
				CourseName = courseDto.CourseName,
				Price = courseDto.Price,
				HoursToComplete=courseDto.HoursToComplete 
			};

			var result = await _repository.AddAsync(course);
			result = await _repository.SaveAsync();

			if (result)
			{
				courseDto.Id = course.Id;

				return new ApiResponse<CourseDto>(true, "Course Added", courseDto);
			}

			return new ApiResponse<CourseDto>(false, "There is a problem in the database");
		}

		public async Task<ApiResponse<CourseDto>> Delete(int id)
		{
			var course = await _repository.GetById(id);
			
			if (course == null)
				return new ApiResponse<CourseDto>(false, "There is no course with this id");

			CourseDto ?courseDto = new CourseDto
			{
				Id = course.Id,
				CourseName = course.CourseName,
				HoursToComplete = course.HoursToComplete,
				Price = course.Price
			};

			var result = _repository.Remove(course);
			result = await _repository.SaveAsync();

			if(result) 
				return new ApiResponse<CourseDto>(true, "Course Removed", courseDto);

			return new ApiResponse<CourseDto>(false , "Problems in the database");
		}


		public async Task<ApiResponse<bool>> Update(CourseDto course)
		{
			if (course == null)
				return new ApiResponse<bool>(false , "The Dto Null");

			var trackCourse = await _repository.GetById(course.Id);

			if(trackCourse == null)
				return new ApiResponse<bool>(false, "There is no course with this id");

			trackCourse.CourseName = course.CourseName;
			trackCourse.HoursToComplete = course.HoursToComplete;
			trackCourse.Price = course.Price;
			
			var result =  _repository.Update(trackCourse);
			result = await _repository.SaveAsync();
			
			if(result)
				return new ApiResponse<bool>(true , "The Course Updated" , true);

			return new ApiResponse<bool>(false, "Problems in the database");
		}

		public async Task<ApiResponse<List<AllCoursesWithTotalNumberOfParticipant>>> GetAllCoursesWithTotalNumOfPartic()
		{
			var courses = await _repository.GetAll
				(
					x => x.Include(s => s.Sections).ThenInclude(p => p.Particpants)
				);

			if (courses == null)
				return new ApiResponse<List<AllCoursesWithTotalNumberOfParticipant>>(false,
					"The Courses was null");

			if (courses.Count == 0)
				return new ApiResponse<List<AllCoursesWithTotalNumberOfParticipant>>(true
					, "There is no any course");

			var result = courses.Select
				(
					x => new AllCoursesWithTotalNumberOfParticipant
					{
						CourseId = x.Id,
						CourseName = x.CourseName,
						TotalParticipant = x.Sections.SelectMany(x => x.Particpants).Count()
					}	
				).ToList();

			
			return new ApiResponse<List<AllCoursesWithTotalNumberOfParticipant>>(true
				, "Success" , result);

		}

		public async Task<ApiResponse<List<AveragePerHours>>> GetAveragePerHours()
		{
			var courses = await _repository.GetAll();

			if (courses == null)
				return new ApiResponse<List<AveragePerHours>>(false, "The courses are null");

			if (!courses.Any())
				return new ApiResponse<List<AveragePerHours>>(true, "The Courses empty");

			var result = courses
				.GroupBy(x => x.HoursToComplete)
				.Select
				(
					x => new AveragePerHours
					{
						HoursToComplete = x.Key,
						AveragePrice = x.Average(p => p.Price)
					}
				).ToList();

			return new ApiResponse<List<AveragePerHours>>(true, "Success", result);
		}
	}
}
