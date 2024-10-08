﻿using Core.Clock.Interfaces;
using Core.Clock.Structures;
using System;
using UnityEngine;

namespace Core.Clock
{
    public class ClockModel : IClockModel
    {
        public DateTime LocalDateTime { get; set; }
        public DateTime ServerDateTime { get; set; }
        public bool IsTimeToSet { get; set; }

        public void UpdateLocalTime(string serverResponse, bool start)
        {
            Data data = JsonUtility.FromJson<Data>(serverResponse);

            var unixTime = data.time / 1000;
            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime);
            var serverDateTime = dateTimeOffset.LocalDateTime;

            if (start)
            {
                LocalDateTime = serverDateTime;
            }
            else
            {
                if (serverDateTime != LocalDateTime)
                {
                    LocalDateTime = serverDateTime;
                }
            }
        }

        public void UpdateLocalDateTime(DateTime time)
        {
            LocalDateTime = time;
        }
    }
}
