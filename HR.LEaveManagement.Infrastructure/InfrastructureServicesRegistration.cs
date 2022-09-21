using HR.LeaveManagement.Application.Infrastructure.Contracts;
using HR.LeaveManagement.Application.Models;
using HR.LEaveManagement.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.In
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailSender, EmailSender>();
            return services;
        }
    }
}
