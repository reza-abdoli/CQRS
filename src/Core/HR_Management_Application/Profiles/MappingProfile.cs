using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Management_Application.Dtos;
using HR_Management_Application.Dtos.LeaveAllocation;
using HR_Management_Application.Dtos.LeaveRequest;
using HR_Management_Application.Dtos.LeaveType;
using HR_Management_Domain;

namespace HR_Management_Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<LeaveRequest,LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest,LeaveRequestListDto>().ReverseMap();
            CreateMap<LeaveRequest,CreateLeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest,UpdateLeaveRequestDto>().ReverseMap();

            CreateMap<LeaveAllocation,LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation,CreateLeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation,UpdateLeaveAllocationDto>().ReverseMap();

            CreateMap<LeaveType,LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType,CreateLeaveTypeDto>().ReverseMap();
        }
    }
}