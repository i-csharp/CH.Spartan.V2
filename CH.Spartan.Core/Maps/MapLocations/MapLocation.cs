﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CH.Spartan.Maps;

namespace CH.Spartan.Maps
{

    public class MapLocation
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("infocode")]
        public string Infocode { get; set; }

        [JsonProperty("regeocode")]
        public Regeocode Regeocode { get; set; }

        public override string ToString()
        {
            return Regeocode.FormattedAddress;
        }
    }

}
