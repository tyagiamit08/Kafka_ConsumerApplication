using System;
using System.Threading.Tasks;
using Kafka.Interfaces;
using Kafka.Messages.UserRegistration;

namespace Kafka_ConsumerApplication.Core.kafkaEvents.UserRegistration.Handlers
{
	public class UserRegisteredHandler : IKafkaHandler<string, UserRegistered>
	{
		public Task HandleAsync(string key, UserRegistered value)
		{
			Console.WriteLine($"Consuming UserRegistered topic message with the below data\n FirstName: {value.FirstName}\n LastName: {value.LastName}\n UserName: {value.UserName}\n EmailId: {value.EmailId}");
			return Task.CompletedTask;
		}
	}
}

