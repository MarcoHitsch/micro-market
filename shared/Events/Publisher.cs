using EasyNetQ;

namespace shared.Events
{
    public class Publisher
    {
        private readonly IBus rabbitMqBus;

        public Publisher(IBus rabbitMqBus)
        {
            this.rabbitMqBus = rabbitMqBus;
        }

        public async Task PublishAsync<T>(T pubEvent)
        {
            System.Console.WriteLine($"Publishing Event {pubEvent}");
            await this.rabbitMqBus.PubSub.PublishAsync(pubEvent, typeof(T).Name);
        }
    }
}
