using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest> {
    Task<LeaveRequest> GetLeaveRequestDetails(int id);
    Task<List<LeaveRequest>> GetAllLeaveRequestListWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestsByUserId(string userId);
}
