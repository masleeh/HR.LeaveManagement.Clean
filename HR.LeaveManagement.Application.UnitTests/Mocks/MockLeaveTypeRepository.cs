using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks {
    public class MockLeaveTypeRepository {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypes() {
            var leaveTypes = new List<LeaveType>() {
                new LeaveType {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test vacation"
                },
                new LeaveType {
                    Id = 2,
                    DefaultDays = 12,
                    Name = "Test Sick"
                },
                new LeaveType {
                    Id = 3,
                    DefaultDays = 8,
                    Name = "Holiday"
                },

            };

            var leaveTypeDetails = new LeaveType () {
                Id = 3,
                DefaultDays = 8,
                Name = "Holiday",
                DateCreated = It.IsAny<DateTime>(),
                DateModified = It.IsAny<DateTime>()
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) => {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

            mockRepo.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(leaveTypeDetails);

            return mockRepo;
        }
    }
}
