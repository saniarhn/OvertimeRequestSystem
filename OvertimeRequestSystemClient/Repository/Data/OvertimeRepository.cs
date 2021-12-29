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
    public class OvertimeRepository : GeneralRepository<Overtime, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public OvertimeRepository(Address address, string request = "Overtimes/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }
        public HttpStatusCode PostOvertimeRequest(OvertimeRequestVM overtimerequestVM,string email)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(overtimerequestVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "OvertimeRequest/" + email, content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode OvertimeResponseManager(OvertimeResponseVM overtimeResponseVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(overtimeResponseVM), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(address.Link + request + "OvertimeResponseManager" , content).Result;
            return result.StatusCode;
        }
        public HttpStatusCode OvertimeResponseFinance(OvertimeResponseVM overtimeResponseVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(overtimeResponseVM), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(address.Link + request + "OvertimeResponseFinance", content).Result;
            return result.StatusCode;
        }
    }
}
