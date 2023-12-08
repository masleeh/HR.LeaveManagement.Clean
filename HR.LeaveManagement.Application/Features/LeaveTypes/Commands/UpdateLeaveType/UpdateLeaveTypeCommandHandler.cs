﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit> {
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository) {
        this._mapper = mapper;
        this._leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken) {
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);
        await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
        return Unit.Value;
    }
}
