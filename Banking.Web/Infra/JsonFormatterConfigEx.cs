using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Banking.Web.Infra
{
    public static class JsonFormatterConfigEx
    {
        /// <summary>
        /// Configure application to return camelCase JSON 
        /// </summary>
        public static HttpConfiguration CamelCaseFormat(this HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return config;
        }
    }
}