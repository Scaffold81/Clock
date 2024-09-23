using System;
using UnityEngine;

namespace Core.Clock
{
    public class ClockHourArrowInput : ClockArrowInputBase
    {
        protected override float GetRotationValue()
        {
            return 11 - Mathf.FloorToInt((arrow.eulerAngles.z % 360) / 30f);
        }

        protected override int GetTimeUnit()
        {
            return model.LocalDateTime.Hour;
        }

        protected override void UpdateDateTime(DateTime date, float rotationValue, int timeUnit)
        {
            model.UpdateLocalDateTime(new DateTime(date.Year, date.Month, date.Day, (int)rotationValue, date.Minute, date.Second));
        }
    }
}
