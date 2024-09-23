public interface IClockPresenter
{
    IClockPresenter Init(IClockModel clockModel, IClockView clockView, string url);
    void InitClock();
}
