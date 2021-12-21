using OvertimeRequestSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        //nama object
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<OvertimeDetail> OvertimeDetails { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Response> Responses { get; set; }
        //membuat relasi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to one employee x account
            modelBuilder.Entity<Employee>()
                .HasOne(acc => acc.Account)
                .WithOne(emp => emp.Employee)
                .HasForeignKey<Account>(acc => acc.NIP);

            //many to one account x account_roles
            modelBuilder.Entity<Account>()
                .HasMany(acr => acr.AccountRoles)
                .WithOne(acc => acc.Account);

            //one to many role x account_role
            modelBuilder.Entity<AccountRole>()
                .HasOne(rol => rol.Role)
                .WithMany(acr => acr.AccountRoles);

            //one to many overtime x overtime_detail
            modelBuilder.Entity<OvertimeDetail>()
                .HasOne(ovt => ovt.Overtime)
                .WithMany(ovd => ovd.overtimeDetails);

            //one to many overtime x responses
            modelBuilder.Entity<Response>()
                .HasOne(ovt => ovt.Overtime)
                .WithMany(res => res.responses);

            //one to many employee x response
            modelBuilder.Entity<Response>()
                .HasOne(emp => emp.Employee)
                .WithMany(res => res.responses);

            //one to many employee x overtime
            modelBuilder.Entity<Overtime>()
                .HasOne(emp => emp.Employee)
                .WithMany(ovt => ovt.overtimes);

            //one to many 2
            /*modelBuilder.Entity<Response>(table => {
                table.Property<int>("ManagerOrFinanceId");

                table.HasOne(e => e.Employee)
                    .WithMany(r => r.responses)
                    .HasForeignKey("ManagerOrFinanceId");
                    //.HasPrincipalKey<Database>(d => d.DatabaseId);
            });*/
            

        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }*/
    }
}
