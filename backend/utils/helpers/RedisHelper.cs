using StackExchange.Redis;

namespace Backend.Utils.Helpers
{
    public class RedisHelper
    {
        private ConnectionMultiplexer? _connection;
        private readonly Lock _lock = new();
        private string? _redisUrl;

        public IDatabase Database => _connection?.GetDatabase() ?? throw new ExceptionCustom("Redis connection is not established");

        public async Task Connect(
            string redisUrl,
            int maxRetries = 5,
            int delay = 5000)
        {
            _redisUrl = redisUrl;
            int retries = 0;

            Console.WriteLine($"⏳ Attempting to connect to Redis at {redisUrl}");

            while (retries < maxRetries)
            {
                try
                {
                    lock (_lock)
                    {
                        if (_connection == null || !_connection.IsConnected)
                        {
                            var options = ConfigurationOptions.Parse(_redisUrl!);
                            options.AbortOnConnectFail = false;
                            _connection = ConnectionMultiplexer.Connect(options);
                        }
                    }

                    if (_connection.IsConnected)
                    {
                        Console.WriteLine("✅ Redis connected successfully");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("❌ Redis connection object created but not connected");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Redis connection error: {ex.Message}");
                }

                retries++;
                if (retries >= maxRetries)
                {
                    Console.WriteLine("🚨 Max retries reached for Redis connection.");
                    // Don't exit the application, just log the error
                    return;
                }

                Console.WriteLine($"⏳ Retrying Redis connection in {delay}ms... (Attempt {retries}/{maxRetries})");
                await Task.Delay(delay);
            }
        }

        public async Task StoreValue(string key, string value, TimeSpan timeSpan)
        {
            try
            {
                await Database.StringSetAsync(key, value, timeSpan);
            }
            catch (Exception ex)
            {
                throw new ExceptionCustom(ex.Message);
            }
        }

        public async Task<string> GetValue(string key)
        {
            try
            {
                if (_connection == null)
                {
                    Console.WriteLine($"❌ Redis connection is null when getting key: {key}");
                    throw new ExceptionCustom("Redis connection is not established");
                }

                if (!_connection.IsConnected)
                {
                    Console.WriteLine($"❌ Redis connection is not connected when getting key: {key}");
                    throw new ExceptionCustom("Redis connection is not connected");
                }

                Console.WriteLine($"🔍 Getting Redis key: {key}");
                var redisValue = await Database.StringGetAsync(key);
                Console.WriteLine($"📄 Redis key {key} value: {(redisValue.IsNullOrEmpty ? "empty" : "found")}");

                return redisValue.IsNullOrEmpty ? "" : redisValue.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Redis GetValue error for key {key}: {ex.Message}");
                throw new ExceptionCustom(ex.Message);
            }
        }
    }
}
