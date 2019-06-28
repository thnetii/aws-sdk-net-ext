using System.Threading.Tasks;

using Amazon.TestParameters;

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

using Xunit;

namespace Amazon.IoTDeviceGateway.Test
{
    public static class MqttClientTest
    {
        private static readonly MqttFactory mqttFactory = new MqttFactory();

        [SkippableFact]
        public static async Task ConnectIoTDeviceGatewayWithWebSocketsMqtt()
        {
            var credentials = Credentials.AWSCredentials;
            Skip.If(credentials is null, "No AWS Credentials configured");
            Skip.If(IotEndpointAddress.Region is null, "No AWS region configured");

            IMqttClientOptions mqttOptions;
            using (var iotDeviceGatewayClient = new AmazonIoTDeviceGatewayClient(credentials, IotEndpointAddress.Region))
            {
                var uriDetails = await iotDeviceGatewayClient.CreateMqttWebSocketUriAsync(new Model.CreateMqttWebSocketUriRequest
                {
                    EndpointAddress = IotEndpointAddress.EndpointAddress
                });

                mqttOptions = new MqttClientOptionsBuilder()
                    .WithTls()
                    .WithWebSocketServer(uriDetails.RequestUri.ToString())
                    .Build();

                if (mqttOptions.ChannelOptions is MqttClientWebSocketOptions webSocketOptions)
                {
                    webSocketOptions.RequestHeaders = uriDetails.Headers;
                }
            }
            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var connectResult = await mqttClient.ConnectAsync(mqttOptions);

                await mqttClient.DisconnectAsync();
            }
        }
    }
}
