using System;

using Amazon.IoTDeviceGateway.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Auth;

namespace Amazon.IoTDeviceGateway.Model
{
    /// <summary>
    /// Container for the parameters of an MQTT over WebSocket connection
    /// request, contaiing the IoT Device Gatway endpoint address to
    /// connect to.
    /// </summary>
    public class CreateMqttWebSocketUriRequest : AmazonIoTDeviceGatewayRequest
    {
        /// <inheritdoc/>
        protected override AbstractAWSSigner CreateSigner() =>
            new MqttWebSocketAWS4Signer();

        /// <summary>
        /// Your AWS account-specific AWS IoT endpoint. You can use the AWS IoT CLI <a href="https://docs.aws.amazon.com/cli/latest/reference/iot/describe-endpoint.html">describe-endpoint</a> command to find this endpoint.
        /// </summary>
        /// <seealso href="https://docs.aws.amazon.com/iot/latest/developerguide/mqtt-ws.html">MQTT over the WebSocket Protocol</seealso>
        public string? EndpointAddress { get; set; }
    }
}
