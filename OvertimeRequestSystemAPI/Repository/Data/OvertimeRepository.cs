using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OvertimeRequestSystemAPI.ViewModel;
using Newtonsoft.Json;


namespace OvertimeRequestSystemAPI.Repository.Data
{
    public class OvertimeRepository : GeneralRepository<MyContext, Overtime, int>
    {
        private readonly MyContext context;
        public OvertimeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public int OvertimeRequest(OvertimeRequestVM overtimerequestVM)
        {
            var result = 0;
            try
            {
              
                Overtime overtime = new Overtime()
                {
                    StartDate = overtimerequestVM.StartDate,
                    EndDate = overtimerequestVM.EndDate,
                    NIP = overtimerequestVM.NIP,
             
                };


                context.Overtimes.Add(overtime);
                context.SaveChanges();

                foreach(var detail in overtimerequestVM.ListDetail)
                {

                    OvertimeDetail overtimeDetail = new OvertimeDetail()
                    {
                    StartHour = detail.StartHour,
                    EndHour = detail.EndHour,
                    TaskName = detail.TaskName,
                    LocationName = detail.LocationName,
                    OvertimeId = overtime.OvertimeId

                    };
                 


                    context.OvertimeDetails.Add(overtimeDetail);
                }
                

                
                context.SaveChanges();
                result = 1;

           /*     if (overtime.StartDate == overtime.EndDate)
                {*/
                    /*    if(overtime.StartDate.DayOfWeek != DayOfWeek.Sunday || overtime.StartDate.DayOfWeek != DayOfWeek.Saturday )
                        {
                            var tes = overtimerequestVM.ListDetail.Count();
                            for (int i = 0; i < overtimerequestVM.ListDetail.Count(); i++)
                            {
                               var starthour = overtimerequestVM.ListDetail[i].StartHour;
                                result = 1;
                            }
                            result = 2;
                        }
                        var tes2 = overtimerequestVM.ListDetail.Count();
                        result = 1;*/

             /*       if (overtime.StartDate.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        *//*int[] tes2;
                        for (int i = 0; i < overtimerequestVM.ListDetail.Count(); i++)
                        {
                            var starthour = overtimerequestVM.ListDetail[i].StartHour;
                            var endhour = overtimerequestVM.ListDetail[i].EndHour;
                            int[] tes= (endhour - starthour).Hours;
                          
                          
                        }*//*

                        int[] coba = new int[overtimerequestVM.ListDetail.Count()];
                        for (int i = 0; i < overtimerequestVM.ListDetail.Count(); i++)
                        {
                            coba[i] = (overtimerequestVM.ListDetail[i].EndHour - overtimerequestVM.ListDetail[i].StartHour).Hours;
                        }



                        result = 2;
                    }
                }
                else
                {
                    result = 1;
                }
       */

            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }
    }
}
