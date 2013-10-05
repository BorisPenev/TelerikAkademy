using System;
using System.ServiceModel;

namespace WcfDemo.Server
{
    [ServiceContract]
    public interface IDemoService
    {
        [OperationContract]
        string GetDayOfWeek(DateTime value);

        [OperationContract]
        int GetStringRepeatedCount(string text, string search);

    }
}
