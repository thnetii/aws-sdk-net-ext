namespace Amazon.IoTDeviceGateway.Model
{
    public class AmazonIoTDeviceGatewaySigningDetails
    {
        public string AccessKeyId { get; set; }
        public string ForAuthorizationHeader { get; set; }
        public string ForQueryParameters { get; set; }
        public string ISO8601Date { get; set; }
        public string ISO8601DateTime { get; set; }
        public string Scope { get; set; }
        public string Signature { get; set; }
        public string SignedHeaders { get; set; }
    }
}
