using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZJAdvertApi.Models;
using Microsoft.AspNetCore.Mvc;
using ZJWebAdvert.AdvertApi.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZJWebAdvert.AdvertApi.Controllers
{
    [ApiController]
    [Route("adverts/v1")]
    public class Advert : Controller
    {
        private readonly IAdvertStorageService _advertStorageService;
        
        public Advert(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;

        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201,Type=typeof(CreateAdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            string  recordId;
            try
            {
                recordId = await _advertStorageService.Add(model);
            }
            catch(KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
            return StatusCode(201, new CreateAdvertResponse { Id= recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {

            try
            {
                await _advertStorageService.Confirm(model);
                await RaiseAdvertConfirmdMessage();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);                
            }

            return new OkResult();
        }

        private Task RaiseAdvertConfirmdMessage()
        {
            throw new NotImplementedException();
        }
    }
}
