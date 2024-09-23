using System;

public interface IClockView
{
    void UpdateAnalogTime(DateTime time);
    void UpdateDigitalTime(DateTime time);
}
