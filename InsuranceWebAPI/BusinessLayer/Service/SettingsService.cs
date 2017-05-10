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

        public SettingsDTO GetSettings()
        {
            var settings = _unitOfWork.SettingRepository.GetAll().First();
            return new SettingsDTO
            {
                CredentialEmailID = settings.CredentialEmailID,
                CredentialPassword = settings.CredentialPassword,
                Mobile = settings.Mobile,
                SenderEmail = settings.SenderEmail,
                SenderName = settings.SenderName,
                SenderPassword = settings.SenderPassword,
                SMSPassword = settings.SMSPassword,
                SMSRoute = settings.SMSRoute,
                SMSSender = settings.SMSSender,
                SMSType = settings.SMSType,
                SMSUserName = settings.SMSUserName,
                DailySubject = settings.DailySubject,
                SMTPIP = settings.SMTPIP,
                SMTPPORT = settings.SMTPPORT
            };
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
                settings.SMTPIP = settingEntity.SMTPIP;

                _unitOfWork.SettingRepository.Update(settings);
                _unitOfWork.Save();
                success = true;
            }
            return success;
        }
    }
}