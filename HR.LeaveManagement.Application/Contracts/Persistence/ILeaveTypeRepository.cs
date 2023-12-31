﻿using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType> {
    public Task<bool> IsUniqueLeaveType(string name);
}
