using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Contracts.Persistence;
using HR_Management_Application.Dtos;
using HR_Management_Application.Dtos.LeaveType;
using HR_Management_Application.Features.LeaveType.Handlers.Commands;
using HR_Management_Application.Features.LeaveType.Handlers.Queries;
using HR_Management_Application.Features.LeaveType.Requests;
using HR_Management_Application.Features.LeaveType.Requests.Commands;
using HR_Management_Application.Profiles;
using HR_Management_Application.Responses;
using HR_Management_UnitTests.Mock;
using Moq;
using Shouldly;

namespace HR_Management_UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTest
    {
        private IMapper _mapper;
        private Mock<ILeaveTypeRepository> _mockRepo;
        private CreateLeaveTypeDto _createLeaveTypeDto;
        public CreateLeaveTypeCommandHandlerTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _mockRepo = MockLeaveTypeRepository.mockLeaveTypeRepository();
            _createLeaveTypeDto = new CreateLeaveTypeDto()
            {
                Name = "Test Leave Type",
                DefaultDay = 10
            };
        }

        [Fact]
        public async Task Handle_Create_Leave_Type()
        {

            var handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new CreateLeaveTypeCommand() { CreateLeaveTypeDto = _createLeaveTypeDto }, CancellationToken.None);

            // we should check id of added leaveType which is Test Leave Type
            Assert.True(result.Success);

            result.ShouldBeOfType<BaseCommandResponse>();

            Assert.Equal(3, result.Id);
        }
        [Fact]
        public async Task Leave_Type_Repository_Changed()
        {

            var handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new CreateLeaveTypeCommand() { CreateLeaveTypeDto = _createLeaveTypeDto }, CancellationToken.None);

            //check size of repo is changed to 3
            Assert.Equal(3, (await _mockRepo.Object.GetAll()).Count);
        }
    }

}