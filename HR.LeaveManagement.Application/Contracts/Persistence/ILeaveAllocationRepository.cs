using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation> {
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>> GetAllLeaveAllocations();
    Task<List<LeaveAllocation>> GetLeaveAllocationByUserId(string userId);
    Task<bool> DoesAllocationExist(string userId, int leaveTypeId, int period);
    Task AddAllocations(List<LeaveAllocation> allocations);
    Task<LeaveAllocation> GetUsersAllocation(string userId, int leaveTypeId);
}
