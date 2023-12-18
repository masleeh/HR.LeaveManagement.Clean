using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetAllLeaveRequests {
    public class GetAllLeaveRequestsQueryHandler : IRequestHandler<GetAllLeaveRequestsQuery, List<LeaveRequestListDto>> {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetAllLeaveRequestsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            this._mapper = mapper;
            this._leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken) {
            var leaveRequests = await _leaveRequestRepository.GetAllAsync();
            return _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
        }
    }
}
