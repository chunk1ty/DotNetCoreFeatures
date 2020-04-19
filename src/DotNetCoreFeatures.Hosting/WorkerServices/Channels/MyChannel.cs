using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DotNetCoreFeatures.Hosting.WorkerServices.Channels
{
    public class MyChannel
    {
        private const int MaxMessagesInChannel = 100;

        private readonly Channel<string> _channel;
        private readonly ILogger<MyChannel> _logger;

        public MyChannel(ILogger<MyChannel> logger)
        {
            var options = new BoundedChannelOptions(MaxMessagesInChannel)
            {
                SingleWriter = false,
                SingleReader = true
            };

            _channel = Channel.CreateBounded<string>(options);

            _logger = logger;
        }

        public async Task<bool> AddMessage(string message, CancellationToken ct = default)
        {
            while (await _channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                if (_channel.Writer.TryWrite(message))
                {
                    _logger.LogInformation($"Message was sent to the channel {message}");

                    return true;
                }
            }

            return false;
        }

        public IAsyncEnumerable<string> ReadAllAsync(CancellationToken ct = default) =>
            _channel.Reader.ReadAllAsync(ct);
       
    }
}
