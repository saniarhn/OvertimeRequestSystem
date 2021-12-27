using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OvertimeRequestSystemAPI.ViewModel;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

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
            var getData = context.Employees.Find(overtimerequestVM.NIP);
            float salary = getData.BasicSalary;
         /*   var getData2 = context.Parameters.ToList();*/
            var a = context.Parameters.Where(e => e.ParameterName.Contains("konstanta_rumus")).FirstOrDefault();
            var konstanta_rumus = a.Value;
            var b = context.Parameters.Where(e => e.ParameterName.Contains("wd_percent_first_hour")).FirstOrDefault();
            var wd_percent_first_hour = b.Value;
            var c = context.Parameters.Where(e => e.ParameterName.Contains("wd_percent_second_hour")).FirstOrDefault();
            var wd_percent_second_hour = c.Value;
            var d = context.Parameters.Where(e => e.ParameterName.Contains("wk_percent_first_hour")).FirstOrDefault();
            var wk_percent_first_hour = d.Value;
            var e = context.Parameters.Where(e => e.ParameterName.Contains("wk_percent_second_hour")).FirstOrDefault();
            var wk_percent_second_hour = e.Value;
            var f = context.Parameters.Where(e => e.ParameterName.Contains("wk_percent_third_hour")).FirstOrDefault();
            var wk_percent_third_hour = f.Value;
            var g = context.Parameters.Where(e => e.ParameterName.Contains("wd_first_hour")).FirstOrDefault();
            var wd_first_hour = g.Value;
            var h = context.Parameters.Where(e => e.ParameterName.Contains("wk_first_hour")).FirstOrDefault();
            var wk_first_hour = h.Value;
            var j = context.Parameters.Where(e => e.ParameterName.Contains("wk_second_hour")).FirstOrDefault();
            var wk_second_hour = j.Value;
            var k = context.Parameters.Where(e => e.ParameterName.Contains("wk_third_hour")).FirstOrDefault();
            var wk_third_hour = k.Value;
            var result = 0;
            try
            {
        
                int sisajam2 = 0;
                int sisajam3 = 0;
                double hasil1 = 0;
                double hasil2 = 0;
                double hasil3 = 0;
                double hasil_lembur = 0;

          
                    /*untuk menampung hasil jam per detail*/
                    List<int> Hitung = new List<int>();

                    for (int i = 0; i < overtimerequestVM.ListDetail.Count(); i++)
                    {
                        var starthour = overtimerequestVM.ListDetail[i].StartHour;
                        var endhour = overtimerequestVM.ListDetail[i].EndHour;
                        Hitung.Add((endhour - starthour).Hours);


                    }
                    var arrayjam = Hitung;
                    int sumjam = Hitung.Sum();
                    /*  masukkan nilai total jam lembur*/
              /*      overtimerequestVM.SumOvertimeHour = sumjam;*/

                    if (overtimerequestVM.Date.DayOfWeek == DayOfWeek.Sunday || overtimerequestVM.Date.DayOfWeek == DayOfWeek.Saturday)
                    {
                      
                        /*  jika jumlah jam lembur diantara 1-8*/
                        if (sumjam > 0 && sumjam <= wk_first_hour)
                        {
                            /* sisajam pasti 0 karena sumjam dikurang angka lembur antara 1-8 , dimana 1-8 adalah 1-8 jam pertama*/
                       
                            hasil1 = konstanta_rumus * salary * wk_percent_first_hour * sumjam;
                            hasil_lembur = hasil1;
                        }
                        /* jika jumlah jam lembur 9 jam */
                        else if (sumjam == wk_second_hour)
                        {
                            /* jam awal 8 menyatakan 9 pasti sudah melewati 8 jam pertama*/
                            
                            sisajam2 = sumjam - Convert.ToInt32(wk_first_hour);
                            hasil1 = konstanta_rumus * salary * wk_percent_first_hour * Convert.ToInt32(wk_first_hour);
                            hasil2 = konstanta_rumus * salary * wk_percent_second_hour * sisajam2;
                            hasil_lembur = hasil1 + hasil2;
                        }
                        else if (sumjam > wk_second_hour && sumjam <= wk_third_hour)
                        {
                        
                            sisajam2 = sumjam - Convert.ToInt32(wk_first_hour);
                            sisajam3 = sumjam - Convert.ToInt32(wk_first_hour) - sisajam2;
                            hasil1 = konstanta_rumus * salary * wk_percent_first_hour * wk_first_hour;
                            hasil2 = konstanta_rumus * salary * wk_percent_second_hour * sisajam2;
                            hasil3 = konstanta_rumus * salary * wk_percent_third_hour * sisajam3;
                            hasil_lembur = hasil1 + hasil2 + hasil3;
                        }

                      

                    }


                    else 
                    {
                        if (sumjam > 0 && sumjam <= wd_first_hour)
                        {
                         
                            hasil1 = konstanta_rumus * salary * wd_percent_first_hour * sumjam;
                            hasil_lembur = hasil1;
                        }
                        else if (sumjam > wd_first_hour)
                        {
                            sisajam2 = sumjam - Convert.ToInt32(wd_first_hour);
                            hasil1 = konstanta_rumus * salary * wd_percent_first_hour * wd_first_hour;
                            hasil2 = konstanta_rumus * salary * wd_percent_second_hour * sisajam2;
                            hasil_lembur = hasil1 + hasil2;
                        }
                        
                    }
                    float sumsalary = Convert.ToInt32(hasil_lembur);
                    /*  masukkan nilai salary lembur*/
                /*    overtimerequestVM.OvertimeSalary = sumsalary;*/

                    Overtime overtime = new Overtime()
                    {
                        Date = overtimerequestVM.Date,
                        NIP = overtimerequestVM.NIP,
                        SumOvertimeHour = sumjam,
                        OvertimeSalary = sumsalary
                    };


                    context.Overtimes.Add(overtime);
                    context.SaveChanges();

                    foreach (var detail in overtimerequestVM.ListDetail)
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

                

            


            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }

        public int OvertimeResponseManager(OvertimeResponseVM overtimeResponseVM)
        {
            var getData = context.Overtimes.Find(overtimeResponseVM.OvertimeId);
            var result = 0;
            if (getData.StatusByManager == null)
            {
                if (overtimeResponseVM.StatusByManager == "Ditolak")
                {
                    Response response = new Response()
                    {
                        ResponseDescription = overtimeResponseVM.ResponseDescription,
                        ManagerOrFinanceId = overtimeResponseVM.ManagerOrFinanceId,
                        OvertimeId = getData.OvertimeId
                    };
                    context.Responses.Add(response);
                    context.SaveChanges();
                    getData.StatusByManager = overtimeResponseVM.StatusByManager;
                    context.Entry(getData).State = EntityState.Modified;
                    context.SaveChanges();
                    result = 1;
                }
                getData.StatusByManager = overtimeResponseVM.StatusByManager;
                context.Entry(getData).State = EntityState.Modified;
                context.SaveChanges();
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public int OvertimeResponseFinance(OvertimeResponseVM overtimeResponseVM)
        {
            var getData = context.Overtimes.Find(overtimeResponseVM.OvertimeId);
            var result = 0;
            if (getData.StatusByManager == "Ditolak" || getData.StatusByManager == "Diterima")
            {
                if (overtimeResponseVM.StatusByFinance == "Ditolak")
                {
                    Response response = new Response()
                    {
                        ResponseDescription = overtimeResponseVM.ResponseDescription,
                        ManagerOrFinanceId = overtimeResponseVM.ManagerOrFinanceId,
                        OvertimeId = getData.OvertimeId
                    };
                    context.Responses.Add(response);
                    context.SaveChanges();
                    getData.StatusByFinance = overtimeResponseVM.StatusByFinance;
                    context.Entry(getData).State = EntityState.Modified;
                    context.SaveChanges();
                    result = 1;
                }
                getData.StatusByFinance = overtimeResponseVM.StatusByFinance;
                context.Entry(getData).State = EntityState.Modified;
                context.SaveChanges();
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }

    }
}
