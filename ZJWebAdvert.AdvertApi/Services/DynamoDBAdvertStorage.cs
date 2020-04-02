﻿using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace ZJWebAdvert.AdvertApi.Services
{
    public class DynamoDBAdvertStorage : IAdvertStorageService
    {
        private readonly IMapper _mapper;

        public DynamoDBAdvertStorage(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> Add(AdvertModel model)
        {
            var dbModel = _mapper.Map<AdvertDBModel>(model);
            
            dbModel.Id = new Guid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = AdvertStatus.Pending;
            
            using (var client = new AmazonDynamoDBClient())
            {
                using(var context=new DynamoDBContext(client))
                {
                    await context.SaveAsync(dbModel);
                }
            }
            return dbModel.Id;
        }

       public async Task Confirm(ConfirmAdvertModel model)
        {
            using (var client=new AmazonDynamoDBClient())
            {
                using(var context=new DynamoDBContext(client))
                {
                    var record = await context.LoadAsync<AdvertDBModel>(model.Id);
                    if(record==null)
                    {
                        throw new KeyNotFoundException($"A record with ID={model.Id} was not found");
                    }
                    if (model.Status==AdvertStatus.Active)
                    {
                        record.Status = AdvertStatus.Active;
                        await context.SaveAsync(record);
                    }
                    else
                    {
                        await context.DeleteAsync(record);
                    }
                }
                
            }
        }
    }
}
