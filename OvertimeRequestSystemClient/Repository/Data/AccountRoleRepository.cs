using Microsoft.AspNetCore.Http;
using OvertimeRequestSystemAPI.Models;
using OvertimeRequestSystemClient.Base.Urls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OvertimeRequestSystemClient.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<AccountRole, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public AccountRoleRepository(Address address, string request = "AccountRoles/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }
    }
}
