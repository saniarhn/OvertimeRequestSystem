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
            var result = httpClient.PutAsync(address.Link + request + "OvertimeResponseFinance" , content).Result;
            return result.StatusCode;
        }

        /*        public HttpStatusCode OvertimeHistory(int nip)
                {

                    var result = httpClient.GetAsync(address.Link + request + "OvertimeHistory/" + nip).Result;
                    return result.StatusCode;
                }*/

        public async Task<List<Overtime>> GetOvertimeHistory(int nip)
        {
            List<Overtime> entities = new List<Overtime>();

            using (var response = await httpClient.GetAsync(request + "OvertimeHistory/" + nip))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Overtime>>(apiResponse);
            }
            return entities;
        }


        public async Task<List<GetResponseVM>> GetResponseForManager(int nip)
        {
            List<GetResponseVM> entities = new List<GetResponseVM>();

            using (var response = await httpClient.GetAsync(request + "GetResponseForManager/" + nip))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<GetResponseVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<GetResponseVM>> GetResponseForFinance()
        {
            List<GetResponseVM> entities = new List<GetResponseVM>();

            using (var response = await httpClient.GetAsync(request + "GetResponseForFinance" ))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<GetResponseVM>>(apiResponse);
            }
            return entities;
        }

    }
}
