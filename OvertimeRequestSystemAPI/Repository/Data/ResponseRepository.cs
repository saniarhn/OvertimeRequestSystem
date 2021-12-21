using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Repository.Data
{
    public class ResponseRepository : GeneralRepository<MyContext, Response, int>
    {
        private readonly MyContext context;
        public ResponseRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
    }
}
