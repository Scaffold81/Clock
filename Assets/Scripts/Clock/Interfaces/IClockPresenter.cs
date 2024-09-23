namespace Core.Clock.Interfaces
{
    public interface IClockPresenter
    {
        IClockPresenter Init(IClockModel clockModel, IClockView clockView, string url);
        void InitClock();
    }
}
