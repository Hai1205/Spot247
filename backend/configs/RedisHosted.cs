namespace Backend.Configs;

public class RedisHosted(RedisHelper redisHelper) : IHostedService
{
    private readonly RedisHelper _redisHelper = redisHelper;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var redisUrl = Variable.Enviroments.REDIS_URL;

        if (string.IsNullOrEmpty(redisUrl))
        {
            Console.WriteLine("⚠️ REDIS_URL is not configured in environment variables");
            return;
        }

        try
        {
            await _redisHelper.Connect(redisUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to connect to Redis: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
