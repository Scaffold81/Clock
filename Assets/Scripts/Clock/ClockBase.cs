using UnityEngine;

public class ClockBase : MonoBehaviour
{
    [SerializeField]
    private string url = "http://time.microsoft.com";
    private IClockView clockView;
    private IClockInputView clockInputView;
    private IClockModel clockModel;
    private IClockPresenter clockPresenter;

    private void Awake()
    {
        clockView = GetComponent<IClockView>();
        clockModel = new ClockModel(); 
        clockInputView = GetComponent<IClockInputView>().Init(clockModel);
        clockPresenter = GetComponent<IClockPresenter>().Init(clockModel, clockView, url);;
    }

    private void Start()
    {
        clockPresenter.InitClock();
    }
}
