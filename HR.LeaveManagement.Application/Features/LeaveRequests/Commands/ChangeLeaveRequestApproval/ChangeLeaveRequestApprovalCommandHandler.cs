using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval {
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit> {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _logger;

        public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender, IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger) {
            this._mapper = mapper;
            this._leaveRequestRepository = leaveRequestRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._emailSender = emailSender;
            this._logger = logger;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken) {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null) {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            var validator = new ChangeLeaveRequestApprovalValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid) {
                throw new BadRequestException("Invalid request", validationResult);
            }

            leaveRequest.Approved = request.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            try {
                var email = new EmailMessage() {
                    To = string.Empty,
                    Body = $"The approval status for your leave request from {leaveRequest.StartDate} to {leaveRequest.EndDate} has been changed successfully.",
                    Subject = "Leave Request Approval Status changed"
                };
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex) {
                _logger.LogWarning(ex.Message);
            }

            return Unit.Value;
        }
    }
}
