using Amazon.Runtime;
using System;

namespace Amazon.IoTDeviceGateway.Model
{
    public class AmazonIoTDeviceGatewayResponse : AmazonWebServiceResponse
    {
        public Uri RequestUri { get; set; }
        public AmazonIoTDeviceGatewaySigningDetails SigningDetails { get; set; }
    }
}
