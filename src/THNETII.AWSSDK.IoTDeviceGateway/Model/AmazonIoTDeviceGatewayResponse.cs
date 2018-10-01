using Amazon.Runtime;
using System;
using System.Collections.Generic;

namespace Amazon.IoTDeviceGateway.Model
{
    public class AmazonIoTDeviceGatewayResponse : AmazonWebServiceResponse
    {
        public Uri RequestUri { get; set; }
        public IDictionary<string, string> Headers { get; set; }
    }
}
