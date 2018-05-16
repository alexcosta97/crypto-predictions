using CryptoStats.GDAX.Shared.Utilities.Clock;
using CryptoStats.GDAX.Shared.Utilities.Extensions;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using CryptoStats.GDAX.Exceptions;
using CryptoStats.GDAX.Shared;

namespace CryptoStats.GDAX.Network.HttpRequest
{
    public class HttpRequestMessageService : IHttpRequestMessageService{

        private readonly IClock clock;

        private readonly bool sandBox;

        public HttpRequestMessageService(
            IClock clock,
            bool sandBox)
        {
            this.clock = clock;
            this.sandBox = sandBox;
        }

        public HttpRequestMessage CreateHttpRequestMessage(
            HttpMethod httpMethod,
            string requestUri,
            string contentBody = "")
        {

            var apiUri = sandBox
                ? ApiUris.ApiUriSandbox
                : ApiUris.ApiUri;

            var requestMessage = new HttpRequestMessage(httpMethod, new Uri(new Uri(apiUri), requestUri))
            {
                Content = contentBody == string.Empty
                    ? null
                    : new StringContent(contentBody, Encoding.UTF8, "application/json")
            };

            var timeStamp = clock.GetTime().ToTimeStamp();

            AddHeaders(requestMessage, timeStamp);
            return requestMessage;
        }

        private void AddHeaders(
            HttpRequestMessage httpRequestMessage,
            double timeStamp)
        {
            httpRequestMessage.Headers.Add("User-Agent", "GDAXClient");
            httpRequestMessage.Headers.Add("CB-ACCESS-TIMESTAMP", timeStamp.ToString("F0", CultureInfo.InvariantCulture));
        }
    }
}
