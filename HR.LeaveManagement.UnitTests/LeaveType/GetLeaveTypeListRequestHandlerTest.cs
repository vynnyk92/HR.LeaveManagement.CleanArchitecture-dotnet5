using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace HR.LeaveManagement.UnitTests.LeaveType
{
    public class GetLeaveTypeListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeListRequestHandlerTest()
        {
            _leaveTypeRepository = MockRepos.GetLeaveTypeRepoMock().Object;
            var mapConf = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapConf.CreateMapper();
        }

        [Fact]
        public async Task WhenCalling_GetLeaveTypeListRequest_ItReturnsExpectedResult()
        {
            //Arrange
            var handler = new GetLeaveTypeListRequestHandler(_leaveTypeRepository, _mapper);
            var request = new GetLeaveTypeListRequest();

            //Act
            var res = await handler.Handle(request, default);

            //Assert
            res.Count.ShouldBe(2);
        }

        [Fact]
        public async Task WhenCalling_CreateLeaveTypeListRequest_ItReturnsExpectedNumberOfLeaveTypes()
        {
            //Arrange
            var handler = new CreateLeaveTypeCommandHandler(_leaveTypeRepository, _mapper);
            var request = new CreateLeaveTypeCommand(new CreateLeaveTypeDto
            {
                DefaultDays = 1,
                Name = "s"
            });

            //Act
            var res = await handler.Handle(request, default);

            //Assert
            var data = _leaveTypeRepository.GetAll().Result;
            data.Count.ShouldBe(3);
        }
    }
}
