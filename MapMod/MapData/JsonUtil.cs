using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Reflection;

// Code taken from homothety: https://github.com/homothetyhk/RandomizerMod/

namespace MapMod.MapData
{
    public static class JsonUtil
    {
        public static readonly JsonSerializer _js;

        public static T Deserialize<T>(string embeddedResourcePath)
        {
            using StreamReader sr = new(typeof(JsonUtil).Assembly.GetManifestResourceStream(embeddedResourcePath));
            using JsonTextReader jtr = new(sr);
            return _js.Deserialize<T>(jtr);
        }

        public static T DeserializeString<T>(string json)
        {
            using StringReader sr = new(json);
            using JsonTextReader jtr = new(sr);
            return _js.Deserialize<T>(jtr);
        }

        public static void Serialize(object o, string fileName)
        {
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(typeof(JsonUtil).Assembly.Location), fileName), Serialize(o));
        }

        public static string Serialize(object o)
        {
            using StringWriter sw = new();
            _js.Serialize(sw, o);
            sw.Flush();
            return sw.ToString();
        }

        static JsonUtil()
        {
            _js = new JsonSerializer
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,

                //ContractResolver = new CustomContractResolver(),
            };

            _js.Converters.Add(new StringEnumConverter());
        }

        private class CustomContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                return base.CreateProperties(type, memberSerialization).Where(p => p.Writable).ToList();
            }
        }
    }
}