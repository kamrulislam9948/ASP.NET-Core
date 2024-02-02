namespace R54_M8_Class_09_Work_01.HostedServices
{
    public class SeederHostedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        public SeederHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        /*The constructor of the SeederHostedService class takes one parameter, serviceProvider. 
          serviceProvider is an instance of the IServiceProvider interface, which is used to create and 
          manage instances of services within the ASP.NET Core application.*/
        public async Task StartAsync(CancellationToken cancellationToken) /*This method is part of the IHostedService interface and is called when the hosted service is started. It receives a CancellationToken that can be used to gracefully stop the service when requested.*/
        {
            using var scope = serviceProvider.CreateScope(); /*Inside the StartAsync method, a new scope is created using serviceProvider.CreateScope(). This is done to ensure that the dependencies used within this method are properly managed and disposed of when the scope is completed.*/

            var seeder = scope.ServiceProvider.GetRequiredService<IdentityDbSeeder>(); /*Within the scope, an instance of the IdentityDbSeeder class is retrieved using scope.ServiceProvider.GetRequiredService<IdentityDbSeeder>().*/
            
            await seeder.SeedAsync();/*Finally, the SeedAsync() method of the IdentityDbSeeder is called to initiate the database seeding process. This is where roles may be created in the database, as explained in your previous question.*/
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask; /*This method is also part of the IHostedService interface and is called when the hosted service is stopped. In this implementation, it returns Task.CompletedTask, indicating that no specific action needs to be taken when stopping the service.*/
        }
    }
}
