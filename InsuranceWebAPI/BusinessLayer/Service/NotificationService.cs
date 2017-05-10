using InsuranceWebAPI.BusinessLayer.Interface;
using InsuranceWebAPI.DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceWebAPI.BusinessLayer.Service
{
    public class NotificationService : INotificationService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Notification service constructor
        /// </summary>
        public NotificationService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public bool SendNotification(int policyHistoryId)
        {
            var policyHistory = _unitOfWork.PolicyHistoryRepository.GetByID(policyHistoryId);
            var setting = _unitOfWork.SettingRepository.GetAll().FirstOrDefault();
            var ExpiryDate = policyHistory.EndDate.HasValue ? policyHistory.EndDate.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
            var emailSent = new Common().sendMail(policyHistory.Policy.Name, policyHistory.PolicyNumber, ExpiryDate,
                 policyHistory.Policy.Email, setting.SenderEmail, setting.SenderName, setting.CredentialEmailID,
                 setting.CredentialPassword);

            var smsSent = new Common().sendSMS(policyHistory.Policy.Name, policyHistory.PolicyNumber, ExpiryDate,
              setting.SMSUserName, setting.SMSPassword, setting.SMSSender, setting.SMSType, setting.SMSRoute, policyHistory.Policy.Mobile,
              policyHistory.Policy.PolicyType1.PolicyTypeName);
            return true;
        }
    }
}