using Amazon.IoTDeviceGateway.Runtime.Internal;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;

using System;
using System.Net.Http;

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

        /// <inheritdoc cref="IMarshaller{IRequest, CreateMqttWebSocketUriRequest}.Marshall" />
        public IRequest Marshall(CreateMqttWebSocketUriRequest input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            var request = new NonHttpRequest(input, AmazonIoTDeviceGatewayConfig.ServiceName)
            {
                HttpMethod = HttpMethod.Get.Method,
                Endpoint = new Uri($"wss://{input.EndpointAddress}"),
                ResourcePath = "/mqtt",
                UseSigV4 = true,
                Content = Array.Empty<byte>(),
                UseQueryString = true
            };
            return request;
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
