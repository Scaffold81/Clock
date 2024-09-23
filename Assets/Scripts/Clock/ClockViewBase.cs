using Core.Clock.Interfaces;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace Core.Clock
{
    public class ClockViewBase : MonoBehaviour, IClockView
    {
        [SerializeField]
        private GameObject hourArrow;
        [SerializeField]
        private GameObject minuteArrow;
        [SerializeField]
        private GameObject secondArrow;

        [SerializeField]
        private TMP_InputField hour;
        [SerializeField]
        private TMP_InputField minute;
        [SerializeField]
        private TMP_InputField second;

        private IClockModel model;

        public IClockView Init(IClockModel clockModel)
        {
            model = clockModel;
            return this;
        }

        public void UpdateAnalogTime(DateTime time)
        {
            if (model.IsTimeToSet) return;

            var hoursAngle = (time.Hour % 12 + time.Minute / 60f) * 30f;
            var minutesAngle = time.Minute * 6f;
            var secondsAngle = time.Second * 6f;

            hourArrow.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -hoursAngle), 0.2f);
            minuteArrow.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -minutesAngle), 0.2f);
            secondArrow.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -secondsAngle), 0.2f);
        }

        public void UpdateDigitalTime(DateTime time)
        {
            if (model.IsTimeToSet) return;

            hour.text = time.Hour.ToString("D2");
            minute.text = time.Minute.ToString("D2");
            second.text = time.Second.ToString("D2");
        }
    }
}
