using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IteaLinqToSql
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class ExcludeFromResponse : Attribute
    {
    }

    public class JsonExcludeFromResponse : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {

            var includedPropList = type.GetProperties()
                .Where(prop => !Attribute.IsDefined(prop, typeof(ExcludeFromResponse)) &&
                               !Attribute.IsDefined(prop, typeof(TimestampAttribute)))
                .Select(x => x.Name)
                .ToList();

            var properties = base.CreateProperties(type, memberSerialization)
                .Where(p => includedPropList.Contains(p.PropertyName))
                .ToList();

            return properties;
        }
    }
}
