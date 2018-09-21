using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using System;

namespace Amazon.IoTDeviceGateway.Model.Internal.MarshallTransformations
{
    public class CreateMqttWebSocketUriRequestMarshaller :
        IMarshaller<IRequest, CreateMqttWebSocketUriRequest>,
        IMarshaller<IRequest, AmazonWebServiceRequest>
    {
        /// <inheritdoc />
        /// <exception cref="InvalidCastException" />
        public IRequest Marshall(AmazonWebServiceRequest input) =>
            Marshall((CreateMqttWebSocketUriRequest)input);

        /// <inheritdoc />
        public IRequest Marshall(CreateMqttWebSocketUriRequest input)
        {
            return new DefaultRequest(input, AmazonIoTDeviceGatewayConfig.ServiceName)
            {
                HttpMethod = "GET",
                Endpoint = new Uri($"wss://{input.EndpointAddress}"),
                ResourcePath = "/mqtt"
            };
        }

        internal static CreateMqttWebSocketUriRequestMarshaller GetInstance() =>
            Instance;

        /// <summary>
        /// Gets the singleton.
        /// </summary>  
        public static CreateMqttWebSocketUriRequestMarshaller Instance { get; } =
            new CreateMqttWebSocketUriRequestMarshaller();
    }
}
