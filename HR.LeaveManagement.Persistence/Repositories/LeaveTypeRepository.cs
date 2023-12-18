using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories {
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository {
        public LeaveTypeRepository(HRDatabaseContext context) : base(context) {}

        public async Task<bool> IsUniqueLeaveType(string name) {
            var leaveType = await _context.LeaveType.AnyAsync(q => q.Name == name);
            if (leaveType)
                return false;
            else return true;
        }
    }
}
