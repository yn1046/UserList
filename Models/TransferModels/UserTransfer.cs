using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace UserList.Models.TransferModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class UserTransfer
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
