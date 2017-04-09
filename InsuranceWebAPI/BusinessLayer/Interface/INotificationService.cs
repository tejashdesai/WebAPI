using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceWebAPI.BusinessLayer.Interface
{
    public interface INotificationService
    {
        bool SendNotification(int policyHistoryId);
    }
}
