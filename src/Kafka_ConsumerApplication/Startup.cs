using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Confluent.Kafka;
using Kafka.Consumer;
using Kafka.Interfaces;
using Kafka.Messages.UserRegistration;
using Kafka_ConsumerApplication.Core.kafkaEvents.UserRegistration.Consumers;
using Kafka_ConsumerApplication.Core.kafkaEvents.UserRegistration.Handlers;

namespace Kafka_ConsumerApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SurveyAudit API", Version = "v1" });
				c.EnableAnnotations();
			});

			services.AddControllers();

			var clientConfig = new ClientConfig()
			{
				BootstrapServers = Configuration["Kafka:ClientConfigs:BootstrapServers"]
			};

			var consumerConfig = new ConsumerConfig(clientConfig)
			{
				GroupId = "SourceApp",
				EnableAutoCommit = true,
				AutoOffsetReset = AutoOffsetReset.Earliest,
				StatisticsIntervalMs = 5000,
				SessionTimeoutMs = 6000
			};

			services.AddSingleton(consumerConfig);

			services.AddScoped<IKafkaHandler<string, UserRegistered>, UserRegisteredHandler>();
			services.AddSingleton(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));
			services.AddHostedService<UserRegisteredConsumer>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "SurveyAudit API");
				c.RoutePrefix = string.Empty;
			});

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
