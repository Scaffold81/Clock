using System;

namespace Core.Clock.Interfaces
{
    public interface IClockModel
    {
        DateTime LocalDateTime { get; set; }
        DateTime ServerDateTime { get; set; }
        bool IsTimeToSet { get; set; }

        void UpdateLocalDateTime(DateTime time);
        void UpdateLocalTime(string serverResponse, bool start);
    }
}