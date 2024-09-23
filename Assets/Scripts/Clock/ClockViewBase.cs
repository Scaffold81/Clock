using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

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

    public void UpdateAnalogTime(DateTime time)
    {
        var hoursAngle = (time.Hour % 12 + time.Minute / 60f) * 30f;
        var minutesAngle = time.Minute * 6f;
        var secondsAngle = time.Second * 6f;

        hourArrow.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -hoursAngle), 0.2f);
        minuteArrow.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -minutesAngle), 0.2f);
        secondArrow.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -secondsAngle), 0.2f);
    }

    public void UpdateDigitalTime(DateTime time)
    {
        hour.text = time.Hour.ToString("D2") + " : ";
        minute.text = time.Minute.ToString("D2") + " : ";
        second.text = time.Second.ToString("D2");
    }
}
