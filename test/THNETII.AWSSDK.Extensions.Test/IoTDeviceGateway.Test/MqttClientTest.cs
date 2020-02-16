using Amazon.TestParameters;

using MQTTnet;
using MQTTnet.Client;

using Xunit;

namespace Amazon.IoTDeviceGateway.Test
{
    public static class MqttClientTest
    {
        private static readonly MqttFactory mqttFactory = new MqttFactory();

        [SkippableFact]
        public static void ConnectIoTDeviceGatewayWithWebSocketsMqtt()
        {
            var credentials = TelenorMicCredentials.AWSCredentials;
            Skip.If(credentials is null, "No AWS Credentials configured");

            IMqttClientOptions mqttOptions;
            using (var iotDeviceGatewayClient = new AmazonIoTDeviceGatewayClient(credentials, RegionEndpoint.GetBySystemName(TelenorMicCredentials.RegionSystemName)))
            {
                var uriDetails = iotDeviceGatewayClient.CreateMqttWebSocketUriAsync(new Model.CreateMqttWebSocketUriRequest
                {
                    EndpointAddress = TelenorMicCredentials.IotEndpoint
                }).ConfigureAwait(false).GetAwaiter().GetResult();

                mqttOptions = new MqttClientOptionsBuilder()
                    .WithTls()
                    .WithWebSocketServer(uriDetails.RequestUri.ToString())
                    .Build();

                if (mqttOptions.ChannelOptions is MqttClientWebSocketOptions webSocketOptions)
                {
                    webSocketOptions.RequestHeaders = uriDetails.Headers;
                }
            }
            using var mqttClient = mqttFactory.CreateMqttClient();
            var connectResult = mqttClient.ConnectAsync(mqttOptions)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            mqttClient.DisconnectAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
