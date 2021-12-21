using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Repository.Data
{
    public class ParameterRepository : GeneralRepository<MyContext, Parameter, int>
    {
        private readonly MyContext context;
        public ParameterRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
    }
}
