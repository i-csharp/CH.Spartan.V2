﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;

namespace CH.Spartan.Events
{
    [Serializable]
    public class UserSettingUpdatedEventData : EventData
    {
        public long UserId { get; set; }
    }
}
