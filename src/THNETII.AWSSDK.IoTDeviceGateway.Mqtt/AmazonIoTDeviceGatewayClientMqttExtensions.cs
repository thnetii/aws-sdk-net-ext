using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;

namespace Amazon.IoTDeviceGateway
{
    /// <summary>
    /// Provides MQTT.NET specific extenions for the IoT Device Gateway client.
    /// </summary>
    public static class AmazonIoTDeviceGatewayClientMqttExtensions
    {
        private static readonly Uri localhostUri = new Uri("http://localhost/");

        /// <summary>
        /// Provides the MQTT client options to create an authenticated MQTT
        /// over WebSocket connection to an AWS IoT Device Gateway endpoint.
        /// </summary>
        /// <param name="client">The authenticated AWS IoT Device Gateway client.</param>
        /// <param name="iotEndpointAddress">The AWS account-specific AWS IoT endpoint address.</param>
        /// <param name="cancelToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public static async Task<IMqttClientOptions> CreateMqttWebSocketClientOptionsAsync(this AmazonIoTDeviceGatewayClient client, string iotEndpointAddress, CancellationToken cancelToken = default)
        {
            if (client is null)
                throw new ArgumentNullException(nameof(client));
            var uriDetails = await client.CreateMqttWebSocketUriAsync(new Model.CreateMqttWebSocketUriRequest
            {
                EndpointAddress = iotEndpointAddress
            }, cancelToken).ConfigureAwait(continueOnCapturedContext: false);

            var optionsBuilder = new MqttClientOptionsBuilder();
            optionsBuilder = optionsBuilder.WithTls();
            optionsBuilder = optionsBuilder.WithWebSocketServer(uriDetails.RequestUri.ToString());

            IWebProxy iProxy = client.Config.GetWebProxy();
            if (!(iProxy is null))
            {
                Uri proxyUri;
                if (iProxy is Amazon.Runtime.Internal.Util.WebProxy awssdkProxy)
                    proxyUri = awssdkProxy.ProxyUri;
                else
                    proxyUri = new Uri("http://" + client.Config.ProxyHost + ":" + client.Config.ProxyPort);
                var iCreds = iProxy.Credentials ?? client.Config.ProxyCredentials;
                var netCreds = iCreds?.GetCredential(proxyUri, default);
                optionsBuilder = optionsBuilder.WithProxy(proxyUri.ToString(),
                    username: netCreds?.UserName, password: netCreds?.Password, domain: netCreds?.Domain,
                    bypassOnLocal: iProxy.IsBypassed(localhostUri)
                    );
            }

            var options = optionsBuilder.Build();

            if (options.ChannelOptions is MqttClientWebSocketOptions webSocketOptions)
            {
                webSocketOptions.RequestHeaders = uriDetails.Headers;
            }

            return options;
        }

        /// <summary>
        /// Connects an MQTT client to an AWS IoT Endpoint using an authenticated
        /// MQTT over WebSocket connection request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="iotEndpointAddress">The AWS account-specific AWS IoT endpoint address.</param>
        /// <param name="mqttClient">An MQTT.NET client instance to connect.</param>
        /// <param name="cancelToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public static async Task<MqttClientAuthenticateResult> ConnectMqttWebSocketsClientAsync(this AmazonIoTDeviceGatewayClient client,
            string iotEndpointAddress, IMqttClient mqttClient, CancellationToken cancelToken = default)
        {
            if (mqttClient is null)
                throw new ArgumentNullException(nameof(mqttClient));

            var optionsTask = client.CreateMqttWebSocketClientOptionsAsync(iotEndpointAddress, cancelToken);
            return await mqttClient.ConnectAsync(await optionsTask.ConfigureAwait(continueOnCapturedContext: false))
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
