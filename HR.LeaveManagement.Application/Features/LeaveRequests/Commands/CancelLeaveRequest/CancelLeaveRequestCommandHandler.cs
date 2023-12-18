using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CancelLeaveRequest {
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit> {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CancelLeaveRequestCommandHandler> _logger;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<CancelLeaveRequestCommandHandler> logger)
        {
            this._leaveRequestRepository = leaveRequestRepository;
            this._emailSender = emailSender;
            this._logger = logger;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken) {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null) {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            leaveRequest.Cancelled = true;

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            try {
                var email = new EmailMessage() {
                    To = string.Empty,
                    Body = $"Your leave request from {leaveRequest.StartDate} to {leaveRequest.EndDate} has been cancelled successfully.",
                    Subject = "Leave Request cancelled"
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
