using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClockBase : MonoBehaviour
{
    [SerializeField]
    private string url = "http://time.microsoft.com";

    private IClockView clockView;
    private IClockModel clockModel;
    private IClockPresenter clockPresenter;

    private void Awake()
    {
        clockView = GetComponent<IClockView>();
        clockModel = new ClockModel();
        
        clockPresenter = new ClockPresenter(clockModel, clockView, url);
    }

    private void Start()
    {
        clockPresenter.InitClock();
    }
}
