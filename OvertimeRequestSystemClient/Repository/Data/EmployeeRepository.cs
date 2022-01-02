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
    public class EmployeeRepository : GeneralRepository<Employee, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }
        /*public async Task<List<KeyValuePair<string, int>>> GetCountPosition()
        {
            List<KeyValuePair<string, int>> entities = new List<KeyValuePair<string, int>>();
            using (var response = await httpClient.GetAsync(request+ "GetCountPosition"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(apiResponse);
            }
            return entities;
        }*/
    }
}
