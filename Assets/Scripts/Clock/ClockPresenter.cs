using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ClockPresenter : MonoBehaviour, IClockPresenter
{
    private IClockModel clockModel;
    private IClockView clockView;
    private string url = "http://time.microsoft.com";

    private CancellationTokenSource cancellationTokenSource;

    public ClockPresenter(IClockModel model, IClockView view, string url)
    {
        cancellationTokenSource = new CancellationTokenSource();
        clockModel = model;
        clockView = view;
        this.url = url;
    }

    public void InitClock()
    {
        GetTimeFromServer(true);
        CheckTimeFromServer(false);
        StartClock();
    }

    private async void StartClock()
    {
        while (!cancellationTokenSource.Token.IsCancellationRequested && clockModel != null)
        {
            clockModel.LocalDateTime = clockModel.LocalDateTime.AddSeconds(1);

            await Task.Delay(1000);

            if (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                clockView.UpdateAnalogTime(clockModel.LocalDateTime);
                clockView.UpdateDigitalTime(clockModel.LocalDateTime);
            }
        }
    }

    private async void GetTimeFromServer(bool start)
    {
        await FetchTimeFromServer(start);
    }

    private async void CheckTimeFromServer(bool start)
    {
        while (true)
        {
            await Task.Delay(60000);
            await FetchTimeFromServer(start);
        }
    }

    private async Task FetchTimeFromServer(bool start)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        
        request.SendWebRequest();
        
        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            var response = request.downloadHandler.text;
            clockModel.UpdateLocalTime(response, start);
            clockView.UpdateAnalogTime(clockModel.LocalDateTime);
            clockView.UpdateDigitalTime(clockModel.LocalDateTime);
        }
        else
        {
            Debug.LogError("Error getting time from server: " + request.error);
        }

        request.Dispose();
    }

    private async void ShowAnalogTime()
    {
        while (!cancellationTokenSource.Token.IsCancellationRequested)
        {
            var time = clockModel.LocalDateTime;
            var hoursAngle = (time.Hour % 12 + time.Minute / 60f) * 30f;
            var minutesAngle = time.Minute * 6f;
            var secondsAngle = time.Second * 6f;

            clockView.UpdateAnalogTime(clockModel.LocalDateTime);

            await Task.Delay(100);
        }
    }

    private async void ShowDigitalTime()
    {
        while (!cancellationTokenSource.Token.IsCancellationRequested)
        {
            clockView.UpdateDigitalTime(clockModel.LocalDateTime);

            await Task.Delay(100);
        }
    }

    private void OnDestroy()
    {
        cancellationTokenSource.Cancel();
    }
}