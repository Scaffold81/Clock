using System;

public interface IClockModel
{
    DateTime LocalDateTime { get; set; }
    DateTime ServerDateTime { get; set; }

    void UpdateLocalDateTime(DateTime time);
    void UpdateLocalTime(string serverResponse, bool start);
}
