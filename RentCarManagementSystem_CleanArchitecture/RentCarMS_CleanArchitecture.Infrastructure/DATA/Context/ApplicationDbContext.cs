using Microsoft.EntityFrameworkCore;
using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.DuePayments;
using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Infrastructure.DATA.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Member> members { get; set; }
        public DbSet<Car> cars { get; set; }       
        public DbSet<RentCar> rentCars { get; set; }
        public DbSet<RentCarDetail> rentCarDetails { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<DuePayment> duePayments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RentCar>()
           .HasOne(rc => rc.Member)
           .WithMany(m => m.RentCars)
           .HasForeignKey(rc => rc.MemberID);


            modelBuilder.Entity<RentCar>()
                .HasMany(rc => rc.RentCarDetails)
                .WithOne(rcd => rcd.RentCar)
                .HasForeignKey(rcd => rcd.RentCarID);

            modelBuilder.Entity<RentCarDetail>()
                .HasOne(rcd => rcd.Car)
                .WithMany(c => c.RentCarDetails)
                .HasForeignKey(rcd => rcd.CarID);


            modelBuilder.Entity<Payment>()
                .HasOne(p => p.RentCar)
                .WithMany(rc => rc.Payments)
                .HasForeignKey(p => p.RentCarID);


            //modelBuilder.Entity<DuePayment>()
            //   .HasOne(p => p.RentCar)
            //   .WithMany(rc => rc.DuePayments)
            //   .HasForeignKey(p => p.RentCarID);


            //Configure the RentCar relationship without cascading delete
            modelBuilder.Entity<DuePayment>()
                .HasOne(dp => dp.RentCar)
                .WithMany(r => r.DuePayments)
                .HasForeignKey(dp => dp.RentCarID)
                .OnDelete(DeleteBehavior.Restrict); // or .OnDelete(DeleteBehavior.NoAction);

           // Configure the RentCarDetail relationship with cascading delete
            modelBuilder.Entity<DuePayment>()
                .HasOne(dp => dp.RentCarDetail)
                .WithMany()
                .HasForeignKey(dp => dp.RentCarDetailID)
                .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<Payment>()
            //.HasOne(rc => rc.RentCarDetail)
            //.WithOne(p => p.Payment)
            //.HasForeignKey<Payment>(p => p.RentCarDetailID);


            //     modelBuilder.Entity<RentCar>()
            //.HasOne(rc => rc.Member)
            //.WithMany(m => m.RentCars)
            //.HasForeignKey(rc => rc.MemberID);

            //     RentCar to RentCarDetail Relationship
            //     modelBuilder.Entity<RentCar>()
            //         .HasMany(rc => rc.RentCarDetails)
            //         .WithOne(rcd => rcd.RentCar)
            //         .HasForeignKey(rcd => rcd.RentCarID)
            //         .OnDelete(DeleteBehavior.Cascade);  // Default behavior, but you can change it if needed.

            //     RentCarDetail to Car Relationship
            //     modelBuilder.Entity<RentCarDetail>()
            //         .HasOne(rcd => rcd.Car)
            //         .WithMany(c => c.RentCarDetails)
            //         .HasForeignKey(rcd => rcd.CarID);

            //     RentCar to Payment Relationship
            //     modelBuilder.Entity<Payment>()
            //         .HasOne(p => p.RentCar)
            //         .WithMany(rc => rc.Payments)
            //         .HasForeignKey(p => p.RentCarID)
            //         .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete on this path.

            //     RentCarDetail to Payment Relationship
            //     modelBuilder.Entity<Payment>()
            //         .HasOne(p => p.RentCarDetail)
            //         .WithMany(rcd => rcd.Payments)
            //         .HasForeignKey(p => p.RentCarDetailID)
            //         .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete on this path.

        }
    }
}
