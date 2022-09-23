using HR.LeaveManagement.Application;
using HR.LeaveManagement.In;
using HR.LeaveManagement.Persistence;

public class Startup
{
    public Startup(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureApplicationServices();
        services.ConfigureInfrastructureServices(Configuration);
        services.ConfigurePersistenceServices(Configuration);
    }

    public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}