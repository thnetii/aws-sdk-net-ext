using Amazon.Runtime;

using System;
using System.Collections.Generic;

namespace Amazon.IoTDeviceGateway.Model
{
    /// <summary>
    /// Container for the information necessary to perform an authenticated
    /// connection requrest to an IoT Device Gateway endpoint using the
    /// <a href="https://docs.aws.amazon.com/iot/latest/developerguide/mqtt-ws.html">MQTT over WebSocket Protocol</a>
    /// </summary>
    public class CreateMqttWebSocketUriResponse : AmazonWebServiceResponse
    {
        /// <inheritdoc/>
        public CreateMqttWebSocketUriResponse() : base() { }

        internal CreateMqttWebSocketUriResponse(
            Uri requestUri, IDictionary<string, string> headers
            ) : this() =>
            (RequestUri, Headers) = (requestUri, headers);

        /// <summary>
        /// The requested WebSocket protol Uri with complete
        /// AWS Signature Version 4 Signing Information added in the query
        /// parameters.
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// AWS Signature Version 4 Headers that can be used with the original
        /// endpoint address instead of <see cref="RequestUri"/> for clients
        /// that have maximum-URL-length restrictions.
        /// </summary>
        public IDictionary<string, string> Headers { get; }
    }
}
