using System;
using System.IO;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.IoTDeviceGateway.Model.Internal.MarshallTransformations
{
    public abstract class AmazonIoTDeviceGatewayResponseUnmarshaller : ResponseUnmarshaller
    {
        public abstract AmazonIoTDeviceGatewayResponse CreateResponse();

        public override AmazonWebServiceResponse Unmarshall(UnmarshallerContext input)
        {
            throw new NotSupportedException();
        }

        protected override UnmarshallerContext ConstructUnmarshallerContext(Stream responseStream, bool maintainResponseBody, IWebResponseData response)
        {
            throw new NotSupportedException();
        }
    }
}
