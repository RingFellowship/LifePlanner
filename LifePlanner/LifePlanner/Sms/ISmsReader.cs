using System.Collections.Generic;

namespace LifePlanner.Sms
{
    public interface ISmsReader
    {
        IEnumerable<string> ReadSms();
    }
}