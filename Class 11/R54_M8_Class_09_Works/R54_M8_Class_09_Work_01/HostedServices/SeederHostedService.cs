namespace R54_M8_Class_09_Work_01.HostedServices
{
    public class SeederHostedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        public SeederHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IdentityDbSeeder>();
            await seeder.SeedAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
