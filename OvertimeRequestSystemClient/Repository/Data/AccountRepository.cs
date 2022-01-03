

using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemAPI.ViewModel;
using OvertimeRequestSystemClient.Base.Urls;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace OvertimeRequestSystemClient.Repository.Data
{
    public class AccountRepository : GeneralRepository<Account, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public AccountRepository(Address address, string request = "Accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }

        public async Task<JWTokenVM> Auth(LoginVM loginVM)
        {
            JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "Login", content).Result;

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;
        }

        public HttpStatusCode InsertAccount(AccVM accVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(accVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "InsertAccount", content).Result;
            return result.StatusCode;
        }
    }
}
