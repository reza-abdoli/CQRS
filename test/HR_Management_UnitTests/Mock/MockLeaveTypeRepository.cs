using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Contracts.Persistence;
using HR_Management_Domain;
using Moq;

namespace HR_Management_UnitTests.Mock
{
    public static class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> mockLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    Name = "Vacation",
                    DefaultDay = 10
                },
                new LeaveType
                {
                    Id = 2,
                    Name = "Sick",
                    DefaultDay = 5
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return leaveTypes.FirstOrDefault(q => q.Id == id);
            });

            mockRepo.Setup(r => r.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
            {
                leaveType.Id = leaveTypes.Max(x => x.Id) + 1;
                leaveTypes.Add(leaveType);
                return leaveType;
            });

            return mockRepo;
        }
    }
}