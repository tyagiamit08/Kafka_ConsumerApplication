using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Constants;
using Kafka.Interfaces;
using Kafka.Messages.UserRegistration;
using Microsoft.Extensions.Hosting;

namespace Kafka_ConsumerApplication.Core.kafkaEvents.UserRegistration.Consumers
{
	public class UserRegisteredConsumer : BackgroundService
	{
		private readonly IKafkaConsumer<string, UserRegistered> _consumer;
		public UserRegisteredConsumer(IKafkaConsumer<string, UserRegistered> kafkaConsumer)
		{
			_consumer = kafkaConsumer;
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				await _consumer.Consume(KafkaTopics.UserRegistered, stoppingToken);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{(int)HttpStatusCode.InternalServerError} ConsumeFailedOnTopic - {KafkaTopics.UserRegistered}, {ex}");
			}
		}

		public override void Dispose()
		{
			_consumer.Close();
			_consumer.Dispose();

			base.Dispose();
		}
	}
}
