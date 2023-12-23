using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests {
    public class HrDatabaseContextTests {
        private readonly HRDatabaseContext _hrDatabaseContext;

        public HrDatabaseContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<HRDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _hrDatabaseContext = new HRDatabaseContext(dbOptions);
        }
        [Fact]
        public async void Save_SetDateCreatedValue() {

            var leaveType = new LeaveType() {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            await _hrDatabaseContext.LeaveType.AddAsync(leaveType);
            await _hrDatabaseContext.SaveChangesAsync();

            leaveType.DateCreated.ToString().ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async void Save_SetDateModifiedValue() {
            var leaveType = new LeaveType() {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            await _hrDatabaseContext.LeaveType.AddAsync(leaveType);
            await _hrDatabaseContext.SaveChangesAsync();

            leaveType.DateModified.ToString().ShouldNotBeNullOrEmpty();
        }
    }
}