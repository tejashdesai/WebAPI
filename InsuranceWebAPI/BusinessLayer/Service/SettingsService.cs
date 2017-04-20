using InsuranceWebAPI.BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceWebAPI.Entity;
using InsuranceWebAPI.DataLayer.UnitOfWork;

namespace InsuranceWebAPI.BusinessLayer.Service
{
    public class SettingsService : ISettingsService
    {
        private readonly UnitOfWork _unitOfWork;

        public SettingsService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public bool UpdateSettings(SettingsDTO settingEntity)
        {
            var success = false;
            var settings = _unitOfWork.SettingRepository.DbSet.First();
            if (settings != null)
            {
                settings.CredentialEmailID = settingEntity.CredentialEmailID;
                settings.CredentialPassword = settingEntity.CredentialPassword;
                settings.Mobile = settingEntity.Mobile;
                settings.SenderEmail = settingEntity.SenderEmail;
                settings.SenderName = settingEntity.SenderName;
                settings.SenderPassword = settingEntity.SenderPassword;
                settings.SMSPassword = settingEntity.SMSPassword;
                settings.SMSRoute = settingEntity.SMSRoute;
                settings.SMSSender = settingEntity.SMSSender;
                settings.SMSType = settingEntity.SMSType;
                settings.SMSUserName = settingEntity.SMSUserName;

                _unitOfWork.SettingRepository.Update(settings);
                _unitOfWork.Save();
                success = true;
            }
            return success;
        }
    }
}