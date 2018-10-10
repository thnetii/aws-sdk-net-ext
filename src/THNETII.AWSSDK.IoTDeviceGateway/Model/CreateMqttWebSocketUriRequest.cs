using Amazon.IoTDeviceGateway.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Auth;

namespace Amazon.IoTDeviceGateway.Model
{
    public class CreateMqttWebSocketUriRequest : AmazonIoTDeviceGatewayRequest
    {
        protected override AbstractAWSSigner CreateSigner() =>
            new MqttWebSocketAWS4Signer();

        public string EndpointAddress { get; set; }
    }
}
