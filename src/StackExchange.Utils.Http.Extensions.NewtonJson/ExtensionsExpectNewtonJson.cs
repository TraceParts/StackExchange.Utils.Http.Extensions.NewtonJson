using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
// ReSharper disable UnusedMember.Global

// ReSharper disable once CheckNamespace
namespace StackExchange.Utils.Extensions.NewtonJson
{
#pragma warning disable 1591
    public static class ExtensionsExpectNewtonJson
#pragma warning restore 1591
    {
        /// <summary>
        /// <para>Holds handlers for ExpectJson(T) calls, so we don't re-create them in the common "default Options" case.</para>
        /// <para>Without this, we create a new Func for each ExpectJson call even</para>
        /// </summary>
        /// <typeparam name="T">The type being deserialized.</typeparam>
        private static class JsonHandler<T>
        {
            internal static Func<HttpResponseMessage, Task<T>> WithOptions(IRequestBuilder builder, JsonSerializerSettings serializerSettings = null)
            {
                return async responseMessage =>
                {
                    using (var responseStream = await responseMessage.Content.ReadAsStreamAsync()) // Get the response here
                    using (var streamReader = new StreamReader(responseStream))                    // Stream reader
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        using (builder.Settings.ProfileGeneral?.Invoke("Deserialize: JSON"))
                        {
                            if (builder.BufferResponse && responseStream.Length == 0)
                            {
                                return default;
                            }
                            // if serializerSettings is null, JsonSerializer will use default settings
                            // from DefaultSettings
                            var serializer = JsonSerializer.CreateDefault(serializerSettings);

                            return serializer.Deserialize<T>(jsonReader);
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Sets the response handler for this request to a JSON deserializer.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="builder">The builder we're working on.</param>
        /// <param name="serializerSettings">The Jil options to use when serializing.</param>
        /// <returns>A typed request builder for chaining.</returns>
        public static IRequestBuilder<T> ExpectNewtonJson<T>(this IRequestBuilder builder, JsonSerializerSettings serializerSettings = null) =>
            builder.WithHandler(JsonHandler<T>.WithOptions(builder, serializerSettings));
    }
}
