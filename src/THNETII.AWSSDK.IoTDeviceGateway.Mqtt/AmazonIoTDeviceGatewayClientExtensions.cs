using Amazon.Util;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Amazon.IoTDeviceGateway
{
    public static class AmazonIoTDeviceGatewayClientExtensions
    {
        public static async Task<IMqttClientOptions> CreateMqttWebSocketClientOptionsAsync(this AmazonIoTDeviceGatewayClient client, string iotEndpointAddress, CancellationToken cancelToken = default)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));
            var uriDetails = await client.CreateMqttWebSocketUriAsync(new Model.CreateMqttWebSocketUriRequest
            {
                EndpointAddress = iotEndpointAddress
            }, cancelToken).ConfigureAwait(continueOnCapturedContext: false);

            var options = new MqttClientOptionsBuilder()
                .WithWebSocketServer(uriDetails.RequestUri.ToString())
                .Build();

            if (options.ChannelOptions is MqttClientWebSocketOptions webSocketOptions)
            {
                webSocketOptions.RequestHeaders = new Dictionary<string, string>(uriDetails.SigningDetails.SignedHeaders, StringComparer.OrdinalIgnoreCase);
                webSocketOptions.RequestHeaders[HeaderKeys.AuthorizationHeader] = uriDetails.SigningDetails.AuthorizationHeader;
            }

            return options;
        }
    }
}
