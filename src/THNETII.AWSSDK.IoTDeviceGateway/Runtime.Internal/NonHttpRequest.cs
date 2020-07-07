using System.Diagnostics;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.IoTDeviceGateway.Runtime.Internal
{
    public class NonHttpRequest : DefaultRequest
    {
        [DebuggerStepThrough]
        public NonHttpRequest(AmazonWebServiceRequest request, string serviceName)
            : base(request, serviceName)
        { }
    }
}
