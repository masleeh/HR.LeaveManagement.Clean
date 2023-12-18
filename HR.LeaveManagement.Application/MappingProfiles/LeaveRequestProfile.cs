using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles {
    public class LeaveRequestProfile : Profile {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestListDto>();
            CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        }
    }
}
