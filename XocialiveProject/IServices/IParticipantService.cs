using XocialiveProject.Data.DTO;
using XocialiveProject.Models;

namespace XocialiveProject.IServices
{
	public interface IParticipantService
	{
		Task<ApiResponse<List<ParticipantDto>>> GetAllParticipant(string? search,
			string? orderBy, bool descSort = false , int ? page = null , int ?pageSize = null);

		Task<ApiResponse<List<ParticipantDto>>> GetAll();
		Task<ApiResponse<ParticipantDto>> GetParticipant(int id);
		Task<ApiResponse<List<IndividualsDto>>> GetIndividuals();
		Task<ApiResponse<ParticipantAndCourses>> GetParticipantWithCourses(int participantId);
		Task<ApiResponse<string>> GetTotalSections(int participantId);
		Task<ApiResponse<List<TotalSectionsPerParticipant>>> TotalSectionsPerParticipant();
		Task<ApiResponse<List<ParticipantDto>>> ParticipantWithMaxEnrollments();
		Task<ApiResponse<List<Partic_Indiv_Copo>>> GetParticipantIndividualOrCopor();
	}
}
