using System;

namespace Core.Clock.Interfaces
{
    public interface IClockView
    {
        IClockView Init(IClockModel clockModel);
        void UpdateAnalogTime(DateTime time);
        void UpdateDigitalTime(DateTime time);
    }
}
