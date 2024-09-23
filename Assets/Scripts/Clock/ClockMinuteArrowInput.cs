using System;
using UnityEngine;

namespace Core.Clock
{
    public class ClockMinuteArrowInput : ClockArrowInputBase
    {
        protected override float GetRotationValue()
        {
            return 59 - Mathf.FloorToInt((arrow.eulerAngles.z % 360) / 6f);
        }

        protected override int GetTimeUnit()
        {
            return model.LocalDateTime.Minute;
        }

        protected override void UpdateDateTime(DateTime date, float rotationValue, int timeUnit)
        {
            model.UpdateLocalDateTime(new DateTime(date.Year, date.Month, date.Day, date.Hour, (int)rotationValue, date.Second));
        }
    }
}
