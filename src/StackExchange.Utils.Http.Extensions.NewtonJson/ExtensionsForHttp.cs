using System.Net.Http;
using System.Text;

using Newtonsoft.Json;

namespace StackExchange.Utils.Extensions.NewtonJson
{
    internal static class ExtensionsForHttp
    {
        /// <summary>
        /// Adds JSON (NewtonSoft-serialized) content as the body for this request.
        /// </summary>
        /// <param name="builder">The builder we're working on.</param>
        /// <param name="obj">The object to serialize as JSON in the body.</param>
        /// <param name="serializerSettings">The NewtownSoft setting to use when serializing (if null, serializer take global setting or default).</param>
        /// <returns>The request builder for chaining.</returns>
        public static IRequestBuilder SendNewtonJson(this IRequestBuilder builder, object obj, JsonSerializerSettings serializerSettings = null) =>
            builder.SendContent(new StringContent(JsonConvert.SerializeObject(obj, serializerSettings), Encoding.UTF8, "application/json"));
    }
}
