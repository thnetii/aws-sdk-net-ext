using Amazon.IoTDeviceGateway.Runtime.Internal.Auth;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Auth;

namespace Amazon.IoTDeviceGateway.Model
{
    /// <summary>
    /// Base class for IoT Device Gateway requests.
    /// </summary>
    public class AmazonIoTDeviceGatewayRequest : AmazonWebServiceRequest
    {
        /// <inheritdoc />
        protected override AbstractAWSSigner CreateSigner() =>
            new MqttWebSocketAWS4Signer();
    }
}
