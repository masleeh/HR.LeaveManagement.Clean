﻿using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations {
    public class GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>> {
        
    }
}
