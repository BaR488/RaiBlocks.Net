﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RaiBlocks.Converters;
using RaiBlocks.ValueObjects;

namespace RaiBlocks.Results
{
    public class AccountHistoryResult
    {
        [JsonProperty("history")]
        public SingleAccountHistory[] Entries { get; set; }
    }

    public class SingleAccountHistory
    {
        [JsonProperty("hash")]
        public string Frontier { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public BlockType Type { get; set; }

        [JsonProperty("account")]
        public string RepresentativeBlock { get; set; }

        [JsonConverter(typeof(StringToRawConverter))]
        [JsonProperty("amount")]
        public RaiUnits.RaiRaw Amount { get; set; }
    }
}
