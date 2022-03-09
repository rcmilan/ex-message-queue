using MQ.Sender.Interfaces;

namespace MQ.Sender.Services
{
    internal class DateTimeService : IDateTimeService
    {
        public string Now()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
