using Amazon.Runtime.Internal.Transform;

namespace Amazon.IoTDeviceGateway.Runtime.Internal.Transform
{
    public class NonHttpUnmarshallerContext : UnmarshallerContext
    {
        public NonHttpUnmarshallerContext(NonHttpResponseData nonHttpResponseData) : base()
        {
            ResponseData = nonHttpResponseData;
            WebResponseData = nonHttpResponseData;
        }

        public new NonHttpResponseData ResponseData { get; }

        public override string CurrentPath => default;

        public override int CurrentDepth => default;

        public override bool IsStartElement => default;

        public override bool IsEndElement => default;

        public override bool IsStartOfDocument => default;

        public override bool Read() => default;

        public override string ReadText() => default;
    }
}
