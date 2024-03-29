﻿using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.Repository;
using OvertimeRequestSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OvertimeRequestSystemAPI.ViewModel;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Collections;

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

            var register = (from r in context.Overtimes
                            where r.NIP == overtimerequestVM.NIP && r.Date.Month == overtimerequestVM.Date.Month
                            group r by r.NIP into testcount
                            select new
                            {

                                NIP = testcount.Key,
                                Hour = testcount.Sum(t => t.SumOvertimeHour),

                            }).FirstOrDefault();

            var result = 0;



            try
            {





                int sisajam2 = 0;
                int sisajam3 = 0;
                double hasil1 = 0;
                double hasil2 = 0;
                double hasil3 = 0;
                double hasil_lembur = 0;


                if (register == null || register.Hour <= 40)
                {

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
                else
                {
                    result = 0;
                }




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

            if (getData.StatusByManager == null || getData.StatusByManager == "pending")
            {
                if (overtimeResponseVM.StatusByManager == "denied")
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
            if (getData.StatusByManager == "denied" || getData.StatusByManager == "accepted")
            {
                if (overtimeResponseVM.StatusByFinance == "denied")
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



        public int OvertimeRequestMail(string email)
        {
            var checkEmail = context.Employees.Where(e => e.Email == email).FirstOrDefault();

            if (checkEmail != null)
            {
                var getName = checkEmail.Name;
                var getNIP = checkEmail.NIP;
                var getData = checkEmail.overtimes.LastOrDefault();

                if (getData != null)
                {
                    /*       string ChangePassword = Guid.NewGuid().ToString();

                           getData.Password = Hashing.Hashing.HashPassword(ChangePassword);
                           context.SaveChanges();*/

                    var getHour = getData.SumOvertimeHour;
                    var getDate = getData.Date.DayOfWeek;
                    DateTime today = DateTime.Now;

                    /*  untuk mengirim email*/
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("sniaaaa044@gmail.com");
                    msg.To.Add(new MailAddress(email));
                    msg.Subject = "Your Overtime Request Submitted" + today;
                    msg.Body = $"<p>Hai,{getName}</p>" + $"</br><p> Your Overtime Request {getHour} Hour on {getDate} <p>" + $"</br><p> has submitted <p>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("sniaaaa044@gmail.com", "sania1234");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                    return 1;

                }
                return 2;
            }
            return 2;


        }

        public int OvertimeResponseMailByManager(int OvertimeId)
        {
            var getDataOvertime = context.Overtimes.Find(OvertimeId);
            if (getDataOvertime != null)
            {
                var getDataEmployee = context.Employees.Find(getDataOvertime.NIP);
                var checkEmail = getDataEmployee.Email;

                if (checkEmail != null)
                {
                    var getName = getDataEmployee.Name;
                    var getDataStatus = getDataOvertime.StatusByManager;

                    /*       string ChangePassword = Guid.NewGuid().ToString();

                           getData.Password = Hashing.Hashing.HashPassword(ChangePassword);
                           context.SaveChanges();*/

                    var getHour = getDataOvertime.SumOvertimeHour;
                    var getDate = getDataOvertime.Date.DayOfWeek;
                    var getDate2 = getDataOvertime.Date.ToShortDateString();
                    DateTime today = DateTime.Now;

                    /*  untuk mengirim email*/
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("sniaaaa044@gmail.com");
                    msg.To.Add(new MailAddress(checkEmail));
                    msg.Subject = "Your Overtime Request Info" + today;
                    msg.Body = $"<p>Hai,{getName}</p>" + $"</br><p> Your Overtime Request {getHour} Hour  on {getDate}, {getDate2}<p>" + $"</br><p>status {getDataStatus} By Your Manager<p>" + $"</br><p> Check Your Account For Details <p>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("sniaaaa044@gmail.com", "sania1234");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                    return 1;


                }
                return 2;
            }
            return 2;

        }

        public int OvertimeResponseMailByFinance(int OvertimeId)
        {
            var getDataOvertime = context.Overtimes.Find(OvertimeId);
            var getDataEmployee = context.Employees.Find(getDataOvertime.NIP);
            var checkEmail = getDataEmployee.Email;
            if (getDataOvertime != null)
            {
                if (checkEmail != null)
                {
                    var getName = getDataEmployee.Name;
                    var getDataStatusManager = getDataOvertime.StatusByManager;
                    var getDataStatusFinance = getDataOvertime.StatusByFinance;

                    /*       string ChangePassword = Guid.NewGuid().ToString();

                           getData.Password = Hashing.Hashing.HashPassword(ChangePassword);
                           context.SaveChanges();*/

                    var getHour = getDataOvertime.SumOvertimeHour;
                    var getDate = getDataOvertime.Date.DayOfWeek;
                    var getDate2 = getDataOvertime.Date.ToShortDateString();
                    var a = getDate2;
                    DateTime today = DateTime.Now;

                    /*  untuk mengirim email*/
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("sniaaaa044@gmail.com");
                    msg.To.Add(new MailAddress(checkEmail));
                    msg.Subject = "Your Overtime Request Info" + today;
                    msg.Body = $"<p>Hai,{getName}</p>" + $"</br><p> Your Overtime Request {getHour} Hour  on {getDate}, {getDate2} <p>" + $" </br> <p> status {getDataStatusManager} By Your Manager and status  {getDataStatusFinance} By Your Finance <p>" + $"</br><p> Check Your Account For Details <p>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("sniaaaa044@gmail.com", "sania1234");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                    return 1;


                }
                return 2;
            }
            return 2;

        }

        public IEnumerable<Overtime> GetHistory(int nip)
        {
            var getHistory = context.Overtimes.Where(e => e.NIP == nip);
            return getHistory.ToList();
        }

        public IEnumerable<GetResponseVM> GetResponseForManager(int nip)
        {
            /* ambil data pegawai sesuai manajer nya */
            var register = from a in context.Employees
                           where a.ManagerId == nip
                           join b in context.Overtimes on a.NIP equals b.NIP
                           select new GetResponseVM()
                           {
                               NIP = a.NIP,
                               OvertimeId = b.OvertimeId,
                               Name = a.Name,
                               Position = a.Position,
                               Date = b.Date,
                               SumOvertimeHour = b.SumOvertimeHour,
                               StatusByManager = b.StatusByManager
                           };

            return register.ToList();
        }
        public IEnumerable<GetResponseVM> GetResponseForFinance()
        {
            /* ambil data pegawai apabila status overtimenya diterima manajernya*/
            var register = from a in context.Employees
                           join b in context.Overtimes on a.NIP equals b.NIP
                           where b.StatusByManager == "accepted" && b.StatusByManager != null
                           select new GetResponseVM()
                           {
                               NIP = a.NIP,
                               OvertimeId = b.OvertimeId,
                               Name = a.Name,
                               Position = a.Position,
                               Date = b.Date,
                               SumOvertimeHour = b.SumOvertimeHour,
                               StatusByFinance = b.StatusByFinance
                           };

            return register.ToList();
        }

        public IEnumerable<GetDetailResponseVM> GetDetailResponse(int overtimeId)
        {
            /* ambil data pegawai dan detail overtimenya*/
            var register = from a in context.Employees
                           join b in context.Overtimes on a.NIP equals b.NIP
                           join c in context.OvertimeDetails on b.OvertimeId equals c.OvertimeId
                           where b.OvertimeId == overtimeId
                           select new GetDetailResponseVM()
                           {
                               NIP = a.NIP,
                               OvertimeId = b.OvertimeId,
                               Name = a.Name,
                               Position = a.Position,
                               Date = b.Date,
                               SumOvertimeHour = b.SumOvertimeHour,
                               StartHour = c.StartHour,
                               EndHour = c.EndHour,
                               TaskName = c.TaskName,
                               LocationName = c.LocationName,
                               OvertimeSalary = (int)b.OvertimeSalary,
                           };

            return register.ToList();
        }

        public IEnumerable<GetResponseVM> GetResponseForDirector(int nip)
        {
            /* ambil data pegawai sesuai manajer nya */
            var register = from a in context.Employees
                           where a.ManagerId == nip
                           join b in context.Overtimes on a.NIP equals b.NIP
                           join c in context.AccountRoles on b.NIP equals c.NIP
                           join d in context.Roles on c.RoleId equals d.RoleId
                           where d.RoleName == "manager" || d.RoleName == "finance"
                           select new GetResponseVM()
                           {
                               NIP = a.NIP,
                               OvertimeId = b.OvertimeId,
                               Name = a.Name,
                               Position = a.Position,
                               Date = b.Date,
                               SumOvertimeHour = b.SumOvertimeHour,
                               StatusByManager = b.StatusByManager
                           };

            return register.ToList();
        }

        public int OvertimeResponseDirector(OvertimeResponseVM overtimeResponseVM)
        {
            var getData = context.Overtimes.Find(overtimeResponseVM.OvertimeId);
            var result = 0;
            if (getData.StatusByManager == null || getData.StatusByManager == "pending")
            {
                if (overtimeResponseVM.StatusByManager == "denied")
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
                    getData.StatusByFinance = getData.StatusByManager;
                    context.Entry(getData).State = EntityState.Modified;
                    context.SaveChanges();
                    result = 1;
                }
                getData.StatusByManager = overtimeResponseVM.StatusByManager;
                getData.StatusByFinance = getData.StatusByManager;
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
        public int OvertimeResponseMailByDirector(int OvertimeId)
        {
            var getDataOvertime = context.Overtimes.Find(OvertimeId);
            if (getDataOvertime != null)
            {
                var getDataEmployee = context.Employees.Find(getDataOvertime.NIP);
                var checkEmail = getDataEmployee.Email;

                if (checkEmail != null)
                {
                    var getName = getDataEmployee.Name;
                    var getDataStatus = getDataOvertime.StatusByManager;

                    /*       string ChangePassword = Guid.NewGuid().ToString();

                           getData.Password = Hashing.Hashing.HashPassword(ChangePassword);
                           context.SaveChanges();*/

                    var getHour = getDataOvertime.SumOvertimeHour;
                    var getDate = getDataOvertime.Date.DayOfWeek;
                    var getDate2 = getDataOvertime.Date.ToShortDateString();
                    DateTime today = DateTime.Now;

                    /*  untuk mengirim email*/
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("sniaaaa044@gmail.com");
                    msg.To.Add(new MailAddress(checkEmail));
                    msg.Subject = "Your Overtime Request Info" + today;
                    msg.Body = $"<p>Hai,{getName}</p>" + $"</br><p> Your Overtime Request {getHour} Hour  on {getDate}, {getDate2}<p>" + $"</br><p>status {getDataStatus} By Your Director<p>" + $"</br><p> Check Your Account For Details <p>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("sniaaaa044@gmail.com", "sania1234");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                    return 1;


                }
                return 2;
            }
            return 2;

        }


        public IEnumerable GetCount()
        {
          
            var register = from b in context.Overtimes
                           where b.NIP == 2 && b.Date.Month == DateTime.Now.Month
                           group b by b.NIP into testcount
                           select new
                           {

                               NIP = testcount.Key,
                               Quantity = testcount.Sum(t => t.SumOvertimeHour),

                           };


            return register.ToList();
        }

        public IEnumerable<Overtime> GetCountSalary(int NIP)
        {

            var register = from r in context.Overtimes
                           where r.NIP == NIP && r.Date.Month == DateTime.Now.Month
                           && r.StatusByManager == "accepted" && r.StatusByFinance == "accepted"
                           group r by r.NIP into testcount
                           select new Overtime()
                           {

                               NIP = testcount.Key,
                               SumOvertimeHour = testcount.Sum(t => t.SumOvertimeHour),
                               OvertimeSalary = testcount.Sum(s => s.OvertimeSalary)

                           };


            return register.ToList();
        }



/*
        public int OvertimeSalaryMail(string email)
        {
            var checkEmail = context.Employees.Where(e => e.Email == email).FirstOrDefault();

            if (checkEmail != null)
            {
                var getName = checkEmail.Name;
                var getNIP = checkEmail.NIP;


                var getData = (from r in context.Overtimes
                                where r.NIP == checkEmail.NIP && r.Date.Month == DateTime.Now.Month
                                && r.StatusByManager=="accepted" && r.StatusByFinance == "accepted"
                                group r by r.NIP into testcount
                                select new
                                {

                                    NIP = testcount.Key,
                                    Hour = testcount.Sum(t => t.SumOvertimeHour),
                                    Salary =testcount.Sum(s => s.OvertimeSalary)

                                }).FirstOrDefault();


                *//*       string ChangePassword = Guid.NewGuid().ToString();

                       getData.Password = Hashing.Hashing.HashPassword(ChangePassword);
                       context.SaveChanges();*//*
                if (getData != null)
                {
                    var getHour = getData.Hour;
                    var getSalary = getData.Salary;
                    var getDateMonth = DateTime.Now.Month;
                    var getDateYear = DateTime.Now.Year;
                    DateTime today = DateTime.Now;

                    *//*  untuk mengirim email*//*
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("sniaaaa044@gmail.com");
                    msg.To.Add(new MailAddress(email));
                    msg.Subject = "Your Overtime Invoice Info" + today;
                    msg.Body = $"<p>Hai,{getName}</p>" + $"</br><p> at this time, your overtime total is {getHour} Hour for {getDateMonth}{getDateYear}  <p>" + $"</br><p> this amount of overtime pay Will be sent to you on your payday<p>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("sniaaaa044@gmail.com", "sania1234");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg);
                    return 1;

                }
                else
                {
                    return 2;
                }



            }
            else
            {
                return 2;
            }
    


        }*/
   
    }
}
