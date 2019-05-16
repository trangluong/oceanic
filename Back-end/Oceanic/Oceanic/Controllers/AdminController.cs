using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oceanic.Core;
using Oceanic.Services.Interface;

namespace Oceanic.Controllers
{
    [Produces("application/json")]
 //   [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        //private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("city")]
        [HttpGet]
        public IEnumerable<CityViewModel> LoadCity()
        {
            return _adminService.LoadCity(true).Select(x => new CityViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            });
        }

        [Route("goodstype")]
        [HttpGet]
        public IEnumerable<GoodsType> LoadGoodsType()
        {
            return _adminService.LoadGoodsTypes();
        }

        [Route("size")]
        [HttpGet]
        public IEnumerable<Size> LoadSize()
        {
            return _adminService.LoadSizeSettings();
        }

        [Route("price")]
        [HttpGet]
        public IEnumerable<Price> LoadPrice()
        {
            return _adminService.LoadPriceSettings();
        }


        [Route("extrafee")]
        [HttpGet]
        public IEnumerable<ExtraFee> LoadExtraFee()
        {
            return _adminService.LoadExtraFeeSettings();
        }

        [HttpPost()]
        [Route("api/size")]
        public void UpdateSize([FromBody]SizeViewModel sizeModel)
        {
            Size size = new Size()
            {
                Id = sizeModel.id,
                Type = sizeModel.type,
                MaxHeight = sizeModel.maxHeight,
                MaxBreath = sizeModel.maxBreath,
                MaxDepth = sizeModel.maxDepth
            };

            _adminService.UpdateSizeSettings(size);

        }

    }
}