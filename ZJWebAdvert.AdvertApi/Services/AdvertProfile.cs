using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZJAdvertApi.Models;
using AutoMapper;

namespace ZJWebAdvert.AdvertApi.Services
{
    public class AdvertProfile :Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertModel, AdvertDBModel>();

        }
    }
}
