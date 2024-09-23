using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    private bool isTimeToSet;

    IClockModel model;

    public IClockInputView Init(IClockModel clockModel)
    {
        model = clockModel;
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

        if (isTimeToSet)
        {
            colors.color = Color.white;

            hour.interactable = false;
            minute.interactable = false;
            second.interactable = false;
            isTimeToSet = false;
        }
        else
        {
            colors.color = Color.red;

            hour.interactable = true;
            minute.interactable = true;
            second.interactable = true;
            isTimeToSet = true;
        }
    }
    private void ChangeHour(string value)
    {
        if (int.TryParse(value, out int hour))
        {
            var date = model.LocalDateTime;

            if (hour < 24)
                date = new DateTime(date.Year, date.Month, date.Day, hour, date.Minute, date.Second);

            model.UpdateLocalDateTime(date);
        }
        else
        {
            Debug.LogError("Invalid input for hour: " + value);
        }
    }
    private void ChangeMinute(string value)
    {
        if (int.TryParse(value, out int minute))
        {
            var date = model.LocalDateTime;
            if (minute < 60)
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, minute, date.Second);

            model.UpdateLocalDateTime(date);
        }
        else
        {

            Debug.LogError("Invalid input for hour: " + value);
        }
    }

    private void ChangeSecond(string value)
    {
        if (int.TryParse(value, out int second))
        {
            var date = model.LocalDateTime;

            if (second < 60)
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, second);

            model.UpdateLocalDateTime(date);
        }
        else
        {
            Debug.LogError("Invalid input for hour: " + value);
        }

    }
}
