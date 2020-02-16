using System;
using System.Collections.Generic;

namespace Amazon.IoTDeviceGateway.Model
{
    public class CreateMqttWebSocketUriResponse : AmazonIoTDeviceGatewayResponse
    {
        public CreateMqttWebSocketUriResponse() : base() { }
        internal CreateMqttWebSocketUriResponse(
            Uri requestUri, IDictionary<string, string> headers
            ) : this() =>
            (RequestUri, Headers) = (requestUri, headers);

        public Uri RequestUri { get; set; }
        public IDictionary<string, string> Headers { get; }
    }
}
