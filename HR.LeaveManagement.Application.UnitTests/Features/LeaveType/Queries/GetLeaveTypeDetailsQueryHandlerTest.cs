using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType.Queries {
    public class GetLeaveTypeDetailsQueryHandlerTest {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;

        public GetLeaveTypeDetailsQueryHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypes();

            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveTypeDetails() {
            var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object);

            var result = await handler.Handle(new GetLeaveTypeDetailsQuery(3), CancellationToken.None);

            result.ShouldBeOfType<LeaveTypeDetailsDto>();
            result.ShouldBeEquivalentTo(new LeaveTypeDetailsDto {
                Id = 3,
                DefaultDays = 8,
                Name = "Holiday",
                DateCreated = It.IsAny<DateTime>(),
                DateModified = It.IsAny<DateTime>(),
            });
        }
    }
}
