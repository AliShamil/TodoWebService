using TodoWebService.Data;

namespace TodoWebService.Services.BackgroundServices
{
    public class NotifyUserService : IHostedService
    {
        private Timer? _timer;
        private readonly IMailService? _mailService;
        private readonly IServiceProvider _provider;
        public NotifyUserService(IServiceProvider provider)
        {
            _provider = provider;
        }

        private void Run(object? state)
        {
            using var scope = _provider.CreateScope();
            var _todoDbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
            var _mailService = scope.ServiceProvider.GetRequiredService<IMailService>();
            var todoitems = _todoDbContext.TodoItems.ToList();
            foreach (var todoitem in todoitems)
            {
                if (todoitem.EndTime.Subtract(DateTime.Today) < TimeSpan.FromDays(1) && !todoitem.IsNotified)
                {
                    _mailService?.SendNotifyMessage(todoitem.User.Email, todoitem);
                    todoitem.IsNotified = true;
                    _todoDbContext.Update(todoitem);
                }
            }
            _todoDbContext.SaveChanges();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("SomeBackgroundService started ....");
            _timer = new Timer(Run, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("SomeBackgroundService stopped ....");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
