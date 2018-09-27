using System.Collections.Generic;

namespace Amazon.IoTDeviceGateway.Model
{
    public class AmazonIoTDeviceGatewaySigningDetails
    {
        public string AccessKeyId { get; set; }
        public string AuthorizationHeader { get; set; }
        public string QueryParameters { get; set; }
        public string ISO8601Date { get; set; }
        public string ISO8601DateTime { get; set; }
        public string Scope { get; set; }
        public string Signature { get; set; }
        public IDictionary<string, string> SignedHeaders { get; set; }
    }
}
