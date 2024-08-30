using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentCarMS_CleanArchitecture.Application.Members.Service;
using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.DuePayments;
using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using RentCarMS_CleanArchitecture.Infrastructure.Cars;
using RentCarMS_CleanArchitecture.Infrastructure.DATA.Context;
//using RentCarMS_CleanArchitecture.Infrastructure.DuePayments;
using RentCarMS_CleanArchitecture.Infrastructure.Members;
using RentCarMS_CleanArchitecture.Infrastructure.Payments;
using RentCarMS_CleanArchitecture.Infrastructure.RentCarDetails;
using RentCarMS_CleanArchitecture.Infrastructure.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Infrastructure
{
    public static class ServicesConfiguration
    {
       public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
       {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddScoped(typeof(ICarRepository), typeof(CarRepository));
            services.AddScoped(typeof(IRentCarDetailRepository), typeof(RentCarDetailRepository));
            services.AddScoped(typeof(IRentCarRepository), typeof(RentCarRepository));
            services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
           // services.AddScoped(typeof(IDuePaymentRepository), typeof(DuePaymentRepository));

            //services.AddScoped<MemberService>();
            return services;
       }

    }
}
