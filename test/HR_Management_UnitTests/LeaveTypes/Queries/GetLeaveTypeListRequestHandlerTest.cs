using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Contracts.Persistence;
using HR_Management_Application.Dtos;
using HR_Management_Application.Features.LeaveType.Handlers.Queries;
using HR_Management_Application.Features.LeaveType.Requests;
using HR_Management_Application.Profiles;
using HR_Management_UnitTests.Mock;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace HR_Management_UnitTests.LeaveTypes
{
    public class GetLeaveTypeListRequestHandlerTest
    {
        private IMapper _mapper;
        private Mock<ILeaveTypeRepository> _mockRepo;
        public GetLeaveTypeListRequestHandlerTest()
        {
            _mapper = new MapperConfiguration(
                cfg => { cfg.AddProfile<MappingProfile>(); },LoggerFactory.Create(builder => { }))
                .CreateMapper();
            // var configuration = new MapperConfiguration(
            //                                     cfg =>
            //                                     {
            //                                         cfg.AddProfile<MappingProfile>();
            //                                     },
            //                                     LoggerFactory.Create(builder => { }));
            // _mapper = configuration.CreateMapper();
            _mockRepo = MockLeaveTypeRepository.mockLeaveTypeRepository();
        }

        [Fact]
        public async Task Handle_Returns_All_Leave_Types()
        {
            // Arrange
            // var request = new GetLeaveTypeListRequest();
            var handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<LeaveTypeDto>>();
            // result.Count.ShouldBe(2);
        }
    }
}