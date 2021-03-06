﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CH.Spartan.Maps;

namespace CH.Spartan.Maps
{

    public class Pois
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tel")]
        public object Tel { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("poiweight")]
        public string Poiweight { get; set; }

        [JsonProperty("businessarea")]
        public string Businessarea { get; set; }
    }

}
