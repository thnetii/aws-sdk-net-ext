using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Amazon.IoTDeviceGateway.Runtime.Internal.Transform
{
    public class NonHttpResponseData : IWebResponseData, IHttpResponseBody
    {
        public NonHttpResponseData() : base() => ResponseBody = this;

        public long ContentLength => 0L;

        public string ContentType => default;

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessStatusCode { get; set; }

        public IHttpResponseBody ResponseBody { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public IRequest OriginalRequest { get; set; }

        public void Dispose() { }

        public string[] GetHeaderNames()
        {
            return (Headers is null)
                ? Array.Empty<string>()
                : Headers.Keys.ToArray();
        }

        public string GetHeaderValue(string headerName)
        {
            string headerValue = null;
            Headers?.TryGetValue(headerName, out headerValue);
            return headerValue;
        }

        public bool IsHeaderPresent(string headerName) =>
            Headers?.ContainsKey(headerName) ?? false;

        public Stream OpenResponse() => Stream.Null;

        public Task<Stream> OpenResponseAsync() => Task.FromResult(OpenResponse());
    }
}
