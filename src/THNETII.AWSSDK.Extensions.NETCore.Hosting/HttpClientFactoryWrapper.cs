using Amazon.Runtime;

using Microsoft.Extensions.Options;

using System;
using System.Net.Http;

namespace Amazon.Extensions.NETCore.Hosting
{
    using AwsHttpClientFactory = Amazon.Runtime.HttpClientFactory;

    /// <summary>
    /// A wrapper type that wraps a <see cref="IHttpClientFactory"/> from
    /// the <c>Microsoft.Extensions.Http</c> package to conform to the AWSSDK
    /// <see cref="AwsHttpClientFactory"/> type.
    /// </summary>
    public class HttpClientFactoryWrapper : AwsHttpClientFactory
    {
        private readonly string name;
        private readonly IHttpClientFactory httpFactory;

        /// <summary>
        /// Creates a new wrapper for use by the AWS SDK using the DI configured
        /// <see cref="IHttpClientFactory"/> instance and optionally the
        /// specified HTTP client name.
        /// </summary>
        /// <param name="httpFactory">An <see cref="IHttpClientFactory"/> instance used to create <see cref="HttpClient"/> instances.</param>
        /// <param name="name">Optional. Defaults to use <see cref="Options.DefaultName"/> is set to <see langword="null"/>.</param>
        public HttpClientFactoryWrapper(IHttpClientFactory httpFactory,
            string? name = null)
        {
            this.httpFactory = httpFactory
                ?? throw new ArgumentNullException(nameof(httpFactory));
            this.name = name ?? Options.DefaultName;
        }

        /// <inheritdoc/>
        public override HttpClient CreateHttpClient(IClientConfig clientConfig) =>
            httpFactory.CreateClient(name);
    }
}
