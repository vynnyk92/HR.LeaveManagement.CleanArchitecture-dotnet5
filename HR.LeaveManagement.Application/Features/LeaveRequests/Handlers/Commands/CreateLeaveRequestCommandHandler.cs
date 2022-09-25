using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Infrastructure.Contracts;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _ofWork;

        public CreateLeaveRequestCommandHandler(
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper, IEmailSender emailSender, IUnitOfWork ofWork)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            _ofWork = ofWork;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_ofWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }

            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);

            leaveRequest = await _ofWork.LeaveRequestRepository.Add(leaveRequest);
            await _ofWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveRequest.Id;

            var email = new Email
            {
                To = "test@test.com",
                Body = "test",
                Subject = "Approved"
            };

            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (Exception e)
            {

            }

            return response;
        }
    }
}
