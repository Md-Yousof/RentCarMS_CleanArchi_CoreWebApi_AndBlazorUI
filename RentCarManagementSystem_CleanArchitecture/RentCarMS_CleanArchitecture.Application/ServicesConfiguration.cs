using Microsoft.Extensions.DependencyInjection;
using RentCarMS_CleanArchitecture.Application.Cars.CarServices;
using RentCarMS_CleanArchitecture.Application.DuePayments.DuePaymentServices;
using RentCarMS_CleanArchitecture.Application.Members.Service;
using RentCarMS_CleanArchitecture.Application.Payments.PaymentServices;
using RentCarMS_CleanArchitecture.Application.RentCarDetails.RentCarDetailServices;
using RentCarMS_CleanArchitecture.Application.RentCars.RentCarServices;
using RentCarMS_CleanArchitecture.Domain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<MemberService>();
            services.AddScoped<CareService>();
            services.AddScoped<RentCarService>();
            services.AddScoped<PaymentService>();
            services.AddScoped<RentCarDetailService>();
            services.AddScoped<DuePaymentService>();

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            return services;
        }
    }
}
