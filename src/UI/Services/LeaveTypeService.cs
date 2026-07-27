using AutoMapper;
using UI.Contracts;
using UI.Models;
using UI.Services.Base;

namespace UI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;
        private readonly ILoacalStorageService _localStorageService;

        public LeaveTypeService(IMapper mapper, IClient client, ILoacalStorageService localStorageService)
        : base(client, localStorageService)
        {
            _mapper = mapper;
            _client = client;
            _localStorageService = localStorageService;
        }

        public async Task<Responses<int>> CreateLeaveType(CreateLeaveTypeVM createLeaveTypeVM)
        {
            try
            {
                var response = new Responses<int>();
                CreateLeaveTypeDto createLeaveTypeDto =
                     _mapper.Map<CreateLeaveTypeDto>(createLeaveTypeVM);

                var apiResponse = await _client.LeaveTypePOSTAsync(createLeaveTypeDto);
                if (apiResponse.Success)
                {
                    response.Success = true;
                    response.Data = apiResponse.Id;
                }
                else
                {
                    response.Success = false;
                    foreach (var err in apiResponse.Errors)
                    {
                        response.ValidationErrors += err;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Responses<int>> DeleteLeaveType(int id)
        {
            try
            {
                await _client.LeaveTypeDELETEAsync(id);
                return new Responses<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return this.ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetail(int id)
        {
            var LeaveTypeDto = await _client.LeaveTypeGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(LeaveTypeDto);

        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypeDtos = await _client.LeaveTypeAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypeDtos);
        }

        public async Task<Responses<int>> UpdateLeaveType(int id, LeaveTypeVM leaveTypeVM)
        {
            try
            {
                var updateLeaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveTypeVM);

                await _client.LeaveTypePUTAsync(id, updateLeaveTypeDto);
                return new Responses<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return this.ConvertApiExceptions<int>(ex);
            }
        }
    }
}