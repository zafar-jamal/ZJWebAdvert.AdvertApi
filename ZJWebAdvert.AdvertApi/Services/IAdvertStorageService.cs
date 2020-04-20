﻿using ZJAdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZJWebAdvert.AdvertApi.Services
{
   public  interface IAdvertStorageService
    {
        Task<string> Add(AdvertModel model);
        Task Confirm(ConfirmAdvertModel model);
        Task<bool> CheckHealthAsync();
    }
}
