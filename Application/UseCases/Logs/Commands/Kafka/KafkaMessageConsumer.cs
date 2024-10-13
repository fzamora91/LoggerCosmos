using Application.Common.Interfaces;
using Application.Common.Utils;
using Application.UseCases.Logs.Commands.CreateLog;
using Azure.Core;
using Confluent.Kafka;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Application.UseCases.Logs.Commands.Kafka
{
    public class KafkaMessageConsumer : IMessageReceiver<OrderCommand>
    {

        //private readonly IConsumer<Null, string> _consumer;
        private ILogService logService;

        public KafkaMessageConsumer()
        {
            

            

           

        }

        public async Task<string> ReceiveMessageAsync(OrderCommand command)
        {
            bool result = false;


            var config = new ConsumerConfig
            {
                
                BootstrapServers = "lmsKafkareto3.servicebus.windows.net:9093",
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = "Endpoint = sb://lmskafkareto3.servicebus.windows.net/;SharedAccessKeyName=test;SharedAccessKey=73iqFQK6fBDbNWDMdJ8+2SRWbPItwFv+a+AEhC84p4Q=;EntityPath=eventoreto3",
                //SaslPassword = "Endpoint=sb://lmskafkareto3.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=r5h+sikd7rmVe7UpX4maLxCPK4hJ8Ly5W+AEhAWu+ho=",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = "order-group",
            };


           var _consumer = new ConsumerBuilder<Null, string>(config).Build();

            CancellationTokenSource token = new();
            _consumer.Subscribe("eventoreto3");

             var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(70));

             //var consumeResult = _consumer.Consume(token.Token);

             if(consumeResult!=null)
             {


                    if (consumeResult.Message != null)
                    {
                        // Aquí procesas el mensaje recibido
                        System.Console.WriteLine($"Mensaje recibido: {consumeResult.Message.Value}");

                        string mensaje = consumeResult.Message.Value;


                        var log = JsonConvert.DeserializeObject<Log>(consumeResult.Message.Value);


                        string[] partes = mensaje.Split(',');
                        string order_id = partes[0].Split(':')[1];
                        string product = partes[1].Split(':')[1];


                        // Simulamos procesamiento
                        //await Task.Delay(1000);


                        var r = await logService.LogInformationAsync(order_id, product);


                        if (r.Created)
                            return "Se ha realizado la insersion desde kafka";

                    }


             }

                
            _consumer.Commit(consumeResult);


            return "error";
            
        }
    }


    public class OrderCommand
    {
        public string OrderId { get; set; }
        public string ProductName { get; set; }
    }
}
