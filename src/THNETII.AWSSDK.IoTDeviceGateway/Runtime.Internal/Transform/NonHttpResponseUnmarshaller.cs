using System;
using System.IO;
using System.Net;

using Amazon.Runtime;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;

namespace Amazon.IoTDeviceGateway.Runtime.Internal.Transform
{
    public abstract class NonHttpResponseUnmarshaller : ResponseUnmarshaller, IResponseUnmarshaller<AmazonWebServiceResponse, NonHttpUnmarshallerContext>
    {
        public override AmazonWebServiceResponse Unmarshall(UnmarshallerContext input) =>
            Unmarshall((NonHttpUnmarshallerContext)input);

        public AmazonServiceException UnmarshallException(NonHttpUnmarshallerContext input, Exception innerException, HttpStatusCode statusCode) =>
            base.UnmarshallException(input, innerException, statusCode);

        public abstract AmazonWebServiceResponse Unmarshall(NonHttpUnmarshallerContext input);

        protected override UnmarshallerContext ConstructUnmarshallerContext(Stream responseStream, bool maintainResponseBody, IWebResponseData response) =>
            new NonHttpUnmarshallerContext(response as NonHttpResponseData);

        public override UnmarshallerContext CreateContext(IWebResponseData response, bool readEntireResponse, Stream stream, RequestMetrics metrics)
        {
            return ConstructUnmarshallerContext(stream,
                ShouldReadEntireResponse(response, readEntireResponse),
                response);
        }

        protected override bool ShouldReadEntireResponse(IWebResponseData response, bool readEntireResponse) =>
            false;
    }
}
