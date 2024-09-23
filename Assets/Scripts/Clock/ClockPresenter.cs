using Core.Clock.Interfaces;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.Clock
{
    public class ClockPresenter : MonoBehaviour, IClockPresenter
    {
        private IClockModel clockModel;
        private IClockView clockView;
        private string url = "http://time.microsoft.com";

        private float verificationPeriod = 3600;
        private float displayUpdateTime = 0.1f;

        private CancellationTokenSource cancellationTokenSource;

        public IClockPresenter Init(IClockModel clockModel, IClockView clockView, string url)
        {
            cancellationTokenSource = new CancellationTokenSource();
            this.clockModel = clockModel;
            this.clockView = clockView.Init(clockModel);
            this.url = url;

            return this;
        }

        public void InitClock()
        {
            GetTimeFromServer(true);
            StartCoroutine(CheckTimeFromServer(false));
            StartCoroutine(StartClock());
            StartCoroutine(ShowAnalogTime());
            StartCoroutine(ShowDigitalTime());
        }

        private IEnumerator StartClock()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                clockModel.LocalDateTime = clockModel.LocalDateTime.AddSeconds(1);

                yield return new WaitForSeconds(1);
                clockView.UpdateAnalogTime(clockModel.LocalDateTime);
                clockView.UpdateDigitalTime(clockModel.LocalDateTime);
            }
        }

        private void GetTimeFromServer(bool start)
        {
            StartCoroutine(FetchTimeFromServer(start));
        }

        private IEnumerator CheckTimeFromServer(bool start)
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                yield return new WaitForSeconds(verificationPeriod);
                StartCoroutine(FetchTimeFromServer(start));
            }
        }

        private IEnumerator FetchTimeFromServer(bool start)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = request.downloadHandler.text;
                clockModel.UpdateLocalTime(response, start);
                Debug.Log("Success getting time from server");
            }
            else
            {   //ToDo на всякий случай если сервер будет недоступен. По хорошему добавить перебор доступных серверов по списку и только после исчерпания попыток Error 
                clockModel.UpdateLocalDateTime(DateTime.Now);
                Debug.LogError("Error getting time from server: " + request.error);
            }

            clockView.UpdateAnalogTime(clockModel.LocalDateTime);
            clockView.UpdateDigitalTime(clockModel.LocalDateTime);

            request.Dispose();
        }

        private IEnumerator ShowAnalogTime()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                var time = clockModel.LocalDateTime;
                var hoursAngle = (time.Hour % 12 + time.Minute / 60f) * 30f;
                var minutesAngle = time.Minute * 6f;
                var secondsAngle = time.Second * 6f;

                clockView.UpdateAnalogTime(clockModel.LocalDateTime);

                yield return new WaitForSeconds(displayUpdateTime);
            }
        }

        private IEnumerator ShowDigitalTime()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                clockView.UpdateDigitalTime(clockModel.LocalDateTime);

                yield return new WaitForSeconds(displayUpdateTime);
            }
        }

        private void OnDestroy()
        {
            cancellationTokenSource.Cancel();
        }
    }
}