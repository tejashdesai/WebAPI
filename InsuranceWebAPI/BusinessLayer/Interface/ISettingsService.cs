﻿using InsuranceWebAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceWebAPI.BusinessLayer.Interface
{
    public interface ISettingsService
    {
        bool UpdateSettings(SettingsDTO settingEntity);
        SettingsDTO GetSettings();
    }
}
