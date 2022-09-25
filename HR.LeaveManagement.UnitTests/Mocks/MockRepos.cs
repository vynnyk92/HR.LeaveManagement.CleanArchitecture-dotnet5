using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.UnitTests.Mocks
{
    public static class MockRepos
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepoMock()
        {
            var leaveTypes = new List<Domain.LeaveType>
            {
                new()
                {
                    Id = 11,
                    DefaultDays = 10,
                    Name = "Vacation"
                },
                new()
                {
                    Id = 12,
                    DefaultDays = 2,
                    Name = "Sick"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.Add(It.IsAny<Domain.LeaveType>()))
                .ReturnsAsync((Domain.LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return leaveType;
                });

            return mockRepo;
        }
    }
}
