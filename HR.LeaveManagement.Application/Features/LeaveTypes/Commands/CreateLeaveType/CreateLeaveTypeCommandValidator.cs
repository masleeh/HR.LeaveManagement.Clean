using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Commands.CreateLeaveType {
    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand> {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this._leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(p => p.DefaultDays)
                .NotNull()
                .LessThan(35).WithMessage("{PropertyName} cannot be greater than 35 days")
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

            RuleFor(q => q)
                .MustAsync(LeaveTypeUniqueName)
                .WithMessage("Leave type already exists");
        }

        private async Task<bool> LeaveTypeUniqueName(CreateLeaveTypeCommand command, CancellationToken token) {
            return await _leaveTypeRepository.IsUniqueLeaveType(command.Name);
        }
    }
}
