using Amazon.Runtime.Internal.Auth;

namespace Amazon.IoTDeviceGateway.Model
{
    public class CreateMqttWebSocketUriRequest : AmazonIoTDeviceGatewayRequest
    {
        public string EndpointAddress { get; set; }

        /// <inheritdoc />
        protected override AbstractAWSSigner CreateSigner() =>
            new AWS4Signer(signPayload: false);
    }
}
