﻿using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation {
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit> {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken) {
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

            if (leaveAllocation == null) {
                throw new NotFoundException("LeaveAllocation does not exist", request.Id);
            }

            await _leaveAllocationRepository.DeleteAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
