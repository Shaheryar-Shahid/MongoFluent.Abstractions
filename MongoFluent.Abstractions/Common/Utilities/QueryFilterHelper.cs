using MongoDB.Driver;
using System.Collections.Generic;

namespace MongoFluent.Abstractions.Common.Utilities
{
    public static class QueryFilterHelper
    {
        public static UpdateDefinition<T> ToUpdateDefinition<T>(Dictionary<string, object> updates)
        {
            var updateDefinitions = new List<UpdateDefinition<T>>();

            foreach (var kvp in updates)
            {
                updateDefinitions.Add(Builders<T>.Update.Set(kvp.Key, kvp.Value));
            }

            return Builders<T>.Update.Combine(updateDefinitions);
        }
    }
}
