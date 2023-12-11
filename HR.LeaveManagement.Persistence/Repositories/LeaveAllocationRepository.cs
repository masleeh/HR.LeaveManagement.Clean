using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories {
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository {
        public LeaveAllocationRepository(HRDatabaseContext context) : base(context) {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations) {
            await _context.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<bool> DoesAllocationExist(string userId, int leaveTypeId, int period) {
            return await _context.LeaveAllocation.AnyAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId && q.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetAllLeaveAllocations() {
            var leaveAllAllocations = await _context.LeaveAllocation.Include(q => q.LeaveType).ToListAsync();
            return leaveAllAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationByUserId(string userId) {
            var leaveUserAllocations = await _context.LeaveAllocation.Where(q => q.EmployeeId == userId).Include(q => q.LeaveType).ToListAsync();
            return leaveUserAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id) {
            var leaveAllocation = await _context.LeaveAllocation.Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.Id == id);
            return leaveAllocation;
        }

        public async Task<LeaveAllocation> GetUsersAllocation(string userId, int leaveTypeId) {
            var leaveAllocation = await _context.LeaveAllocation.Include(q => q.LeaveType).FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
            return leaveAllocation;
        }
    }
}
