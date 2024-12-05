using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using MassTransit;
// using Microsoft.Extensions.Caching.Distributed;

using Serilog;
using Serilog.Core;

namespace CueCoach.Consumers
{
    /// <summary>
    /// The consumer 
    /// </summary>
    public class Consumer : IConsumer<MessageContract>
    {

    #region 🤫 Private Variables 🤫

        private readonly IBus Bus;
        // private readonly IDistributedCache DistributedCache;
        readonly Serilog.ILogger Logger;

    #endregion

    #region 🚧Constructors🚧

        public CommunicationsConsumer(
            IBus bus,
            // IDistributedCache distributedCache,
            Serilog.ILogger logger
        )
        {
            Bus = bus;
            // DistributedCache = distributedCache;
            Logger = logger;
        }

    #endregion

    #region 📢 Methods 📢

        /// <summary>
        /// This consumer will take the message and send it to the appropriate channel.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Consume(
            ConsumeContext<MessageContract> context
        )
        {
	        // Only one consumption of this type allowed for the CommunicationsApi (Regardless of node). Pick a wait interval and check to see if a friend got to this first...
            // todo: there probably is a way to do this cleaner...
            var randomTo1k = new Random().Next(1000, 3000);
            Thread.Sleep(randomTo1k);

            var lockKey = $"MessageContract:{context.MessageId}:Lock";
            var lockValue = await DistributedCache.GetStringAsync(lockKey) ?? string.Empty;
            if (lockValue == "Locked")
            {
                Logger
                    .Information($"Another server is taking care of this contract. All done.");
                return;
            }

            // await DistributedCache.SetStringAsync(lockKey, "Locked");

            if (string.IsNullOrWhiteSpace(context.TransactionId))
            {
                context.TransactionId = $"{Guid.NewGuid():N}";
            }

            var transactionId = context.TransactionId;
            try
            {
                //continue here
            }
            catch (Exception ex)
            {
                Logger.Fatal($" failed to process Contract message in Consumer. {ex.Message}");
            }

            // await DistributedCache.RemoveAsync(lockKey);
        }

    #endregion
    }
}