using Core.Clock.Interfaces;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Clock
{
    public class ClockInputViewBase : MonoBehaviour, IClockInputView
    {
        [SerializeField]
        private TMP_InputField hour;
        [SerializeField]
        private TMP_InputField minute;
        [SerializeField]
        private TMP_InputField second;

        [SerializeField]
        private Button setTime;

        private IClockModel model;

        public IClockInputView Init(IClockModel clockModel)
        {
            model = clockModel;
            model.IsTimeToSet = false;

            Subscribe();

            return this;
        }

        private void Subscribe()
        {

            hour.onEndEdit.AddListener(ChangeHour);
            minute.onEndEdit.AddListener(ChangeMinute);
            second.onEndEdit.AddListener(ChangeSecond);

            setTime.onClick.AddListener(ChangeSetTimeState);
        }

        private void ChangeSetTimeState()
        {
            var colors = setTime.image;

            if (model.IsTimeToSet)
            {
                colors.color = Color.white;

                hour.interactable = false;
                minute.interactable = false;
                second.interactable = false;
                model.IsTimeToSet = false;
            }
            else
            {
                colors.color = Color.red;

                hour.interactable = true;
                minute.interactable = true;
                second.interactable = true;
                model.IsTimeToSet = true;
            }
        }

        private void ChangeHour(string value)
        {
            if (int.TryParse(value, out int hourValue))
            {
                ChangeTimeValue(hourValue, 24, (date, newValue) => new DateTime(date.Year, date.Month, date.Day, newValue, date.Minute, date.Second));
            }
            else
            {
                Debug.LogError("Invalid input for hour: " + value);
            }
        }

        private void ChangeMinute(string value)
        {
            if (int.TryParse(value, out int minuteValue))
            {
                ChangeTimeValue(minuteValue, 60, (date, newValue) => new DateTime(date.Year, date.Month, date.Day, date.Hour, newValue, date.Second));
            }
            else
            {
                Debug.LogError("Invalid input for minute: " + value);
            }
        }

        private void ChangeSecond(string value)
        {
            if (int.TryParse(value, out int secondValue))
            {
                ChangeTimeValue(secondValue, 60, (date, newValue) => new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, newValue));
            }
            else
            {
                Debug.LogError("Invalid input for second: " + value);
            }
        }

        private void ChangeTimeValue(int value, int maxLimit, Func<DateTime, int, DateTime> updateFunc)
        {
            var date = model.LocalDateTime;

            if (value < maxLimit)
            {
                date = updateFunc(date, value);
                model.UpdateLocalDateTime(date);
            }
            else
            {
                Debug.LogError("Invalid input value: " + value);
            }
        }
    }
}
