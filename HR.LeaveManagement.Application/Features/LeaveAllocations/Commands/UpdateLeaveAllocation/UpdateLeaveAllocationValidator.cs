﻿using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation {
    public class UpdateLeaveAllocationValidator : AbstractValidator<UpdateLeaveAllocationCommand> {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._leaveTypeRepository = leaveTypeRepository;
            this._leaveAllocationRepository = leaveAllocationRepository;

            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0")
                .LessThan(31)
                .WithMessage("{PropertyName} must be less then 31");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} is not valid")
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist");

            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} must be not null")
                .MustAsync(LeaveAllocationMustExist);
        }

        private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token) {
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token) {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
