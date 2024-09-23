using Core.Clock.Interfaces;
using UnityEngine;

namespace Core.Clock
{
    public class ClockBase : MonoBehaviour
    {
        [SerializeField]
        private string url = "http://time.microsoft.com";
        private IClockView clockView;
        private IClockInputView clockInputView;
        private IClockModel clockModel;
        private IClockPresenter clockPresenter;

        [SerializeField]
        private ClockArrowInputBase clockHourArrowInput;
        [SerializeField]
        private ClockArrowInputBase clockMinuteArrowInput;

        private void Awake()
        {
            clockView = GetComponent<IClockView>();
            clockModel = new ClockModel();
            clockInputView = GetComponent<IClockInputView>().Init(clockModel);
            clockPresenter = GetComponent<IClockPresenter>().Init(clockModel, clockView, url);

            clockHourArrowInput.Init(clockModel);
            clockMinuteArrowInput.Init(clockModel);
        }

        private void Start()
        {
            clockPresenter.InitClock();
        }
    }
}
