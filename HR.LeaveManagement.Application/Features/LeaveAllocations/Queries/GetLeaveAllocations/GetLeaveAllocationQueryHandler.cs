using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations {
    internal class GetLeaveAllocationQueryHandler : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>> {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken) {
            var leaveAllocations = await _leaveAllocationRepository.GetAllAsync();
            return _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
        }
    }
}
