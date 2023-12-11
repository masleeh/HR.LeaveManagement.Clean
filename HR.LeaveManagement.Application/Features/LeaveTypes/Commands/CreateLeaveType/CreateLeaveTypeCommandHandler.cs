using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Commands.CreateLeaveType; 
public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int> {
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<CreateLeaveTypeCommandHandler> logger) {
        this._mapper = mapper;
        this._leaveTypeRepository = leaveTypeRepository;
        this._logger = logger;
    }
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken) {
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any()) {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(LeaveType), request.Name);
            throw new BadRequestException("LeaveType validation error", validationResult);
        }

        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);
        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);
        return leaveTypeToCreate.Id;
    }
}
