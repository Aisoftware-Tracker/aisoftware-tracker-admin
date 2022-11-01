using Aisoftware.Tracker.Worker.DAO.Common.Base;
using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Worker.Reports;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IBaseDAO<User> _userDAO;

    private Timer _timer;
    private int _index = 20;

    public Worker(ILogger<Worker> logger, IBaseDAO<User> userDAO)
    {
        _logger = logger;
        _userDAO = userDAO;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //     await Task.Delay(1000, stoppingToken);
        // }

         _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

        // return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        _index++;

        var user = new User {
            Id = _index,
            Name = $"igor{_index}",
            Email = $"igor{_index}@email.com"
        };

        await _userDAO.Save(user);
        var response = await _userDAO.FindById(_index);

        Console.WriteLine($"{DateTime.Now:o} =>  {_index}");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"Process finished {nameof(Worker)}");

        return Task.CompletedTask;
    }
}
