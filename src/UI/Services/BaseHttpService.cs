using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hanssens.Net;
using UI.Contracts;

namespace UI.Services.Base
{
    public class BaseHttpService
    {
        protected readonly IClient _client;
        protected readonly ILoacalStorageService _localStorageService;
        public BaseHttpService(IClient client, ILoacalStorageService localStorage)
        {
            this._client = client;
            this._localStorageService = localStorage;
        }
        protected Responses<T> ConvertApiExceptions<T>(ApiException exception)
        {
            if (exception.StatusCode == 400)
            {
                return new Responses<T>() { Message = "Validation errors have occurred.", ValidationErrors = exception.Response, Success = false };
            }
            else if (exception.StatusCode == 404)
            {
                return new Responses<T>() { Message = "Not Found ...", Success = false };
            }
            else
            {
                return new Responses<T>() { Message = "Something went wrong,try again ...", Success = false };
            }
        }

        protected void AddBearerToken()
        {
            if (_localStorageService.Exists("token"))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorageService.GetStorageValue<string>("token"));
            }
        }
    }
}