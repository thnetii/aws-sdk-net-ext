using MQTTnet.Client;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Amazon.IoTDeviceGateway
{
    public static class AmazonIoTDeviceGatewayClientMqttExtensions
    {
        private static readonly Uri localhostUri = new Uri("http://localhost/");

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

        public static async Task<MqttClientConnectResult> ConnectMqttWebSocketsClientAsync(this AmazonIoTDeviceGatewayClient client,
            string iotEndpointAddress, IMqttClient mqttClient, CancellationToken cancelToken = default)
        {
            var optionsTask = client.CreateMqttWebSocketClientOptionsAsync(iotEndpointAddress, cancelToken);
            return await mqttClient.ConnectAsync(await optionsTask.ConfigureAwait(continueOnCapturedContext: false))
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
