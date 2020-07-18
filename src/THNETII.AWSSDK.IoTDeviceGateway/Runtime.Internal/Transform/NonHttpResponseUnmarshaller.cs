using System;
using System.IO;
using System.Net;

using Amazon.Runtime;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.IoTDeviceGateway.Runtime.Internal.Transform
{
    public abstract class NonHttpResponseUnmarshaller : ResponseUnmarshaller, IResponseUnmarshaller<AmazonWebServiceResponse, NonHttpUnmarshallerContext>
    {
        public override AmazonWebServiceResponse Unmarshall(UnmarshallerContext input) =>
            Unmarshall((NonHttpUnmarshallerContext)input);

        AmazonServiceException IResponseUnmarshaller<AmazonWebServiceResponse, NonHttpUnmarshallerContext>.UnmarshallException(
            NonHttpUnmarshallerContext input, Exception innerException,
            HttpStatusCode statusCode) =>
            UnmarshallException(input, innerException, statusCode);

        public abstract AmazonWebServiceResponse Unmarshall(NonHttpUnmarshallerContext input);

        protected override UnmarshallerContext ConstructUnmarshallerContext(
            Stream responseStream, bool maintainResponseBody,
            IWebResponseData response, bool isException) =>
            new NonHttpUnmarshallerContext((NonHttpResponseData)response);

        protected override bool ShouldReadEntireResponse(IWebResponseData response, bool readEntireResponse) =>
            false;
    }
}
