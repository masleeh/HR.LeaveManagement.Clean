using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories {
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository {
        private readonly HRDatabaseContext _context;

        public LeaveRequestRepository(HRDatabaseContext context) : base(context) {
            this._context = context;
        }

        public async Task<List<LeaveRequest>> GetAllLeaveRequestListWithDetails() {
            var leaveRequests = await _context.LeaveRequest
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestDetails(int id) {
            var leaveRequest = await _context.LeaveRequest
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);
            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsByUserId(string userId) {
            var leaveRequests = await _context.LeaveRequest
                .Include(q => q.LeaveType)
                .Where(q => q.RequestingEmployeeId == userId)
                .ToListAsync();
            return leaveRequests;
        }
    }
}
